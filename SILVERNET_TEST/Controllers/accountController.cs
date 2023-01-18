using Newtonsoft.Json;
using REST_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace SILVERNET_TEST.Controllers
{
    public class accountController : Controller
    {
        /// <summary>
        /// returns Index view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// returns register view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// request that returns all users. 
        /// </summary>
        /// <returns></returns>
        public List<Users> getUsers()
        {
            WebRequest req = WebRequest.Create("https://localhost:44307/users/getUsers");
            WebResponse res = req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            var result = reader.ReadToEnd().Trim();
            var user = JsonConvert.DeserializeObject<List<Users>>(result);
            return user;
        }

       /// <summary>
       /// the registed method.
       /// </summary>
       /// <param name="newUser"></param>
       /// <returns></returns>
        [System.Web.Http.HttpPost]
        public bool SignUp([FromBody] Users newUser)
        {
            try
            {
                List<Users> listUsers = getUsers();
                var isUser = listUsers.Any(x => x.Email == newUser.Email || x.UidSerie == newUser.UidSerie);
                if (!isUser)
                {
                    WebRequest req = WebRequest.Create("https://localhost:44307/users/Add");
                    req.Method = "post";
                    req.ContentType = "application/json;charset-UTF-8";
                    Users user = new Users
                    {
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        UidSerie = newUser.UidSerie,
                        Email = newUser.Email,
                        BirthDate = newUser.BirthDate,
                        Gender = newUser.Gender,
                        userName = newUser.FirstName + "-" + newUser.LastName,
                        Password = convertSHA256(newUser.Password)
                    };
                    using (var Writer = new StreamWriter(req.GetRequestStream()))
                    {
                        string JSONuser = JsonConvert.SerializeObject(user);
                        Writer.Write(JSONuser);
                        Writer.Flush();
                        Writer.Close();
                    }
                    WebResponse res = req.GetResponse();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to register this user. Error: " + ex);
            }
        }

        /// <summary>
        /// the login method
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public string SignIn([FromBody] Users User)
        {
            List<Users> listUser = getUsers();
            string result = "";
            var isUser = listUser.Any(x => x.userName == User.userName);
            if (isUser)
            {
                var matchPassword = convertSHA256(User.Password);
                var userPassword = listUser.Where(x => x.userName == User.userName);
                if (userPassword.First().Password == matchPassword)
                {
                    Session["User"] = isUser;
                    result = "In";
                }
                else
                {
                    result = "notPassword";
                }
            }
            else
            {
                result = "notExist";
            }
            return result;
        }

        /// <summary>
        /// method that hashes the password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string convertSHA256(string password)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(password));
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
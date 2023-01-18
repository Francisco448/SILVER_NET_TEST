using REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace REST_API.Controllers
{
    public class usersController : ApiController
    {
        /// <summary>
        /// context of database.
        /// </summary>
        SILVERDB db = new SILVERDB();

        /// <summary>
        /// request that returns all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("users/getUsers")]
        public IEnumerable<Users> getUsers() => db.Users.ToList();

        /// <summary>
        /// request that insert a new user.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/Add")]
        public bool addUser(Users newUser)
        {
            try
            {
                db.Users.Add(newUser);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to Register the new user. Error: " + ex);
            }
        }
    }
}
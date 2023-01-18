using Newtonsoft.Json;
using REST_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace SILVERNET_TEST.Controllers
{
    public class ProfileController : Controller
    {
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;

        /// <summary>
        /// returns the index view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// request that returns all products.
        /// </summary>
        /// <returns></returns>
        public List<Products> getProducts()
        {
            WebRequest req = WebRequest.Create("https://localhost:44307/Products/getProduts");
            WebResponse res = req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            var result = reader.ReadToEnd().Trim();
            var products = JsonConvert.DeserializeObject<List<Products>>(result);
            return products;
        }

        /// <summary>
        /// request that returns users by id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Products> getProductsById(int Id)
        {
            WebRequest req = WebRequest.Create("https://localhost:44307/Products/getProdutsById/" + Id);
            WebResponse res = req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            var result = reader.ReadToEnd().Trim();
            var products = JsonConvert.DeserializeObject<List<Products>>(result);
            return products;
        }

        /// <summary>
        /// datatable configuration and data completion.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Product/Data")]
        public ActionResult DataTable()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            var dataProducts = getProducts();
            if (searchValue != "")
            {
                dataProducts = dataProducts.Where(x => x.Name.Contains(searchValue)).ToList();
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                dataProducts = dataProducts.OrderBy(sortColumn + " " + sortColumnDir).ToList();
            }

            recordsTotal = dataProducts.Count;

            dataProducts.Skip(skip).Take(pageSize);

            return Json(new { draw = draw, recordFiltered = recordsTotal, recordsTotal = recordsTotal, data = dataProducts });
        }


        /// <summary>
        /// request that insert a new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public bool addProduct([System.Web.Http.FromBody]Products product)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://localhost:44307/Product/Add");
                req.Method = "post";
                req.ContentType = "application/json;charset-UTF-8";
                Products newProduct = new Products
                {
                    Name = product.Name,
                    BuyCost = product.BuyCost,
                    SalePrice = product.SalePrice
                };
                using (var writer = new StreamWriter(req.GetRequestStream()))
                {
                    var JSONProduct = JsonConvert.SerializeObject(newProduct);
                    writer.Write(JSONProduct);
                    writer.Flush();
                    writer.Close();
                }
                WebResponse res = req.GetResponse();
                return true;
            }catch(Exception ex)
            {
                throw new Exception("Error to add the new product. Error: " + ex);
            }
        }

        /// <summary>
        /// request that update a product.
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost]
        public bool updateProduct([System.Web.Http.FromBody] Products products)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://localhost:44307/Product/Update/" + products.Id);
                req.Method = "post";
                req.ContentType = "application/json;charset-UTF-8";
                Products editProduct = new Products
                {
                    Name = products.Name,
                    BuyCost = products.BuyCost,
                    SalePrice = products.SalePrice
                };
                using (var writer = new StreamWriter(req.GetRequestStream()))
                {
                    var product = JsonConvert.SerializeObject(editProduct);
                    writer.Write(product);
                    writer.Flush();
                    writer.Close();
                }
                WebResponse res = req.GetResponse();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error to update the product. Error: " + ex);
            }   
        }

        /// <summary>
        /// request that delete a product.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public bool deleteProduct([System.Web.Http.FromBody] int Id)
        {
            try
            {
                WebRequest req = WebRequest.Create("https://localhost:44307/Product/Delete/" + Id);
                WebResponse res = req.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to delete the product. Error: " + ex);
            }
        }

        /// <summary>
        /// method of logout.
        /// </summary>
        /// <returns></returns>
        public bool LogOut()
        {
            Session["user"] = null;
            return true;
        }
    }
}
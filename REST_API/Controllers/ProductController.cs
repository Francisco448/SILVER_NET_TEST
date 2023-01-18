using REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace REST_API.Controllers
{
    public class ProductController : ApiController
    {
        /// <summary>
        /// context of database.
        /// </summary>
        private SILVERDB db = new SILVERDB();

        /// <summary>
        /// request that returns all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Products/getProduts")]
        public IEnumerable<Products> GetProducts() => db.Products.ToList();

        /// <summary>
        /// request that returns users by id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Products/getProdutsById/{Id:int}")]
        public IEnumerable<Products> GetProductsById(int Id) => db.Products.Where(x => x.Id == Id).ToList();

        /// <summary>
        /// request that insert a new product.
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Product/Add")]
        public bool AddProduct([FromBody] Products Product)
        {
            try
            {
                db.Products.Add(Product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to add the product. Error: " + ex);
            }
        }

        /// <summary>
        /// request that update a product.
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Product/Update/{Id:int}")]
        public bool UpdateProduct([FromBody] Products Product, int Id)
        {
            try
            {
                var editProduct = db.Products.Where(x => x.Id == Id).FirstOrDefault();
                editProduct.Name = Product.Name;
                editProduct.BuyCost = Product.BuyCost;
                editProduct.SalePrice = Product.SalePrice;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error to update the product. Error: " + ex);
            }
        }

        /// <summary>
        /// request that delete a product.
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet]
        [Route("Product/Delete/{Id:int}")]
        public void DeleteProduct(int Id)
        {
            try
            {
                var identity = GetProductsById(Id).FirstOrDefault();

                if (identity != null)
                {
                    db.Products.Remove(identity);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Not found this product, maybe is already deleted");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error to delete this product. Error: " + ex);
            }
        }
    }
}
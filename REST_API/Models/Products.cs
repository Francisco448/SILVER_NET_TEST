using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace REST_API.Models
{
    public class Products
    {
        /// <summary>
        /// gets ot sets Id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// gets ot sets Name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// gets ot sets BuyCost.
        /// </summary>
        public int BuyCost { get; set; }
        /// <summary>
        /// gets ot sets SalePrice.
        /// </summary>
        public double SalePrice { get; set; }

    }
}
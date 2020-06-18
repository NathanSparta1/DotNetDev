using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class ProductItem
    {
        public int ProductItemID { get; set; }
        public string ImageURL { get; set; }
        public string ImageAlt { get; set; }
        public string Title { get; set; }
    }
}
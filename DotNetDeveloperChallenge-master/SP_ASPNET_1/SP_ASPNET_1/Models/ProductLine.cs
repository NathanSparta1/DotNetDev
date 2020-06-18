using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SP_ASPNET_1.Models
{
    public class ProductLine
    {
        public int ProductLineID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<ProductItem> ProductItems { get; set; }
    }
}
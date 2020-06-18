using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.ViewModels
{
    public class BlogIndexViewModel : IBlogViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
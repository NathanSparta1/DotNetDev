using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SP_ASPNET_1.Models;

namespace SP_ASPNET_1.BusinessLogic
{
    public static class Constants
    {
        public static string DISPLAY_DATETIME_FORMAT => "MMMM dd, yyyy";
        public static string BLOGPOST_IMAGE_PREFIX => "/Content/images/posts/";
        public static string PRODUCT_IMAGE_PREFIX => "/Content/images/products/";
        public static string POST_IMAGE_PREFIX => "/Content/images/posts/";

        public static BlogPost FAKE_BLOGPOST => new BlogPost()
        {
            Author = new Author()
            {
                AuthorID = -1,
                Name = "FAKE",
                Surname = "FAKE"
            },
            BlogPostID = -1,
            Content =
                "FAKEFFAKEKEFFAKEKEFFAKEKEFFAKEKEFFAKEKEFFAKEKEFFAKEKEFFAKEKEFFAKEKEFFAKEKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEFAKEa",
            DateTime = DateTime.Now,
            ImageUrl = String.Empty,
            Title = "FAKE FAKEFAKEFAKE FAKE"

        };
    }
}
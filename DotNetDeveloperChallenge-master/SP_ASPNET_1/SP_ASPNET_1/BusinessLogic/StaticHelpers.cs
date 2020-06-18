using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SP_ASPNET_1.Models;
using SP_ASPNET_1.ViewModels;

namespace SP_ASPNET_1.BusinessLogic
{
    public static class StaticHelpers
    {
        /// <summary>
        /// Inspired by https://stackoverflow.com/a/20411015/9316685
        /// </summary>
        ///  <param name="controllers">conditioned controllers, comma delimited</param>
        /// <param name="actions">conditioned actions, comma delimited</param>
        /// <param name="cssClass">css class/es to be outputed</param>
        /// <returns>string representing css class</returns>
        public static string IsActive(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "active")
        {
            var viewContext = html.ViewContext;
            var isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
            {
                viewContext = html.ViewContext.ParentActionViewContext;
            }

            var routeValues = viewContext.RouteData.Values;
            var currentAction = routeValues["action"].ToString();
            var currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(actions))
            {
                actions = currentAction;
            }


            if (String.IsNullOrEmpty(controllers))
            {
                controllers = currentController;
            }

            var acceptedActions = actions.Trim().Split(',');
            var acceptedControllers = controllers.Trim().Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static IEnumerable<BlogPost> IncludeImagePrefix(this IEnumerable<BlogPost> blogPosts, string prefix)
        {
            return blogPosts.Select(x =>
            {
                x.ImageUrl = prefix + x.ImageUrl;
                return x;
            });
        }

        public static BlogSinglePostViewModel ToBlogSinglePostViewModel(this BlogPost blogPostWithAuthor)
        {
            return new BlogSinglePostViewModel()
            {
                Author = blogPostWithAuthor.Author,
                BlogPost = blogPostWithAuthor
            };
        }
    }
}
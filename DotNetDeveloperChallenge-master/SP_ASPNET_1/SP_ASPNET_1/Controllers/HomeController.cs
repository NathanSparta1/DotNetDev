using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SP_ASPNET_1.DbFiles.Operations;
using SP_ASPNET_1.DbFiles.UnitsOfWork;
using SP_ASPNET_1.ViewModels;

namespace SP_ASPNET_1.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private HomeOperations _homeOperations = new HomeOperations();

        //[Route("~/", Name = "default")]
        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = "ahoj";
            return View();
        }

        [Route("About")]
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [Route("Product")]
        [HttpGet]
        public ActionResult Product()
        {
            HomeProductViewModel productLines = _homeOperations.GetHomeProductViewModel();

            return View(productLines);
        }
    }
}
using eUseControl.BusinessLogic;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.BusinessLogic.Services;
using eUseControl.Controllers;
using eUseControl.Domain.Entities;
using eUseControl.Domain.Entities.Products;
using eUseControl.Web.Controllers;
using eUseControl.Web.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebAplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProduct _product;
        public HomeController()
        {
            var bl = new BusinessLogic();
            _product = bl.GetProductBL();
        }
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                System.Web.HttpContext.Current.Session.Clear();
            }

            return View(_product.GetProductList());
        }
        public ActionResult Men()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                System.Web.HttpContext.Current.Session.Clear();
            }

            return View(_product.GetProductList());
        }
        public ActionResult Women()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                System.Web.HttpContext.Current.Session.Clear();
            }

            return View(_product.GetProductList());
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Shop()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                System.Web.HttpContext.Current.Session.Clear();
            }

            return View(_product.GetProductList());
        }
        public ActionResult Favourites()
        {
            return View();
        }
        public ActionResult NewArrivals()
        {
            return View();
        }
        public ActionResult Sale()
        {
            return View();
        }
    }
}
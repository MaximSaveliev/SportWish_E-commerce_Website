﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAplication.Controllers
{
    public class ViewCartController : Controller
    {
        // GET: ViewCart
        public ActionResult Index()
        {
            return View();
        }
    }
}
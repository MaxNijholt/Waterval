﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Gandalf()
        {
            Response.StatusCode = 403;
            return View();
        }
	}
}
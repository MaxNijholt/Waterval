using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Module/
        static ModuleList modules = new ModuleList();
        public ActionResult Index()
        {
            FillList();
            var mCollection = modules;

            return View(mCollection);
        }

        public ActionResult ShowDetail(string modelname)
        {
            if (modelname == null || modules == null)
                return HttpNotFound();

            Modules model = null;

            foreach (Modules c in modules.mods)
            {
                if (c.Titel == modelname)
                {
                    model = c;
                    break;
                }
            }

            return View(model);
        }

        void FillList()
        {
            if (modules.mods.Count > 0) return;
            modules.mods.Add(new Modules("Project 5", "Project 5 Groepswerk", "12-56", "4 EC", "Groeps Werk", "Project 1-2", "Avans", "Leren in team te werken en waterval methodiek", "Tentamenvorm"));

            modules.mods.Add(new Modules("SWEN3", "Project 5 Groepswerk 12", "12-56-12", "14 EC", "Groeps Werk-12", "Project 1-2-12", "Avans-12", "Leren in team te werken en waterval methodiek-12", "Tentamenvorm-12"));
        }

        public ActionResult Create()
        {
     

            return View();
        }


        public ActionResult Edit()
        {
       
            return View();
        }

    }
}

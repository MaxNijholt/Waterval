using Waterval.Models;
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
        public ActionResult Index(int id = 0)
        {
            FillList(id);
            var mCollection = modules;

            return View(mCollection);
        }

        public ActionResult Details(string modelname)
        {
            if (modelname == null || modules == null)
                return HttpNotFound();

            Modules model = null;

            foreach (Modules c in modules.mods)
            {
                if (c.Vakcode == modelname)
                {
                    model = c;
                    break;
                }
            }

            return View(model);
        }

        void FillList(int id)
        {
            modules.mods.Clear();
            if (modules.mods.Count > 0) return;
            if (id != 1)
                modules.mods.Add(new Modules("Programeren 1", "Programen 1 voor beginners", "PR-01", "4 EC", "Huiswerk+Workshop", "1", "Workshop+Hoorcollege", "Basis java programeren", "Assigment", "Procesanalyse uitvoeren"));
            if (id != 2)
                modules.mods.Add(new Modules("Database 1", "Database start", "DB-01", "2 EC", "Huiswerk+Workshop", "1", "Workshop+Hoorcollege", "Basis Databse", "Opdracht", "Informatieanalyse uitvoeren"));
        }

        public ActionResult Create()
        {


            return View();
        }


        public ActionResult Edit(string modelname)
        {
            if (modelname == null || modules == null)
                return HttpNotFound();

            Modules model = null;

            foreach (Modules c in modules.mods)
            {
                if (c.Vakcode == modelname)
                {
                    model = c;
                    break;
                }
            }

            return View(model);

        }
    }
}

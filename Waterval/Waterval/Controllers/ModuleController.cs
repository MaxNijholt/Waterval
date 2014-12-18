using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Module/
        private ModuleRepository moduleRepository;

        public ModuleController()
        {
            moduleRepository = new ModuleRepository();
        }

       // static ModuleList modules = new ModuleList();

        public ActionResult Index()
        {
            return View(moduleRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Module module)
        {
            try
            {
                moduleRepository.Create(module);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(module);
            }
        }


        public ActionResult Details(int id)
        {
            Module module = moduleRepository.Get(id);
		       
            return View(module);
        }

        public ActionResult Delete(Module module)
        {

            moduleRepository.Delete(module.Module_ID);
            return View();
        }

        public ActionResult Edit(int id)
        {
            Module module = moduleRepository.Get(id);
           
            return View(module);
        }

        [HttpPost]
        public ActionResult Edit(int id, Module module)
        {
            try
            {
                if (moduleRepository.Update(module) == null)
                {
                    return View(module).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(module);
            }
        }

    }
}

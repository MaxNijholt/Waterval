using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class LearnLineController : Controller
    {
        //
        // GET: /LearnLine/
        private LearnLineRepository learnLineRepository;

        public LearnLineController()
        {
            learnLineRepository = new LearnLineRepository();
        }

        public ActionResult Index()
        {
            return View(learnLineRepository.GetAll());
        }

        public ActionResult Create()
        {
            LearnLine leer = new LearnLine();
            return View(leer);
        }

        [HttpPost]
        public ActionResult Create(LearnLine LearnLine)
        {
            try
            {

                learnLineRepository.Create(LearnLine);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(LearnLine);
            }
        }
        public ActionResult Delete(LearnLine learnLine)
        {

            learnLineRepository.Delete(learnLine.LearnLine_ID);
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = learnLineRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, LearnLine learnLine)
        {
            try
            {
                if (learnLineRepository.Update(learnLine) == null)
                {
                    return View(learnLine).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(learnLine);
            }
        }


        public ActionResult Details(int id)
        {
            LearnLine model = learnLineRepository.Get(id);
            return View(model);
        }

    }
}

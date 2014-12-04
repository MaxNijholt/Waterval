using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class CompetenceController : Controller
    {
        //
        // GET: /Compententie/
        private CompetenceRepository compenteceRepository;

        public CompetenceController()
        {
            compenteceRepository = new CompetenceRepository();
        }

        public ActionResult Index()
        {

            return View(compenteceRepository.GetAll());
        }

        public ActionResult Create()
        {
            Competence comp = new Competence();
            return View(comp);
        }

        [HttpPost]
        public ActionResult Create(Competence competence)
        {
            try
            {
       
                compenteceRepository.Create(competence);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(competence);
            }
        }
        public ActionResult Delete(Competence compentence)
        {

            compenteceRepository.Delete(compentence.Competence_ID);
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = compenteceRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, Competence compentence)
        {
         try
         {
             if(compenteceRepository.Update(compentence) == null)
             {
                 return View(compentence).ViewBag.Error = "Er is iets fout gegaan.";
             }
             return RedirectToAction("Index");
         }
            catch
         {
             return View(compentence);
         }
        }


        public ActionResult Details(int id)
        {
            Competence model = compenteceRepository.Get(id);
            return View(model);
        }

    }
}

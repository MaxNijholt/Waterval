using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers
{
    public class LearningToolController : Controller
    {
        //
        // GET: /LearningTool/
        LearningToolRepository learningtoolRepository;

        public LearningToolController()
        {
            learningtoolRepository = new LearningToolRepository();
        }
        public ActionResult Index()
        {
            return View(learningtoolRepository.GetAll());
        }

        public ActionResult Create()
        {
            var model = new LearningTool();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection fromcollection)
        {

            LearningTool model = new LearningTool();

            //Gives back al of the exsiting modules.
          
            try
            {
                //Fills the competence with the values of the form collection.
                //if the defination long is not filled we return the view so we can see the error message.
                model.Description = fromcollection["Description"];


                learningtoolRepository.Create(model);
             

                //We go back to the index.
                return RedirectToAction("Index");
            }
            catch
            {
                //Did something go wrong we return the view with the model.
                return View(model);
            }
        }

        public ActionResult Delete(LearningTool learningtool)
        {
            learningtoolRepository.Delete(learningtool.LearnTool_ID);
            return View();
        }


        public ActionResult Edit(int id)
        {
            var model = learningtoolRepository.Get(id);


            @ViewBag.NewID = newVersion(id);
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(int id, LearningTool model)
        {
            

            try
            {

                //if we update the model and somethign went wrong we send an error messge back
                if (learningtoolRepository.Update(model) == null)
                    return View(model).ViewBag.Error = "Er is iets fout gegaan.";

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }


        public ActionResult Details(int id)
        {
            LearningTool model = learningtoolRepository.Get(id);
            @ViewBag.NewID = newVersion(id);

            return View(model);
        }


        public ActionResult NewVersion(int id)
        {
            LearningTool learningtool = learningtoolRepository.Get(id);

            var model = new LearningTool();
            model.PrevLearnTool_ID = id;
            model.Description = learningtool.Description;
       

            @ViewBag.NewID = newVersion(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult NewVersion(int id, LearningTool model)
        {
     
            try
            {
                @ViewBag.NewID = 0;


                learningtoolRepository.Create(model);
                learningtoolRepository.Delete(id);

              
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
        private int newVersion(int id)
        {
            LearningTool newer = learningtoolRepository.GetNewVersion(id);
            return (newer != null) ? newer.LearnTool_ID : -1;
        }
    }
}


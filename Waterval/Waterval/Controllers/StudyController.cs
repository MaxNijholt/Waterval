using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Models;
using RepositoryModel;
using RepositoryModel.Repository;


namespace Waterval.Controllers
{
    public class StudyController : Controller
    {
        //
        // GET: /Study/
        private StudyRepository studyRepository;

        public StudyController()
        {
            studyRepository = new StudyRepository();
        }
        public ActionResult Index()
        {

            return View(studyRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            Study model = studyRepository.Get(id);
            return View(model);
        }

		[Authorize( Roles = "CreateStudy" )]
        public ActionResult Create()
        {
            Study study = new Study();
            return View(study);
        }
        [HttpPost]
        public ActionResult Create(Study study)
        {
            try
            {
                study.DeleteDate = DateTime.Now;
                studyRepository.Create(study);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(study);
            }
        }
		[Authorize( Roles = "EditStudy" )]
        public ActionResult Edit(int id)
        {
            var model = studyRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, Study study)
        {
            try
            {
                if (studyRepository.Update(study) == null)
                {
                    return View(study).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(study);
            }
        }

		[Authorize( Roles = "DeleteStudy" )]
        public ActionResult Delete(Study study)
        {

            studyRepository.Delete(study.Study_ID);
            return View();
        }
    }
}
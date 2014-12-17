using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers
{
    public class ProgramController : Controller
    {
        //
        // GET: /Program/
        private ProgramRepository ProgramRepository;

        public ProgramController()
        {
            ProgramRepository = new ProgramRepository();
        }
        public ActionResult Index()
        {
            return View(ProgramRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            Program model = ProgramRepository.Get(id);
            return View(model);
        }

        public ActionResult Create()
        {
            Program program = new Program();
            return View(program);
        }
        [HttpPost]
        public ActionResult Create(Program program)
        {
            try
            {
                program.DeleteDate = DateTime.Now;
                ProgramRepository.Create(program);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(program);
            }
        }
        public ActionResult Edit(int id)
        {
            var model = ProgramRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, Program program)
        {
            try
            {
                if (ProgramRepository.Update(program) == null)
                {
                    return View(program).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(program);
            }
        }

        [HttpPost]
        public ActionResult Delete(Program program)
        {

            ProgramRepository.Delete(program.Program_ID);
            return View();
        }
    }
}
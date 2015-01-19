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
        private ProgramRepository programRepository;
        private StudyRepository studyRepository;


        public ProgramController()
        {
            programRepository = new ProgramRepository();
            studyRepository = new StudyRepository();
        }
        public ActionResult Index()
        {
            return View(programRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            Program model = programRepository.Get(id);
            return View(model);
        }
        [Authorize(Roles = "CreateProgram")]
        public ActionResult Create()
        {

            Program program = new Program();
            @ViewBag.StudyList = GetStudys(program);


            return View(program);
        }
        [HttpPost]
        public ActionResult Create(Program program)
        {
            @ViewBag.StudyList = GetStudys(program);
            try
            {
                programRepository.Create(program);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(program);
            }
        }


        [Authorize(Roles = "EditProgram")]
        public ActionResult Edit(int id)
        {
            var model = programRepository.Get(id);
            @ViewBag.StudyList = getstudysThatAreEditedRight(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, Program program)
        {
            @ViewBag.StudyList = getstudysThatAreEditedRight(program);
            try
            {
                if (programRepository.Update(program) == null)
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

        private List<Study> getstudysThatAreEditedRight(Program program)
        {
            List<Study> temp = new List<Study>();
            List<Study> studys = studyRepository.GetAll();
            foreach (Study m in program.Study)
            {
                foreach (Study a in studys)
                {
                    if (m.Study_ID == a.Study_ID)
                    {
                        temp.Add(a);
                    }
                }
            }
            foreach (Study d in temp)
            {
                studys.Remove(d);
            }
            return studys;
        }


        [Authorize(Roles = "DeleteProgram")]
        public ActionResult Delete(Program program)
        {

            programRepository.Delete(program.Program_ID);
            return View();
        }
        private List<Study> GetStudys(Program program)
        {
            List<Study> studys = studyRepository.GetAll();

            /*    if (theme != null && theme.Module != null)
                    foreach (Module modul in theme.Module)
                        modules.Remove(modules.Where(b => b.Module_ID == modul.Module_ID).First());
                */
            return studys.Where(m => m.isDeleted == false).ToList();
        }

        private List<Study> GetStudysThatAreEditedRight(Program program)
        {
            List<Study> temp = new List<Study>();
            List<Study> studys = studyRepository.GetAll();
            foreach (Study m in program.Study)
            {
                foreach (Study a in studys)
                {
                    if (m.Study_ID == a.Study_ID)
                    {
                        temp.Add(a);
                    }
                }
            }
            foreach (Study d in temp)
            {
                studys.Remove(d);
            }
            return studys;
        }
    }
}
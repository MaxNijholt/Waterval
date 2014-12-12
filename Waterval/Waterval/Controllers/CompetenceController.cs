using DomainModel.Models;
using Newtonsoft.Json;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers
{
    public class CompetenceController : Controller
    {
        //
        // GET: /Compententie/
        private CompetenceRepository compenteceRepository;
        private ModuleRepository moduleRepository;

        public CompetenceController()
        {
            compenteceRepository = new CompetenceRepository();
            moduleRepository = new ModuleRepository();
        }

        public ActionResult Index()
        {

            return View(compenteceRepository.GetAll().Where(b => b.isDeleted == false));
        }

        public ActionResult Create()
        {
            Competence comp = new Competence();

            @ViewBag.ModuleList = moduleRepository.GetAll();
            @ViewBag.NewID = -1;
            return View(comp);
        }



        [ValidateInput(true), HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Competence comp = new Competence();

            @ViewBag.ModuleList = moduleRepository.GetAll();
            try
            {


                comp.Title = form["Title"];
                comp.Definition_Long = form["Definition_Long"];
                comp.Definition_Short = form["Definition_Short"];

                if (string.IsNullOrEmpty(comp.Definition_Long))
                    return View(comp);

                var id = compenteceRepository.Create(comp).Competence_ID;

                var mods = form["ModulesTogether"];
                if (!string.IsNullOrEmpty(mods))
                    foreach (string modid in mods.Split('-'))
                    {
                        var level = form["Level" + modid];
                        compenteceRepository.CompentenceAndModules(id, Convert.ToInt16(modid), level);
                    }


                return RedirectToAction("Index");
            }
            catch
            {
                return View(comp);
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

            @ViewBag.ModuleList = GetModules(model);
            @ViewBag.NewID = newVersion(id);
            return View(model);
        }

        [ValidateInput(true), HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            Competence competence = new Competence();
            try
            {
                competence.Competence_ID = Convert.ToInt16(form["Competence_ID"]);
                competence.Title = form["Title"];
                competence.Definition_Long = form["Definition_Long"];
                competence.Definition_Short = form["Definition_Short"];
                @ViewBag.ModuleList = GetModules(competence);

                if (string.IsNullOrEmpty(competence.Definition_Long))
                    return View(competence);

                if (compenteceRepository.Update(competence) == null)
                    return View(competence).ViewBag.Error = "Er is iets fout gegaan.";

                compenteceRepository.CompentenceAndModulesDelete(competence.Competence_ID);

                var mods = form["ModulesTogether"];
                if (!string.IsNullOrEmpty(mods))
                    foreach (string modid in mods.Split('-'))
                    {
                        var level = form["Level" + modid];
                        compenteceRepository.CompentenceAndModules(competence.Competence_ID, Convert.ToInt16(modid), level);
                    }

               
                @ViewBag.NewID = newVersion(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(competence);
            }
        }

        public ActionResult toNewCohort(int id)
        {
            Competence competence = compenteceRepository.Get(id);

            var model = new Competence();
            model.PrevCompetence_ID = id;
            model.Definition_Long = competence.Definition_Long;
            model.Title = competence.Title;
            model.Definition_Short = competence.Definition_Short;
            model.Level = competence.Level;

            @ViewBag.ModuleList = GetModules(competence);
            @ViewBag.NewID = newVersion(id);
            return View(model);
        }

        [ValidateInput(true), HttpPost]
        public ActionResult toNewCohort(int id, FormCollection form)
        {
            Competence competence = new Competence();
            try
            {
                @ViewBag.NewID = newVersion(id);

                competence.Title = form["Title"];
                competence.Definition_Long = form["Definition_Long"];
                competence.Definition_Short = form["Definition_Short"];

                if (string.IsNullOrEmpty(competence.Definition_Long))
                    return View(competence);

                var compid = compenteceRepository.Create(competence).Competence_ID;

                var mods = form["ModulesTogether"];

                if (!string.IsNullOrEmpty(mods))
                    foreach (string modid in mods.Split('-'))
                    {
                        var level = form["Level" + modid];
                        compenteceRepository.CompentenceAndModules(compid, Convert.ToInt16(modid), level);
                    }

                @ViewBag.ModuleList = GetModules(competence);

                compenteceRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(competence);
            }
        }

        public ActionResult Details(int id)
        {
            Competence model = compenteceRepository.Get(id);
            @ViewBag.NewID = newVersion(id);

            return View(model);
        }

        private List<Module> GetModules(Competence competence)
        {
            List<Module> modules = moduleRepository.GetAll();
            foreach (Level mo in competence.Level)
            {
                Module temp = modules.Where(b => b.Module_ID == mo.Module_ID).First();
                modules.Remove(temp);
            }

            return modules;
        }

        private int newVersion(int id)
        {
            Competence newer = compenteceRepository.GetNewVersion(id);
            return (newer != null) ? newer.Competence_ID : -1;
        }

    }
}

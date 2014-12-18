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
        // GET: /Compententie/
        private CompetenceRepository compenteceRepository;
        private ModuleRepository moduleRepository;

        /// <summary>
        /// Initialize this controller
        /// </summary>
        public CompetenceController()
        {
            compenteceRepository = new CompetenceRepository();
            moduleRepository = new ModuleRepository();
        }

        /// <summary>
        /// Return a view with a collection of the competence in the database that are not deleted.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(compenteceRepository.GetAll().Where(b => b.isDeleted == false));
        }

        /// <summary>
        //Creates a view with a competence that contains also of of the modules.
        //A NewID variable gets set on -1.
        //The view gets returned
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Competence comp = new Competence();

            @ViewBag.ModuleList = moduleRepository.GetAll();
            @ViewBag.NewID = -1;
            return View(comp);
        }

        /// <summary>
        /// This create gets called when a httpost get done on the form.
        /// </summary>
        /// <param name="form">The complete collection of al the form controllers</param>
        /// <returns></returns>
        [ValidateInput(true), HttpPost]
        public ActionResult Create(Competence comp)
        {

            //Gives back al of the exsiting modules.
            @ViewBag.ModuleList = moduleRepository.GetAll();
            try
            {
                //Fills the competence with the values of the form collection.
       

                if (string.IsNullOrEmpty(comp.Definition_Long))
                    return View(comp);

                //We create a new competence and we store the id of it in a variabele
                var id = compenteceRepository.Create(comp).Competence_ID;

                //From the textbox ModulesTogether we get the value. 
                //We split this value on - and loop through them all.
                //We save the combination of the Comptence, Module and Level.
                    foreach (var item in comp.Level)
                        compenteceRepository.CompentenceAndModules(id, item.Module_ID, item.Level1);

                //We go back to the index.
                return RedirectToAction("Index");
            }
            catch
            {
                //Did something go wrong we return the view with the model.
                return View(comp);
            }
        }

        /// <summary>
        //  Updates the competence to a delete state.
        /// </summary>
        /// <param name="compentence">The competence that needs to get deleted.</param>
        /// <returns>The view</returns>
        public ActionResult Delete(Competence compentence)
        {
            compenteceRepository.Delete(compentence.Competence_ID);
            return View();
        }
        
        /// <summary>
        /// Calls the create view with a model
        /// </summary>
        /// <param name="id">the id of the competence that needs to get edit.</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = compenteceRepository.Get(id);

            @ViewBag.ModuleList = GetModules(model);
            @ViewBag.NewID = newVersion(id);
            return View(model);
        }

        /// <summary>
        /// Gets called from a post of the edit view it edits an existing competence
        /// </summary>
        /// <param name="id">id of the competence</param>
        /// <param name="form">the complete form collection</param>
        /// <returns></returns>
        [ValidateInput(true), HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            Competence competence = new Competence();
            try
            {
                //Fills the id of the competence with the hidden value from competence id.
                competence.Competence_ID = Convert.ToInt16(form["Competence_ID"]);
                @ViewBag.NewID = newVersion(competence.Competence_ID);

                //Fills al the other values from competence with the form collection
                competence.Title = form["Title"];
                competence.Definition_Long = form["Definition_Long"];
                competence.Definition_Short = form["Definition_Short"];

                //Get a list of modules based on this competence. 
                @ViewBag.ModuleList = GetModules(competence);

                //if the defination is empty we return the view.
                if (string.IsNullOrEmpty(competence.Definition_Long))
                    return View(competence);

                //if we update the model and somethign went wrong we send an error messge back
                if (compenteceRepository.Update(competence) == null)
                    return View(competence).ViewBag.Error = "Er is iets fout gegaan.";

                //We delete all of the levels from this competence. 
                compenteceRepository.CompentenceAndModulesDelete(competence.Competence_ID);

                //From the textbox ModulesTogether we get the value. 
                //We split this value on - and loop through them all.
                //We save the combination of the Comptence, Module and Level.
                var mods = form["ModulesTogether"];
                if (!string.IsNullOrEmpty(mods))
                    foreach (string modid in mods.Split('-'))
                    {
                        var level = form["Level" + modid];
                        compenteceRepository.CompentenceAndModules(competence.Competence_ID, Convert.ToInt16(modid), level);
                    }           
                return RedirectToAction("Index");
            }
            catch
            {
                //if something went wrong we return the view with the model
                return View(competence);
            }
        }

        /// <summary>
        /// Opens a view for making a new version
        /// </summary>
        /// <param name="id">The id of the competence that we want to use as a base.</param>
        /// <returns></returns>
        public ActionResult toNewVersion(int id)
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
        public ActionResult toNewVersion(int id, FormCollection form)
        {
            Competence competence = new Competence();
            try
            {
                @ViewBag.NewID = newVersion(id);
            

                competence.Title = form["Title"];
                competence.Definition_Long = form["Definition_Long"];
                competence.Definition_Short = form["Definition_Short"];
                competence.PrevCompetence_ID = Convert.ToInt16(form["PrevCompetence_ID"]);

              

                if (string.IsNullOrEmpty(competence.Definition_Long))
                    return View(competence);

                var model = compenteceRepository.Create(competence);
              
                
                @ViewBag.ModuleList = GetModules(model);
             
                var mods = form["ModulesTogether"];

                if (!string.IsNullOrEmpty(mods))
                    foreach (string modid in mods.Split('-'))
                    {
                        var level = form["Level" + modid];
                        compenteceRepository.CompentenceAndModules(model.Competence_ID, Convert.ToInt16(modid), level);
                    }

           

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

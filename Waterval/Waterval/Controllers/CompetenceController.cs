using DomainModel.Models;
using Newtonsoft.Json;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Waterval.Controllers
{
    public class CompetenceController : Controller
    {
        // GET: /Compententie/
        private CompetenceRepository compenteceRepository;
		private ModuleRepository moduleRepository;
		private SearchRepository search;

        /// <summary>
        /// Initialize this controller
        /// </summary>
        public CompetenceController()
        {
            compenteceRepository = new CompetenceRepository();
			moduleRepository = new ModuleRepository( );
			search = new SearchRepository( );
        }

        /// <summary>
        /// Return a view with a collection of the competence in the database that are not deleted.
        /// </summary>
        /// <returns></returns>
		//public ActionResult Index()
		//{
		//	return View(compenteceRepository.GetAll().Where(b => b.isDeleted == false));
		//}

		public ActionResult Index ( string sortOrder, string currentFilter, string searchString, int? page, int pagesize = 10, bool isPDF =false) {
			ViewBag.CurrentSort = sortOrder;
			ViewBag.ResultAmount = pagesize;
			ViewBag.NameSortParm = String.IsNullOrEmpty( sortOrder ) ? "Titel" : "";

			if ( searchString != null ) {
				page = 1;
			} else {
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var competences = this.compenteceRepository.GetAll( ).Where( m => m.isDeleted == false );

            if (isPDF)
                pagesize = competences.Count();

			if ( !String.IsNullOrEmpty( searchString ) ) {
				competences = search.GetCompetencesWith( searchString );
			}
			switch ( sortOrder ) {
				case "Titel":
				competences = competences.OrderBy( b => b.Title ).ToList( );
				break;
				default:
				competences = competences.OrderByDescending( b => b.Title ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
            @ViewBag.isPDF = isPDF;
			return View( competences.ToPagedList( pageNumber, pageSize ) );
		}

        /// <summary>
        //Creates a view with a competence that contains also of of the modules.
        //A NewID variable gets set on -1.
        //The view gets returned
        /// </summary>
		/// <returns></returns>
		[Authorize( Roles = "CreateCompetence" )]
        public ActionResult Create()
        {
            Competence comp = new Competence();

            @ViewBag.ModuleList = GetModules(comp);
            @ViewBag.NewID = -1;
            return View(comp);
        }

        /// <summary>
        /// This create gets called when a httpost get done on the form.
        /// </summary>
        /// <param name="competence">The complete ccompetence</param>
        /// <returns></returns>
        [ValidateInput(true), HttpPost]
        public ActionResult Create(Competence comp)
        {

            //Gives back al of the exsiting modules.
            @ViewBag.ModuleList = GetModules(comp);


            try
            {
                if (string.IsNullOrEmpty(comp.Definition_Long))
                    return View(comp);

                //We create a new competence and we store the id of it in a variabele
                var id = compenteceRepository.Create(comp).Competence_ID;

                //We loop through all levels 
                //We save the combination of the Comptence, Module and Level.
                foreach (var item in comp.Level)
                    compenteceRepository.CompentenceAndModules(id, item.Module_ID, item.Level1);

                //We go back to the index.
                return RedirectToAction("Index");
            }
            catch
            {

                foreach (var item in comp.Level)
                    item.Module = moduleRepository.Get(item.Module_ID);

                //Did something go wrong we return the view with the model.
                return View(comp);
            }
        }

        /// <summary>
        //  Updates the competence to a delete state.
        /// </summary>
        /// <param name="compentence">The competence that needs to get deleted.</param>
		/// <returns>The view</returns>
		[Authorize( Roles = "DeleteCompetence" )]
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
		[Authorize( Roles = "EditCompetence" )]
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
        /// <param name="competence">the complete competence</param>
        /// <returns></returns>
        [ValidateInput(true), HttpPost]
        public ActionResult Edit(int id, Competence competence)
        {
            @ViewBag.ModuleList = GetModules(competence);

            foreach (var item in competence.Level)
                item.Module = moduleRepository.Get(item.Module_ID);
            try
            {
                //Fills the id of the competence with the hidden value from competence id.
                @ViewBag.NewID = newVersion(competence.Competence_ID);

                //Get a list of modules based on this competence. 
                @ViewBag.ModuleList = GetModules(competence).Where(m => m.isDeleted = false);

                //if the defination is empty we return the view.
                if (string.IsNullOrEmpty(competence.Definition_Long))
                    return View(competence);

                //if we update the model and somethign went wrong we send an error messge back
                if (compenteceRepository.Update(competence) == null)
                    return View(competence).ViewBag.Error = "Er is iets fout gegaan.";

                //We delete all of the levels from this competence. 
                compenteceRepository.CompentenceAndModulesDelete(competence.Competence_ID);


                foreach (var item in competence.Level)
                    compenteceRepository.CompentenceAndModules(id, item.Module_ID, item.Level1);

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
		[Authorize( Roles = "toNewVersionCompetence" )]
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
        public ActionResult toNewVersion(int id, Competence competence)
        {
            @ViewBag.ModuleList = GetModules(competence);

         
            try
            {
                @ViewBag.NewID = newVersion(id);

                if (string.IsNullOrEmpty(competence.Definition_Long))
                    return View(competence);

                int newestID = compenteceRepository.Create(competence).Competence_ID;

                foreach (var item in competence.Level)
                    compenteceRepository.CompentenceAndModules(newestID, item.Module_ID, item.Level1);

                compenteceRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                foreach (var item in competence.Level)
                    item.Module = moduleRepository.Get(item.Module_ID);

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

            if (competence != null)
                foreach (Level lvl in competence.Level)
                    modules.Remove(modules.Where(b => b.Module_ID == lvl.Module_ID).First());

            return modules.Where(m => m.isDeleted == false).ToList();
        }

		[Authorize( Roles = "toNewVersionCompetence" )]
        private int newVersion(int id)
        {
            Competence newer = compenteceRepository.GetNewVersion(id);
            return (newer != null) ? newer.Competence_ID : -1;
        }

        public ActionResult GeneratePDF(int id)
        {
            return new Rotativa.ActionAsPdf("Details", new {id = id });
        }

        public ActionResult GeneratePDFList()
        {
            return new Rotativa.ActionAsPdf("Index", new { isPDF = true });
        }

    }
}

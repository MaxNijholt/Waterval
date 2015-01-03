using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MvcApplication1.Controllers
{
    public class ModuleController : Controller
    {
        //
        // GET: /Module/
		private ModuleRepository moduleRepository;
		private SearchRepository search;

        private LearnLineRepository learnLineRepository;
        private WorkformRepository workformRepository;


        public ModuleController()
        {
			moduleRepository = new ModuleRepository( );
			search = new SearchRepository( );
            learnLineRepository = new LearnLineRepository();
            workformRepository = new WorkformRepository();

        }
		
		public ActionResult Index ( string sortOrder, string currentFilter, string searchString, int? page, int pagesize = 10 ) {
			ViewBag.CurrentSort = sortOrder;
			ViewBag.ResultAmount = pagesize;
			ViewBag.NameSortParm = String.IsNullOrEmpty( sortOrder ) ? "Title" : "";

			if ( searchString != null ) {
				page = 1;
			} else {
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var modules = moduleRepository.GetAll( );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				modules = search.GetModulesWith( searchString );
			}
			switch ( sortOrder ) {
				case "Title":
				modules = modules.OrderBy( b => b.Title ).ToList( );
				break;
				case "Vakcode":
				modules = modules.OrderBy( b => b.CourseCode ).ToList( );
				break;
				case "EC":
				modules = modules.OrderBy( b => b.AssignmentCode.Sum( s => s.EC ) ).ToList( );
				break;
				case "Ingangsniveau":
				modules = modules.OrderBy( b => b.Entry_Level ).ToList( );
				break;
				default:
				modules = modules.OrderByDescending( b => b.Title ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
			return View( modules.ToPagedList( pageNumber, pageSize ) );
		}


        public ActionResult Create()
        {
            Module module = new Module();

            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.WorkformList = GetWorkForms(module);

            return View(module);
        }

        [ValidateInput(true), HttpPost]
        public ActionResult Create(Module module)
        {
            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);

            try
            {
                //We create a new competence and we store the id of it in a variabele
                var id = moduleRepository.Create(module).Module_ID;

                //We loop through all levels 
                //We save the combination of the Comptence, Module and Level.
                foreach (var item in module.ModelWithWorkform)
                    moduleRepository.ModulesAndWorkForm(id, item.Workform_ID);
                foreach (var item in module.GradeType)
                    moduleRepository.ModulesAndGradeTypes(id);
                //We go back to the index.
                return RedirectToAction("Index");
            }
            catch
            {

                foreach (var item in module.ModelWithWorkform)
                    item.Workform = workformRepository.Get(item.Workform_ID);
                foreach (var item in module.GradeType)
                    item.Module = moduleRepository.Get(item.Module_ID);

                //Did something go wrong we return the view with the model.
                return View(module);
            }



            //try
            //{
            //    moduleRepository.Create(module);
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View(module);
            //}
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

            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);

           
            return View(module);
        }

        [HttpPost]
        public ActionResult Edit(int id, Module module)
        {


            @ViewBag.LearnLineList = GetLearnLines(module);
            @ViewBag.WorkformList = GetWorkForms(module);
            @ViewBag.GradeTypes = GetGradeTypes(module);

            foreach (var item in module.ModelWithWorkform)
                item.Workform = workformRepository.Get(item.Workform_ID);

            try
            {
                //Get a list of modules based on this learnline, workform, gradetype. 
                @ViewBag.LearnLineList = GetLearnLines(module).Where(m => m.isDeleted = false);
                @ViewBag.WorkformList = GetWorkForms(module).Where(m => m.isDeleted = false);
                @ViewBag.GradeTypes = GetGradeTypes(module).Where(m => m.isDeleted = false);

                //if we update the model and somethign went wrong we send an error messge back
                if (moduleRepository.Update(module) == null)
                    return View(module).ViewBag.Error = "Er is iets fout gegaan.";

                //We delete all of the levels from this competence. 
                moduleRepository.ModulesAndWorkFormDelete(module.Module_ID);


                foreach (var item in module.ModelWithWorkform)
                    moduleRepository.ModulesAndWorkForm(id, item.Workform_ID);

                return RedirectToAction("Index");
            }
            catch
            {
                //if something went wrong we return the view with the model
                return View(module);
            }

        }

        private List<LearnLine> GetLearnLines(Module module)
        {
            List<LearnLine> learnLines = learnLineRepository.GetAll();

            if (module != null)
                foreach (LearnLine lnl in module.LearnLine)
                    learnLines.Remove(learnLines.Where(b => b.LearnLine_ID == lnl.LearnLine_ID).First());

            return learnLines.Where(m => m.isDeleted == false).ToList();
        }

        private List<Workform> GetWorkForms(Module module)
        {
            List<Workform> workforms = workformRepository.GetAll();

            if (module != null)
                foreach (ModelWithWorkform mwf in module.ModelWithWorkform)
                    workforms.Remove(workforms.Where(b => b.Workform_ID == mwf.Workform_ID).First());

            return workforms.Where(m => m.isDeleted == false).ToList();
        }

        private List<Module> GetGradeTypes(Module module)
        {
            List<Module> gradetypes = moduleRepository.GetAll();

            if (module != null)
                foreach (GradeType mwf in module.GradeType)
                    gradetypes.Remove(gradetypes.Where(b => b.Module_ID == mwf.Module_ID).First());

            return gradetypes.Where(m => m.isDeleted == false).ToList();
        }

    }
}

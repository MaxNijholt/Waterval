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

        public ModuleController()
        {
			moduleRepository = new ModuleRepository( );
			search = new SearchRepository( );
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(Module module)
        {
            try
            {
                moduleRepository.Create(module);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(module);
            }
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
           
            return View(module);
        }

        [HttpPost]
        public ActionResult Edit(int id, Module module)
        {
            try
            {
                if (moduleRepository.Update(module) == null)
                {
                    return View(module).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(module);
            }
        }

    }
}

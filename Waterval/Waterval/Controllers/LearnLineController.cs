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
    public class LearnLineController : Controller
    {
        //
        // GET: /LearnLine/
		private LearnLineRepository learnLineRepository;
        private ModuleRepository moduleRepository;
		private SearchRepository search;

        public LearnLineController()
        {
			learnLineRepository = new LearnLineRepository( );
            moduleRepository = new ModuleRepository();
            search = new SearchRepository();
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

			var learnLines = learnLineRepository.GetAll( ).Where( m => m.isDeleted == false );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				learnLines = search.GetLearnLinesWith( searchString );
			}
			switch ( sortOrder ) {
				case "Title":
				learnLines = learnLines.OrderBy( b => b.Title ).ToList( );
				break;
				default:
				learnLines = learnLines.OrderByDescending( b => b.Title ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
			return View( learnLines.ToPagedList( pageNumber, pageSize ) );
		}

		[Authorize( Roles = "CreateLearnLine" )]
        public ActionResult Create()
        {
            LearnLine leer = new LearnLine();
            return View(leer);
        }

        [HttpPost]
        public ActionResult Create(LearnLine LearnLine)
        {
            try
            {

                learnLineRepository.Create(LearnLine);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(LearnLine);
            }
        }
		[Authorize( Roles = "DeleteLearnLine" )]
        public ActionResult Delete(LearnLine learnLine)
        {

            learnLineRepository.Delete(learnLine.LearnLine_ID);
            return View();
        }
		[Authorize( Roles = "EditLearnLine" )]
        public ActionResult Edit(int id)
        {
            var model = learnLineRepository.Get(id);
            return View(model);
        }
        private List<Module> GetModules(LearnLine learnline)
        {
            List<Module> modules = moduleRepository.GetAll();

            return modules.Where(m => m.isDeleted == false).ToList();
        }

        [HttpPost]
        public ActionResult Edit(int id, LearnLine learnLine)
        {
            try
            {
                if (learnLineRepository.Update(learnLine) == null)
                {
                    return View(learnLine).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(learnLine);
            }
        }
        [Authorize(Roles = "toNewVersionLearnLine")]
        public ActionResult toNewVersion(int id)
        {
            LearnLine learnLine = learnLineRepository.Get(id);

            var model = new LearnLine();
            model.PrevLearnLine_ID = id;
            model.Definition = learnLine.Definition;

            return View(model);
        }

        [ValidateInput(true), HttpPost]
        public ActionResult toNewVersion(int id, LearnLine learnLine)
        {

            try
            {
                @ViewBag.NewID = newVersion(id);

                if (string.IsNullOrEmpty(learnLine.Definition))
                    return View(learnLine);

                int newestID = learnLineRepository.Create(learnLine).LearnLine_ID;

                learnLineRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(learnLine);
            }
        }

        [Authorize(Roles = "toNewVersionCompetence")]
        private int newVersion(int id)
        {
            LearnLine newer = learnLineRepository.GetNewVersion(id);
            return (newer != null) ? newer.LearnLine_ID : -1;
        }

        public ActionResult Details(int id)
        {
            LearnLine model = learnLineRepository.Get(id);
            return View(model);
        }

    }
}

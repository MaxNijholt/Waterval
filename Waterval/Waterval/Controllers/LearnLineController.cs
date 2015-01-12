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
		private SearchRepository search;

        public LearnLineController()
        {
			learnLineRepository = new LearnLineRepository( );
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

			var learnLines = learnLineRepository.GetAll( );
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
        public ActionResult Delete(LearnLine learnLine)
        {

            learnLineRepository.Delete(learnLine.LearnLine_ID);
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = learnLineRepository.Get(id);
            return View(model);
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


        public ActionResult Details(int id)
        {
            LearnLine model = learnLineRepository.Get(id);
            return View(model);
        }

    }
}

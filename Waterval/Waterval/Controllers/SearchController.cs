using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryModel.Repository;
using DomainModel.Models;
using Waterval.Models;

namespace Waterval.Controllers
{
    public class SearchController : Controller
    {
		private SearchRepository searchRepo = new SearchRepository();

        // GET: Search
        public ActionResult Index() {
			DomainModel.Models.Project_WatervalEntities dbContext = new DomainModel.Models.Project_WatervalEntities( );
			ViewBag.LearnLine = new SelectList( dbContext.LearnLine, "LearnLine_ID", "Title" );
			ViewBag.Theme = new SelectList( dbContext.Theme, "Theme_ID", "Title" );
			ViewBag.Competence = new SelectList( dbContext.Competence, "Competence_ID", "Title" );
			return View();
        }

		public ActionResult SearchResults ( String SearchText) {
            SearchLists sl = new SearchLists()
            {
                Blocks = searchRepo.GetBlocksWith(SearchText),
                LearnLines = searchRepo.GetLearnLinesWith(SearchText),
                Modules = searchRepo.GetModulesWith(SearchText),
                Themes = searchRepo.GetThemesWith(SearchText),
                Compentences = searchRepo.GetCompetencesWith(SearchText),
                Studies = searchRepo.GetStudiesWith(SearchText)
            };
            return View(sl);
		}
    }
}
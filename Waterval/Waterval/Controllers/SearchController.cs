using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
		[ChildActionOnly]
        public ActionResult Index() {
			DomainModel.Models.Project_WatervalEntities dbContext = new DomainModel.Models.Project_WatervalEntities( );
			ViewBag.LearnLine = new SelectList( dbContext.LearnLine, "LearnLine_ID", "Title" );
			ViewBag.Theme = new SelectList( dbContext.Theme, "Theme_ID", "Title" );
			ViewBag.Competence = new SelectList( dbContext.Competence, "Competence_ID", "Title" );
			return View( "~/Views/Shared/_Search.cshtml" );
        }
    }
}
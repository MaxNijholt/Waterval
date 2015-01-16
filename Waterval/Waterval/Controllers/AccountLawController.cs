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
    public class AccountLawController : Controller
    {
        //
        // GET: /LearnLine/
		private AccountLawRepository accountLawRepository;
        private ModuleRepository moduleRepository;
		private SearchRepository search;

        public AccountLawController()
        {
			accountLawRepository = new AccountLawRepository( );
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

			var accountLaws = accountLawRepository.GetAll( );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				accountLaws = search.GetAccountLawsWith( searchString );
			}
			switch ( sortOrder ) {
				case "Title":
				accountLaws = accountLaws.OrderBy( b => b.LawName ).ToList( );
				break;
				default:
				accountLaws = accountLaws.OrderByDescending( b => b.LawName ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
			return View( accountLaws.ToPagedList( pageNumber, pageSize ) );
		}


        public ActionResult Details(int id)
        {
            AccountLaw model = accountLawRepository.Get(id);
            return View(model);
        }

    }
}

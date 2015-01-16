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
    public class AccountController : Controller
    {
        //
        // GET: /LearnLine/
		private AccountRepository accountRepository;
        private ModuleRepository moduleRepository;
		private SearchRepository search;

        public AccountController()
        {
			accountRepository = new AccountRepository( );
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

			var accounts = accountRepository.GetAll( ).Where( m => m.isActive == true );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				accounts = search.GetAccountsWith( searchString );
			}
			switch ( sortOrder ) {
				case "Title":
				accounts = accounts.OrderBy( b => b.Username ).ToList( );
				break;
				default:
				accounts = accounts.OrderByDescending( b => b.Username ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
			return View( accounts.ToPagedList( pageNumber, pageSize ) );
		}

        public ActionResult Delete(Account account)
        {

            accountRepository.Delete(account);
            return View();
        }

        private List<Module> GetModules(LearnLine learnline)
        {
            List<Module> modules = moduleRepository.GetAll();

            return modules.Where(m => m.isDeleted == false).ToList();
        }

        public ActionResult Details(int id)
        {
            Account model = accountRepository.Get(id);
            return View(model);
        }

    }
}

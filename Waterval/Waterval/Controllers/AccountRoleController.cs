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
    public class AccountRoleController : Controller
    {
        //
        // GET: /LearnLine/
		private AccountRoleRepository accountRoleRepository;
        private AccountLawRepository accountLawRepository;
		private SearchRepository search;

        public AccountRoleController()
        {
			accountRoleRepository = new AccountRoleRepository( );
            accountLawRepository = new AccountLawRepository();
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

			var accountRoles = accountRoleRepository.GetAll( );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				accountRoles = search.GetAccountRolesWith( searchString );
			}
			switch ( sortOrder ) {
				case "Title":
				accountRoles = accountRoles.OrderBy( b => b.RoleName ).ToList( );
				break;
				default:
				accountRoles = accountRoles.OrderByDescending( b => b.RoleName ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
			return View( accountRoles.ToPagedList( pageNumber, pageSize ) );
		}

        [Authorize(Roles="DeleteAccountRole")]
        public ActionResult Delete(AccountRole accountRole)
        {

            accountRoleRepository.Delete(accountRole);
            return View();
        }

        private List<AccountLaw> GetLaws(AccountRole role)
        {
            List<AccountLaw> laws = accountLawRepository.GetAll();

            return laws.ToList();
        }

        public ActionResult Details(int id)
        {
            AccountRole model = accountRoleRepository.Get(id);
            return View(model);
        }

        [Authorize(Roles = "CreateAccountRole")]
        public ActionResult Create()
        {
            
            AccountRole role = new AccountRole();
            @ViewBag.LawList = GetLaws(role);


            return View(role);
        }
        [HttpPost]
        public ActionResult Create(AccountRole role)
        {
            @ViewBag.LawList = GetLaws(role);
            try
            {
                accountRoleRepository.Create(role);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(role);
            }
        }


        [Authorize(Roles = "EditAccountRole")]
        public ActionResult Edit(int id)
        {
            var model = accountRoleRepository.Get(id);
            @ViewBag.LawList = GetlawsThatAreEditedRight(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, AccountRole role)
        {
            @ViewBag.LawList = GetlawsThatAreEditedRight(role);
            try
            {
                if (accountRoleRepository.Update(role) == null)
                {
                    return View(role).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(role);
            }
        }

        private List<AccountLaw> GetlawsThatAreEditedRight(AccountRole role)
        {
            List<AccountLaw> temp = new List<AccountLaw>();
            List<AccountLaw> laws = accountLawRepository.GetAll();
            foreach (AccountLaw m in role.AccountLaw)
            {
                foreach (AccountLaw a in laws)
                {
                    if (m.Law_ID == a.Law_ID)
                    {
                        temp.Add(a);
                    }
                }
            }
            foreach (AccountLaw d in temp)
            {
                laws.Remove(d);
            }
            return laws;
        }

    }
}

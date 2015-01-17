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
		private SearchRepository search;

        public AccountController()
        {
			accountRepository = new AccountRepository( );
            search = new SearchRepository();
        }

		public ActionResult Index () {

			return View(accountRepository.GetAll());
		}

        public ActionResult Details(int id)
        {
            Account model = accountRepository.GetAll().SingleOrDefault(m => m.Account_ID == id);
            return View(model);
        }

    }
}

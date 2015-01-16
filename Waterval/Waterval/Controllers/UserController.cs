using DomainModel.Models;
using Newtonsoft.Json.Linq;
using RepositoryModel.IRepository;
using RepositoryModel.Repository;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers {
    public class UserController : Controller {
        private IAccountRepository _accountRepository;

        public UserController()
        {
            _accountRepository = new AccountRepository();
        }

        [Authorize(Roles="EditAccount")]
        public ActionResult Index() {
            List<Account> accounts = _accountRepository.GetAll();
            return View(accounts);
        }

        public ActionResult Details()
        {
            List<Account> accounts = _accountRepository.GetAll();
            foreach (Account a in accounts) {
                if (a.Username.Equals(User.Identity.Name)) {
                    List<string> temp = new List<string>();
                    foreach (AccountLaw al in a.AccountRole.AccountLaw) {
                        temp.Add(al.LawName);
                    }
                    ViewBag.Actions = temp;
                    return View(a);
                }
            }
            return View();
        }

        [Authorize(Roles = "EditAccount")]
        public ActionResult Edit(int id)
        {
            ViewBag.Roles = _accountRepository.GetAllAccountRoles();
            Account a = _accountRepository.GetById(id);
            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(int id, Account account, int chosenRole)
        {
            ViewBag.Roles = _accountRepository.GetAllAccountRoles();
            account.AccountRole = _accountRepository.GetAccountRoleById(chosenRole);
            _accountRepository.Update(account);
            return RedirectToAction("Index");
        }

    }
}
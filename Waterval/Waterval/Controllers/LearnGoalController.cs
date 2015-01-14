using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers
{
    public class LearnGoalController : Controller
    {
        //
        // GET: /LearnGoal/
        private LearnGoalRepository learnGoalRepository;

        public LearnGoalController()
        {
            learnGoalRepository = new LearnGoalRepository();
        }

        public ActionResult Index()
        {
            return View(learnGoalRepository.GetAll().Where(x => x.isDeleted == false));
        }

        public ActionResult Create()
        {
            LearnGoal leer = new LearnGoal();
            return View(leer);
        }

        [HttpPost]
        public ActionResult Create(LearnGoal LearnGoal)
        {
            try
            {

                learnGoalRepository.Create(LearnGoal);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(LearnGoal);
            }
        }
        public ActionResult Delete(LearnGoal learnGoal)
        {

            learnGoalRepository.Delete(learnGoal.LearnGoal_ID);
            return View();
        }
        public ActionResult Edit(int id)
        {
            var model = learnGoalRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, LearnGoal learnGoal)
        {
            try
            {
                if (learnGoalRepository.Update(learnGoal) == null)
                {
                    return View(learnGoal).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(learnGoal);
            }
        }


        public ActionResult Details(int id)
        {
            LearnGoal model = learnGoalRepository.Get(id);
            return View(model);
        }

        private int newVersion(int id)
        {
            LearnGoal newer = learnGoalRepository.GetNewVersion(id);
            return (newer != null) ? newer.LearnGoal_ID : -1;
        }

        public ActionResult ToNewVersion(int id)
        {
            LearnGoal learngoal = learnGoalRepository.Get(id);

            var model = new LearnGoal();
            model.PrevLearnGoal_ID = id;
            model.Description = learngoal.Description;

            @ViewBag.NewID = newVersion(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ToNewVersion(int id, LearnGoal learnGoal)
        {
            try
            {
                learnGoal.PrevLearnGoal_ID = id;
                if (learnGoalRepository.Create(learnGoal) == null)
                {
                    return View(learnGoal).ViewBag.Error = "Er is iets fout gegaan.";
                }
                learnGoalRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(learnGoal);
            }
        }

    }
}
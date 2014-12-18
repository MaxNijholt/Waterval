using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryModel.Repository;
using DomainModel.Models;

namespace Waterval.Controllers
{
	public class PhasingController : Controller
	{
		// GET: /Phasing/
		private PhasingRepository phasingRepository;
		public PhasingController()
		{
			phasingRepository = new PhasingRepository();
		}
		public ActionResult Index()
		{
			return View(phasingRepository.GetAll());
		}

		[HttpGet]
		public ActionResult Create()
		{
			Phasing phasing = new Phasing();
			return View(phasing);
		}
		[HttpPost]
		public ActionResult Create(Phasing phasing)
		{
			try
			{
				phasingRepository.Create(phasing);
				return RedirectToAction("Index");
			}
			catch
			{
				return View(phasing);
			}
		}

		public ActionResult Delete(Phasing phasing)
		{
			phasingRepository.Delete(phasing.Phasing_ID);
			return View();
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			var model = phasingRepository.Get(id);
			return View(model);
		}
		[HttpPost]
		public ActionResult Edit(int id, Phasing phasing)
		{
			try
			{
				if(phasingRepository.Update(phasing) == null)
				{
					return View(phasing).ViewBag.Error = "Er is iets fout gegaan.";
				}
				return RedirectToAction("Index");
			}
			catch
			{
				return View(phasing);
			}
		}

		public ActionResult Details(int id)
		{
			Phasing model = phasingRepository.Get(id);
			return View(model);
		}
	}
}
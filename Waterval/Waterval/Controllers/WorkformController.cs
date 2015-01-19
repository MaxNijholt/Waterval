using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waterval.Controllers
{
	public class WorkformController : Controller
	{
		//
		// GET: /Workform/
		WorkformRepository workformRepository;

		public WorkformController()
		{
			workformRepository = new WorkformRepository();
		}

		public ActionResult Index()
		{
			return View(workformRepository.GetAll().Where(w => w.isDeleted == false));
		}

		[Authorize(Roles = "CreateWorkform")]
		public ActionResult Create()
		{
			var model = new Workform();
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(Workform model)
		{
			try
			{
				workformRepository.Create(model);
				//We go back to the index.
				return RedirectToAction("Index");
			}
			catch
			{
				//Did something go wrong we return the view with the model.
				return View(model);
			}
		}
		[Authorize(Roles = "EditWorkform")]
		public ActionResult Edit(int id)
		{
			var model = workformRepository.Get(id);
			@ViewBag.NewID = newVersion(id);

			//   @ViewBag.NewID = newVersion(id);
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(int id, Workform model)
		{
			try
			{
				@ViewBag.NewID = newVersion(id);
				//if we update the model and somethign went wrong we send an error messge back
				if(workformRepository.Update(model) == null)
					return View(model).ViewBag.Error = "Er is iets fout gegaan.";

				return RedirectToAction("Index");
			}
			catch
			{
				return View(model);
			}
		}

		public ActionResult Details(int id)
		{
			@ViewBag.NewID = newVersion(id);
			return View(workformRepository.Get(id));
		}
		[Authorize(Roles = "DeleteWorkform")]
		public ActionResult Delete(Workform model)
		{
			workformRepository.Delete(model.Workform_ID);
			return RedirectToAction("Index");
		}

		private int newVersion(int id)
		{
			Workform newer = workformRepository.GetNewVersion(id);
			return (newer != null) ? newer.Workform_ID : -1;
		}

		[Authorize(Roles = "toNewVersionWorkform")]
		public ActionResult toNewVersion(int id)
		{
			Workform workform = workformRepository.Get(id);

			var model = new Workform();
			model.PrevWorkform_ID = id;
			model.Description = workform.Description;

			@ViewBag.NewID = newVersion(id);
			return View(model);
		}

		[HttpPost]
		public ActionResult toNewVersion(int id, Workform model)
		{
			try
			{
				@ViewBag.NewID = 0;

				workformRepository.Create(model);
				workformRepository.Delete(id);

				return RedirectToAction("Index");
			}
			catch
			{
				return View(model);
			}
		}
	}
}
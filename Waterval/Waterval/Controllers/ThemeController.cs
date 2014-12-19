using DomainModel.Models;
using RepositoryModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Waterval.Controllers
{
	public class ThemeController : Controller
	{
		private ThemeRepository themeRepository;
		public ThemeController()
		{
			themeRepository = new ThemeRepository();
		}
		//public ActionResult Index()
		//{
		//	return View(themeRepository.GetAll());
		//}

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

			var themes = themeRepository.GetAll( );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				themes = themes.Where( b => b.Title.Contains( searchString ) ).ToList( );
				// TODO: change this to the searchRepo!
			}
			switch ( sortOrder ) {
				case "Title":
				themes = themes.OrderBy( b => b.Title ).ToList( );
				break;
				default:
				themes = themes.OrderByDescending( b => b.Title ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
			return View( themes.ToPagedList( pageNumber, pageSize ) );
		}

		public ActionResult Details(int id)
		{
			Theme theme = themeRepository.Get(id);
			return View(theme);
		}

		public ActionResult Create()
		{
			Theme theme = new Theme();
			return View(theme);
		}
		[HttpPost]
		public ActionResult Create(Theme theme)
		{
			try
			{
				themeRepository.Create(theme);
				return RedirectToAction("Index");
			}
			catch
			{
				return View(theme);
			}
		}

		public ActionResult Edit(int id)
		{
			var model = themeRepository.Get(id);
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(int id, Theme theme)
		{
			try
			{
				if(themeRepository.Update(theme) == null)
				{
					return View(theme).ViewBag.Error = "Er is iets fout gegaan.";
				}
				return RedirectToAction("Index");
			}
			catch
			{
				return View(theme);
			}
		}

		public ActionResult Delete(Theme theme)
		{
			themeRepository.Delete(theme.Theme_ID);
			return View();
		}
	}
}

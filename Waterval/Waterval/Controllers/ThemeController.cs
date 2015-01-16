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
		private SearchRepository search;
        private ModuleRepository moduleRepository;
		public ThemeController()
		{
			themeRepository = new ThemeRepository();
            moduleRepository = new ModuleRepository();
			search = new SearchRepository( );
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

			var themes = themeRepository.GetAll( ).Where( m => m.isDeleted == false );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				themes = search.GetThemesWith( searchString );
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

		[Authorize( Roles = "CreateTheme" )]
		public ActionResult Create()
		{
			Theme theme = new Theme();
            @ViewBag.ModuleList = GetModules(theme);


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



		[Authorize( Roles = "EditTheme" )]
		public ActionResult Edit(int id)
		{
			var model = themeRepository.Get(id);
            @ViewBag.ModuleList = GetModules(model);
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(int id, Theme theme)
		{
            @ViewBag.ModuleList = GetModules(theme);
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

		[Authorize( Roles = "DeleteTheme" )]
		public ActionResult Delete(Theme theme)
		{
			themeRepository.Delete(theme.Theme_ID);
			return View();
		}

        private List<Module> GetModules(Theme theme)
        {
            List<Module> modules = moduleRepository.GetAll();

        /*    if (theme != null && theme.Module != null)
                foreach (Module modul in theme.Module)
                    modules.Remove(modules.Where(b => b.Module_ID == modul.Module_ID).First());
            */
            return modules.Where(m => m.isDeleted == false).ToList();
        }
	}
}

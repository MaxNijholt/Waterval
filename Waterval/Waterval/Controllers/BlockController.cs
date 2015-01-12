using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Models;
using RepositoryModel;
using PagedList;
using RepositoryModel.Repository;


namespace Waterval.Controllers {
	public class BlockController : Controller {
		//
		// GET: /Block/
		private BlockRepository BlockRepository;
		private SearchRepository search;

		public BlockController ( ) {
			BlockRepository = new BlockRepository( );
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

			var blocks = BlockRepository.GetAll( );
			if ( !String.IsNullOrEmpty( searchString ) ) {
				blocks = search.GetBlocksWith( searchString );
			}
			switch ( sortOrder ) {
				case "Title":
				blocks = blocks.OrderBy( b => b.Title ).ToList( );
				break;
				default:
				blocks = blocks.OrderByDescending( b => b.Title ).ToList( );
				break;
			}
			int pageSize = pagesize;
			int pageNumber = ( page ?? 1 );
			return View( blocks.ToPagedList( pageNumber, pageSize ) );
		}

		public ActionResult Details ( int id ) {
			Block model = BlockRepository.Get( id );
			return View( model );
		}

		public ActionResult Create ( ) {
			Block block = new Block( );
			return View( block );
		}
		[HttpPost]
		public ActionResult Create ( Block block ) {
			try {
				block.DeleteDate = DateTime.Now;
				BlockRepository.Create( block );

				return RedirectToAction( "Index" );
			}
			catch {
				return View( block );
			}
		}
		public ActionResult Edit ( int id ) {
			var model = BlockRepository.Get( id );
			return View( model );
		}

		[HttpPost]
		public ActionResult Edit ( int id, Block block ) {
			try {
				if ( BlockRepository.Update( block ) == null ) {
					return View( block ).ViewBag.Error = "Er is iets fout gegaan.";
				}
				return RedirectToAction( "Index" );
			}
			catch {
				return View( block );
			}
		}

		[HttpPost]
		public ActionResult Delete ( Block blok ) {

			BlockRepository.Delete( blok.Block_ID );
			return View( );
		}
	}
}
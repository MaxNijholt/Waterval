using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Waterval.EntityRepos;
using Waterval.Models;
namespace Waterval.Controllers
{
    public class BlokController : Controller
    {
        //
        // GET: /Block/
        private BlockRepository BlockRepository;

        public BlokController()
        {
              BlockRepository = new BlockRepository();
        }
        public ActionResult Index()
        {
          
            return View(BlockRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            Block model = BlockRepository.Get(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = BlockRepository.Get(id);
            return View(model);
        }

           [HttpPost]
        public ActionResult Edit(int id, Block block)
        {
            return View();
        }


        public ActionResult Delete()
        {
            return View();
        }
	}
}
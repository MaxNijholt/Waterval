using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Models;
using RepositoryModel.Repository;


namespace Waterval.Controllers
{
    public class BlockController : Controller
    {
        //
        // GET: /Block/
        private BlockRepository BlockRepository;

        public BlockController()
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
            Block block = new Block();
            return View(block);
        }
        [HttpPost]
        public ActionResult Create(Block block)
        {
            try
            {
                block.DeleteDate = DateTime.Now;
                BlockRepository.Create(block);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(block);
            }
        }
        public ActionResult Edit(int id)
        {
            var model = BlockRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, Block block)
        {
            try
            {
                if (BlockRepository.Update(block) == null)
                {
                    return View(block).ViewBag.Error = "Er is iets fout gegaan.";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(block);
            }
        }

        [HttpPost]
        public ActionResult Delete(Block blok)
        {

            BlockRepository.Delete(blok.Block_ID);
            return View();
        }
    }
}
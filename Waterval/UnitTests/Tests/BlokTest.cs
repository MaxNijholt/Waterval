using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryModel.DummyRepository;
using System.Linq;
using DomainModel.Models;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class BlokTest
    {
        DummyBlockRepository block;

        [TestMethod]
        public void GetAll()
        {
            block = new DummyBlockRepository();

            List<Block> comp = block.GetAll();

            int result = comp.Count;

            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetByID()
        {
            block = new DummyBlockRepository();
            
            Block test = new Block { Block_ID = 4, Title = "Dummy Blok 4" };
            Block gBlock = block.Get(4);


            Assert.AreEqual(test.Block_ID, gBlock.Block_ID);
            Assert.AreEqual(test.Title, gBlock.Title);
        }

        [TestMethod]
        public void Create()
        {
            block = new DummyBlockRepository();

            block.Create(new Block { Block_ID = 11, Title = "Dummy Block 11" });

            List<Block> blocks = block.GetAll();

            Assert.AreEqual(11, blocks.Count);
        }

        [TestMethod]
        public void Delete()
        {
            block = new DummyBlockRepository();

            block.Delete(3);

            List<Block> blocks = block.GetAll().Where(x => x.isDeleted == false).ToList();

            Assert.AreEqual(9, blocks.Count);

            Assert.AreEqual(1, block.GetAll().Where(x => x.isDeleted == true).Count());

            Assert.AreEqual(block.Get(3).DeleteDate, DateTime.UtcNow);
            Assert.AreEqual(block.Get(3).isDeleted, true);
        }

        [TestMethod]
        public void Edit()
        {
            block = new DummyBlockRepository();

            Block test = new Block { Block_ID = 4, Title = "Blocker 4" };

            block.Update(test);

            Block gBlock = block.Get(4);

            Assert.AreEqual(test.Block_ID, gBlock.Block_ID);
            Assert.AreEqual(test.DeleteDate, gBlock.DeleteDate);
            Assert.AreEqual(test.isDeleted, gBlock.isDeleted);
            Assert.AreEqual(test.Title, gBlock.Title);
        }
    }
}

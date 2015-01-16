using DomainModel.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryModel.DummyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Tests
{
    [TestClass]
    public class ThemeTest
    {
        DummyPhasingRepository phasingRep;

        [TestMethod]
        public void GetAll()
        {
            phasingRep = new DummyPhasingRepository();

            List<Phasing> ph = phasingRep.GetAll();

            int result = ph.Count;

            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetByID()
        {
            phasingRep = new DummyPhasingRepository();
            Phasing test = new Phasing { Phasing_ID = 4, Title = "Phasing 4" };
            Phasing gPhasing = phasingRep.Get(4);

            Assert.AreEqual(test.Phasing_ID, gPhasing.Phasing_ID);
            Assert.AreEqual(test.Title, gPhasing.Title);
        }

        [TestMethod]
        public void Create()
        {
            phasingRep = new DummyPhasingRepository();

            phasingRep.Create( new Phasing { Phasing_ID = 11, Title = "Phasing 11" });

            List<Phasing> phasings = phasingRep.GetAll();

            Assert.AreEqual(11, phasings.Count);
        }

        [TestMethod]
        public void Delete()
        {
            phasingRep = new DummyPhasingRepository();

            phasingRep.Delete(3);

            List<Phasing> phasings = phasingRep.GetAll().Where(x => x.isDeleted == false).ToList();

            Assert.AreEqual(9, phasings.Count);

            Assert.AreEqual(1, phasingRep.GetAll().Where(x => x.isDeleted == true).Count());

            Assert.AreEqual(phasingRep.Get(3).DeleteDate, DateTime.UtcNow);
            Assert.AreEqual(phasingRep.Get(3).isDeleted, true);
        }

        [TestMethod]
        public void Edit()
        {
            phasingRep = new DummyPhasingRepository();

            Phasing test = new Phasing { Phasing_ID = 4, Title = "PhasingEdit 4" };

            phasingRep.Update(test);

            Phasing gPhasing = phasingRep.Get(4);

            Assert.AreEqual(test.Phasing_ID, gPhasing.Phasing_ID);
            Assert.AreEqual(test.DeleteDate, gPhasing.DeleteDate);
            Assert.AreEqual(test.isDeleted, gPhasing.isDeleted);
            Assert.AreEqual(test.Title, gPhasing.Title);
        }
    }
}

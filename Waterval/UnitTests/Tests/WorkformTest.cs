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
    public class WorkformTest
    {
        DummyWorkformRepository workformRep;

        [TestMethod]
        public void GetAll()
        {
            workformRep = new DummyWorkformRepository();

            List<Workform> wf = workformRep.GetAll();

            int result = wf.Count;

            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetByID()
        {
            workformRep = new DummyWorkformRepository();

            Workform test = new Workform { Workform_ID = 4, Description = "Workform 4" };
            Workform gWorkform = workformRep.Get(4);


            Assert.AreEqual(test.Workform_ID, gWorkform.Workform_ID);
            Assert.AreEqual(test.Description, gWorkform.Description);
        }

        [TestMethod]
        public void Create()
        {
            workformRep = new DummyWorkformRepository();

            workformRep.Create( new Workform { Workform_ID = 11, Description = "Workform 11" });

            List<Workform> workforms = workformRep.GetAll();

            Assert.AreEqual(11, workforms.Count);
        }

        [TestMethod]
        public void Delete()
        {
            workformRep = new DummyWorkformRepository();

            workformRep.Delete(3);

            List<Workform> workforms = workformRep.GetAll().Where(x => x.isDeleted == false).ToList();

            Assert.AreEqual(9, workforms.Count);

            Assert.AreEqual(1, workformRep.GetAll().Where(x => x.isDeleted == true).Count());

            Assert.AreEqual(workformRep.Get(3).DeleteDate, DateTime.UtcNow);
            Assert.AreEqual(workformRep.Get(3).isDeleted, true);
        }

        [TestMethod]
        public void Edit()
        {
            workformRep = new DummyWorkformRepository();

            Workform test = new Workform { Workform_ID = 4, Description = "Werkvormer 4" };

            workformRep.Update(test);

            Workform gWorkform = workformRep.Get(4);

            Assert.AreEqual(test.Workform_ID, gWorkform.Workform_ID);
            Assert.AreEqual(test.DeleteDate, gWorkform.DeleteDate);
            Assert.AreEqual(test.isDeleted, gWorkform.isDeleted);
            Assert.AreEqual(test.Description, gWorkform.Description);
        }

    }
}

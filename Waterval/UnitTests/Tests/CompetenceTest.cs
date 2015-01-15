using DomainModel.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryModel.DummyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CompetenceTest
    {
        DummyCompetenceRepository d;

        [TestMethod]
        public void GetAll()
        {
            d = new DummyCompetenceRepository();

            List<Competence> comp = d.GetAll();

            int result = comp.Count;

            Assert.AreEqual(result, 10);

        }
        [TestMethod]
        public void Delete()
        {
            d = new DummyCompetenceRepository();

            List<Competence> comp = d.GetAll();

            d.Delete(1);

            int result = comp.Count;

            Assert.AreEqual(result, 9);
        }

        [TestMethod]
        public void GetAllLevels()
        {
            d = new DummyCompetenceRepository();

            List<Competence> comp = d.GetAll();

            int result = 0;

            foreach (var item in comp)
                foreach (var bitem in item.Level)
                    result++;

            Assert.AreEqual(result, 30);
        }

        [TestMethod]
        public void Update()
        {

            d = new DummyCompetenceRepository();
            List<Competence> comp = d.GetAll();

            Competence update = d.Get(3);

            update.Definition_Long = "Aangepast";

            d.Update(update);

            Assert.AreEqual(update.Definition_Long, d.Get(3).Definition_Long);

        }

        [TestMethod]
        public void Create()
        {
            d = new DummyCompetenceRepository();
            List<Competence> comp = d.GetAll();

            d.Create(new Competence { Competence_ID = 11, Definition_Short = "Kort test", Definition_Long = "Lang Test" });

            int result = comp.Count;

            Assert.AreEqual(result, 11);
        }

        [TestMethod]
        public void GetByID()
        {
            d = new DummyCompetenceRepository();
            List<Competence> comp = d.GetAll();

            Competence Test = new Competence();
            Test.Competence_ID = 1;
            Test.Definition_Short = "Omschrijving Kort 1";
            Test.Definition_Long = "Omschrijving Lang 1";

            Competence competence = d.Get(1                );

            Assert.AreEqual(Test.Competence_ID,competence.Competence_ID);
            Assert.AreEqual(Test.Definition_Short, competence.Definition_Short);
            Assert.AreEqual(Test.Definition_Long, competence.Definition_Long);
        }
    }
}

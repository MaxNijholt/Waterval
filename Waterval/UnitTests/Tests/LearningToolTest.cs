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
    public class LearningToolTest
    {
        DummyLearningToolRepository toolRep;

        [TestMethod]
        public void GetAll()
        {
            toolRep = new DummyLearningToolRepository();

            List<LearningTool> lt = toolRep.GetAll();

            int result = lt.Count;

            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void GetByID()
        {
            toolRep = new DummyLearningToolRepository();

			LearningTool test = new LearningTool { LearnTool_ID = 4, Description = "LearningTool 4" };
			LearningTool gLearningTool = toolRep.Get(4);


            Assert.AreEqual(test.LearnTool_ID, gLearningTool.LearnTool_ID);
            Assert.AreEqual(test.Description, gLearningTool.Description);
        }

        [TestMethod]
        public void Create()
        {
            toolRep = new DummyLearningToolRepository();

			toolRep.Create(new LearningTool { LearnTool_ID = 11, Description = "LearningTool 11" });

			List<LearningTool> learnTools = toolRep.GetAll();

            Assert.AreEqual(11, learnTools.Count);
        }

        [TestMethod]
        public void Delete()
        {
            toolRep = new DummyLearningToolRepository();

            toolRep.Delete(3);

			List<LearningTool> learningTools = toolRep.GetAll().Where(x => x.isDeleted == false).ToList();

            Assert.AreEqual(9, learningTools.Count);

            Assert.AreEqual(1, toolRep.GetAll().Where(x => x.isDeleted == true).Count());

            Assert.AreEqual(toolRep.Get(3).DeleteDate, DateTime.UtcNow);
            Assert.AreEqual(toolRep.Get(3).isDeleted, true);
        }

        [TestMethod]
        public void Edit()
        {
            toolRep = new DummyLearningToolRepository();

			LearningTool test = new LearningTool { LearnTool_ID = 4, Description = "LearingTool 4" };

            toolRep.Update(test);

			LearningTool gLearningTool = toolRep.Get(4);

            Assert.AreEqual(test.LearnTool_ID, gLearningTool.LearnTool_ID);
            Assert.AreEqual(test.DeleteDate, gLearningTool.DeleteDate);
            Assert.AreEqual(test.isDeleted, gLearningTool.isDeleted);
            Assert.AreEqual(test.Description, gLearningTool.Description);
        }
    }
}

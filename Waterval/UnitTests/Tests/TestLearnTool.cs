using DomainModel.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositoryModel.DummyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Processors;

namespace UnitTests.Tests
{
    [TestClass]
    public class TestLearnTool
    {
        [TestMethod]
        public void GetListTest()
        {
            // Arrange
            LearnToolProcessor processor = new LearnToolProcessor(new DLearningToolRepository());
            processor.GetAll();
            // Act
            List<LearningTool> allLearningTools = processor.LearningToolList;

            // Assert
            Assert.AreEqual(allLearningTools.Count(), 8);
        }

        [TestMethod]
        public void DeleteOne()
        {
            // Arrange
            LearnToolProcessor processor = new LearnToolProcessor(new DLearningToolRepository());

            // Act
            processor.Delete();
            List<LearningTool> allLearningTools = processor.LearningToolList;
            // Assert
            Assert.AreEqual(allLearningTools.Count(), 7);
        }

        [TestMethod]
        public void AddOne()
        {
            // Arrange
            LearnToolProcessor processor = new LearnToolProcessor(new DLearningToolRepository());

            // Act
            processor.Add();
            List<LearningTool> allLearningTools = processor.LearningToolList;
            // Assert
            Assert.AreEqual(allLearningTools.Count(), 9);
        }
    }
}

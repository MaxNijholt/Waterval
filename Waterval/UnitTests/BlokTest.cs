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
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            BlockProcessor processor = new BlockProcessor(new DBlockRepository());

            // Act
            List<Block> currentSeries = processor.GetCurrentBlock();

            // Assert
            Assert.AreEqual(currentSeries.Count(), 8);
        }
    }
}

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
	public class LearnGoalTest
	{
		DummyLearnGoalRepository d;

		[TestMethod]
		public void GetAll()
		{
			d = new DummyLearnGoalRepository();

			List<LearnGoal> comp = d.GetAll();

			int result = comp.Count;

			Assert.AreEqual(result, 10);

		}
		[TestMethod]
		public void Delete()
		{
			d = new DummyLearnGoalRepository();

			List<LearnGoal> comp = d.GetAll();

			d.Delete(1);

			int result = comp.Count;

			Assert.AreEqual(result, 9);
		}
		[TestMethod]
		public void Update()
		{
			d = new DummyLearnGoalRepository();
			List<LearnGoal> comp = d.GetAll();

			LearnGoal update = d.Get(3);

			update.Description = "Aangepast";

			d.Update(update);

			Assert.AreEqual(update.Description, d.Get(3).Description);

		}

		[TestMethod]
		public void Create()
		{
			d = new DummyLearnGoalRepository();
			List<LearnGoal> comp = d.GetAll();

			d.Create(new LearnGoal { LearnGoal_ID = 11, Description = "LearnGoal" });

			int result = comp.Count;

			Assert.AreEqual(result, 11);
		}

		[TestMethod]
		public void GetByID()
		{
			d = new DummyLearnGoalRepository();
			List<LearnGoal> comp = d.GetAll();

			LearnGoal Test = new LearnGoal();
			Test.LearnGoal_ID = 1;
			Test.Description = "LearnGoalNew";
			
			LearnGoal learnGoal = d.Get(1);

			Assert.AreEqual(Test.LearnGoal_ID, learnGoal.LearnGoal_ID);
			Assert.AreEqual(Test.Description, learnGoal.Description);
		}
	}
}

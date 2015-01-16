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
	public class GradeTypeTest
	{
		DummyGradeTypeRepository gradeTypeRep;

		[TestMethod]
		public void GetAll()
		{
			gradeTypeRep = new DummyGradeTypeRepository();

			List<GradeType> gt = gradeTypeRep.GetAll();

			int result = gt.Count;

			Assert.AreEqual(result, 10);
		}

		[TestMethod]
		public void GetByID()
		{
			gradeTypeRep = new DummyGradeTypeRepository();
			GradeType test = new GradeType { GradeType_ID = 4, GradeDescription = "GradeType 4" };
			GradeType gGradeType = gradeTypeRep.Get(4);

			Assert.AreEqual(test.GradeType_ID, gGradeType.GradeType_ID);
			Assert.AreEqual(test.GradeDescription, gGradeType.GradeDescription);
		}

		[TestMethod]
		public void Create()
		{
			gradeTypeRep = new DummyGradeTypeRepository();

			gradeTypeRep.Create(new GradeType { GradeType_ID = 11, GradeDescription = "GradeType 11" });

			List<GradeType> gradeTypes = gradeTypeRep.GetAll();

			Assert.AreEqual(11, gradeTypes.Count);
		}

		[TestMethod]
		public void Delete()
		{
			gradeTypeRep = new DummyGradeTypeRepository();

			gradeTypeRep.Delete(3);

			List<GradeType> gradeTypes = gradeTypeRep.GetAll().Where(x => x.isDeleted == false).ToList();

			Assert.AreEqual(9, gradeTypes.Count);

			Assert.AreEqual(1, gradeTypeRep.GetAll().Where(x => x.isDeleted == true).Count());

			Assert.AreEqual(gradeTypeRep.Get(3).DeleteDate, DateTime.UtcNow);
			Assert.AreEqual(gradeTypeRep.Get(3).isDeleted, true);
		}

		[TestMethod]
		public void Edit()
		{
			gradeTypeRep = new DummyGradeTypeRepository();

			GradeType test = new GradeType { GradeType_ID = 4, GradeDescription = "Gradetype 4" };

			gradeTypeRep.Update(test);

			GradeType gGradeType = gradeTypeRep.Get(4);

			Assert.AreEqual(test.GradeType_ID, gGradeType.GradeType_ID);
			Assert.AreEqual(test.DeleteDate, gGradeType.DeleteDate);
			Assert.AreEqual(test.isDeleted, gGradeType.isDeleted);
			Assert.AreEqual(test.GradeDescription, gGradeType.GradeDescription);
		}
	}
}

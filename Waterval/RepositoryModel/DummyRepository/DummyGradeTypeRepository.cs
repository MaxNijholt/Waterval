using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
	public class DummyGradeTypeRepository : IGradeTypeRepository
	{
		List<GradeType> gradeTypes;

		public DummyGradeTypeRepository()
		{
			FillList();
		}

		public List<GradeType> GetAll()
		{
			return gradeTypes;
		}

		public GradeType Get(int gradeType_id)
		{
			return gradeTypes.Where(x => x.GradeType_ID == gradeType_id).First();
		}

		public GradeType Create(GradeType gradeType)
		{
			gradeTypes.Add(gradeType);
			return gradeType;
		}

		public void Delete(int gradeType_id)
		{
			GradeType f = gradeTypes.Where(x => x.GradeType_ID == gradeType_id).First();
			f.isDeleted = true;
			f.DeleteDate = DateTime.UtcNow;
		}

		public GradeType Update(GradeType gradeType)
		{
			GradeType f = gradeTypes.Where(x => x.GradeType_ID == gradeType.GradeType_ID).First();
			f.GradeDescription = gradeType.GradeDescription;
			return f;
		}

		private void FillList()
		{
			gradeTypes = new List<GradeType>();
			for(int i = 1; i <= 10; i++)
				gradeTypes.Add(new GradeType { GradeType_ID = i, GradeDescription = "GradeType " + i });
		}
	}
}

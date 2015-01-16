using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
	public class DummyLearningToolRepository : ILearningToolRepository
	{
		List<LearningTool> testLearningTools;

		public DummyLearningToolRepository()
		{
			FillList();
		}

		public List<LearningTool> GetAll()
		{
			return testLearningTools;
		}

		public LearningTool Get(int learningTool_id)
		{
			return testLearningTools.Where(x => x.LearnTool_ID == learningTool_id).First();
		}

		public LearningTool Create(LearningTool learningTool)
		{
			testLearningTools.Add(learningTool);
			return learningTool;
		}

		public void Delete(int learningTool_id)
		{
			LearningTool f = testLearningTools.Where(x => x.LearnTool_ID == learningTool_id).First();
			f.isDeleted = true;
			f.DeleteDate = DateTime.UtcNow;
		}

		public LearningTool Update(LearningTool learningTool)
		{
			LearningTool f = testLearningTools.Where(x => x.LearnTool_ID == learningTool.LearnTool_ID).First();

			f.Description = learningTool.Description;

			return f;
		}

		private void FillList()
		{
			testLearningTools = new List<LearningTool>();

			for(int i = 1; i <= 10; i++)
				testLearningTools.Add(new LearningTool { LearnTool_ID = i, Description = "LearningTool " + i });
		}
	}
}

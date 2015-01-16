using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DummyLearnGoalRepository: ILearnGoalRepository
    {
        List<LearnGoal> learnGoals;

        public DummyLearnGoalRepository()
        {
            FillList();
        }

		public List<LearnGoal> GetAll()
        {
            return learnGoals;
        }

		public LearnGoal Get(int learnGoal_id)
        {
            return learnGoals.Where(x => x.LearnGoal_ID == learnGoal_id).First();
        }

		public LearnGoal Create(LearnGoal learnGoal)
        {
            learnGoals.Add(learnGoal);
            return learnGoal;
        }

        public void Delete(int learnGoal_id)
        {
			LearnGoal f = learnGoals.Where(x => x.LearnGoal_ID == learnGoal_id ).First();
            f.isDeleted = true;
            f.DeleteDate = DateTime.UtcNow;
        }

		public LearnGoal Update(LearnGoal learnGoal)
        {
			LearnGoal f = learnGoals.Where(x => x.LearnGoal_ID == learnGoal.LearnGoal_ID).First();

            f.Description = learnGoal.Description;

            return f;
        }

        private void FillList()
        {
			learnGoals = new List<LearnGoal>();

            for(int i = 1; i <= 10; i++)
				learnGoals.Add(new LearnGoal { LearnGoal_ID = i, Description = "LearnGoal " + i });
        }
    }
}

using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
    public class LearnGoalRepository : ILearnGoalRepository
    {
        Project_WatervalEntities dbContext;

        public LearnGoalRepository()
        {
            dbContext = new Project_WatervalEntities();
        }
        public List<LearnGoal> GetAll()
        {
            return dbContext.LearnGoal.Where(b => b.isDeleted == false).ToList();
        }

        public LearnGoal Get(int learnGoal_id)
        {
            return dbContext.LearnGoal.Find(learnGoal_id);
        }

        public LearnGoal Create(LearnGoal learnGoal)
        {
            dbContext.LearnGoal.Add(learnGoal);
            dbContext.SaveChanges();
            return learnGoal;
        }

        public void Delete(int learnGoal_id)
        {
            LearnGoal learnGoal = dbContext.LearnGoal.SingleOrDefault(c => c.LearnGoal_ID == learnGoal_id);
            learnGoal.isDeleted = true;
            learnGoal.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();
        }

        public LearnGoal Update(LearnGoal update)
        {
            LearnGoal learnGoal = dbContext.LearnGoal.SingleOrDefault(b => b.LearnGoal_ID == update.LearnGoal_ID);
            if (learnGoal == null) return null;

            learnGoal.Description = update.Description;
            dbContext.SaveChanges();
            return learnGoal;
        }

        public LearnGoal GetNewVersion(int id)
        {
            LearnGoal newGoal = dbContext.LearnGoal.Where(c => c.PrevLearnGoal_ID == id).SingleOrDefault();
            return newGoal;
        }
    }
}

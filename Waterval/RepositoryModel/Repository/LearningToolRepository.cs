using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
   public  class LearningToolRepository : ILearningToolRepository
    {
       Project_WatervalEntities dbContext;

       public LearningToolRepository()
       {
           dbContext = new Project_WatervalEntities();
       }
        public List<LearningTool> GetAll()
        {
            return dbContext.LearningTool.ToList();
        }

        public LearningTool Get(int learningtool_id)
        {
            return dbContext.LearningTool.Find(learningtool_id);
        }

        public LearningTool Create(LearningTool learningTool)
        {
            dbContext.LearningTool.Add(learningTool);
            dbContext.SaveChanges();
            return learningTool;
        }

        public void Delete(int learningtool_id)
        {
            LearningTool learningtool = dbContext.LearningTool.SingleOrDefault(c => c.LearnTool_ID == learningtool_id);
        //    learningtool.isDeleted = true;
        //    learningtool.DeleteDate = DateTime.UtcNow;
            learningtool.isDeleted = true;
            learningtool.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();
        }

        public LearningTool Update(LearningTool update)
        {
            LearningTool learningtool = dbContext.LearningTool.SingleOrDefault(b => b.LearnTool_ID == update.LearnTool_ID);
            if (learningtool == null) return null;

            learningtool.Description = update.Description;

            dbContext.SaveChanges();
            return learningtool;
        }

        public LearningTool GetNewVersion(int id)
        {
            LearningTool newLearning = dbContext.LearningTool.Where(c => c.PrevLearnTool_ID == id).SingleOrDefault();
            return newLearning;
        }
    }
}

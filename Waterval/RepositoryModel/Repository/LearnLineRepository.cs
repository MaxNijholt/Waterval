using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
    public class LearnLineRepository : ILearnLineRepository
    {
        Project_WatervalEntities dbContext;

        public LearnLineRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
        }
        public List<DomainModel.Models.LearnLine> GetAll()
        {
            return dbContext.LearnLine.ToList();
        }

        public DomainModel.Models.LearnLine Get(int learnLine_id)
        {
            return dbContext.LearnLine.Find(learnLine_id);
        }

        public DomainModel.Models.LearnLine Create(DomainModel.Models.LearnLine learnLine)
        {
            dbContext.LearnLine.Add(learnLine);
            dbContext.SaveChanges();
            return learnLine;
        }

        public void Delete(int learnLine_id)
        {
            LearnLine learnLine = dbContext.LearnLine.SingleOrDefault(c => c.LearnLine_ID == learnLine_id);
            learnLine.isDeleted = true;
            learnLine.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();
        }

        public LearnLine GetNewVersion(int prevLearnLine_ID)
        {
            LearnLine newComp = dbContext.LearnLine.Where(c => c.PrevLearnLine_ID == prevLearnLine_ID).SingleOrDefault();
            return newComp;
        }

        public DomainModel.Models.LearnLine Update(DomainModel.Models.LearnLine update)
        {
            LearnLine LearnLine = dbContext.LearnLine.SingleOrDefault(b => b.LearnLine_ID == update.LearnLine_ID);
            if (LearnLine == null) return null;

            LearnLine.Definition = update.Definition;
            LearnLine.Title = update.Title;
            dbContext.SaveChanges();
            return LearnLine;
        }
    }
}

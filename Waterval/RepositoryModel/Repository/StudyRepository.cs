using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
    public class StudyRepository : IStudyRepository
    {
        Project_WatervalEntities dbContext;

        public StudyRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
        }

        public List<Study> GetAll()
        {
            return dbContext.Study.Where(b => b.isDeleted == false).ToList();

        }

        public Study Get(int id)
        {
            return dbContext.Study.Find(id);
        }

        public Study Create(Study study)
        {
            if (dbContext.Study.Any(o => o.Study_ID == study.Study_ID && !o.isDeleted))
                return null;
            dbContext.Study.Add(study);
            dbContext.SaveChanges();
            return study;
        }

        public Study Update(Study update)
        {

            Study study = dbContext.Study.SingleOrDefault(b => b.Study_ID == update.Study_ID);
            if (study == null) return null;
            study.Title = update.Title;
            dbContext.SaveChanges();
            return study;
        }

        public void Delete(int id)
        {
            Study study = dbContext.Study.SingleOrDefault(b => b.Study_ID == id);

            study.isDeleted = true;
            study.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();

        }

    }
}

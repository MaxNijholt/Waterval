using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
    public class WorkformRepository : IWorkformRepository
    {
        Project_WatervalEntities dbContext;

        public WorkformRepository()
        {
            dbContext = new Project_WatervalEntities();
        }
        public List<Workform> GetAll()
        {


            return dbContext.Workform.ToList();
        }

        public Workform Get(int workform_id)
        {
            return dbContext.Workform.Find(workform_id);
        }

        public Workform Create(Workform workform)
        {
            dbContext.Workform.Add(workform);
            dbContext.SaveChanges();
            return workform;
        }

        public void Delete(int workform_id)
        {
            Workform workform = dbContext.Workform.Find(workform_id);

            workform.isDeleted = true;
            workform.DeleteDate = DateTime.Now;

            dbContext.SaveChanges();
        }

        public Workform Update(Workform update)
        {
            Workform workform = dbContext.Workform.Where(w => w.Workform_ID == update.Workform_ID).First();

            workform.Description = update.Description;
            workform.PrevWorkform_ID = update.PrevWorkform_ID;

            dbContext.SaveChanges();

            return workform;
        }

        public Workform GetNewVersion(int id)
        {
            Workform newWorkform = dbContext.Workform.Where(c => c.PrevWorkform_ID == id).SingleOrDefault();
            return newWorkform;
        }
    }
}

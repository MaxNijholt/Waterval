using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using RepositoryModel.IRepository;

namespace RepositoryModel.Repository
{
    public class ProgramRepository : IProgramRepository
    {
          Project_WatervalEntities dbContext;

          public ProgramRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
        }

          public List<DomainModel.Models.Program> GetAll()
        {
            return dbContext.Program.Where(b => b.isDeleted == false).ToList();
        }

        public Program Get(int id)
        {
            return dbContext.Program.Find(id);
        }

        public Program Create(Program program)
        {
            if (dbContext.Program.Any(o => o.Program_ID == program.Program_ID && !o.isDeleted))
                return null;
            dbContext.Program.Add(program);
            dbContext.SaveChanges();
            return program;
        }

        public Program Update(Program update)
        {

            Program program = dbContext.Program.SingleOrDefault(b => b.Program_ID == update.Program_ID);
            if (program == null) return null;
            program.Cohort = update.Cohort;
            dbContext.SaveChanges();
            return program;
        }

        public void Delete(int id)
        {
            Program program = dbContext.Program.SingleOrDefault(b => b.Program_ID == id);

            program.isDeleted = true;
            program.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();

        }
    }
}

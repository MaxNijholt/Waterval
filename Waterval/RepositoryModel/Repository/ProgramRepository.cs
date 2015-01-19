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
          List<Study> studys;


          public ProgramRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
            studys = new List<Study>();

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
            if (program == null) return program;

            addLinks(program);
            program.Study = studys;

            dbContext.Program.Add(program);
            dbContext.SaveChanges();
            return program;
        }


        private void addLinks(Program program)
        {
            studys.Clear();

            for (int index = 0; index < program.Study.Count; index++)
            {
                studys.Add(dbContext.Study.Find(program.Study.ElementAt(index).Study_ID));
            }
        }


        public DomainModel.Models.Program Update(DomainModel.Models.Program update)
        {

            Program program = dbContext.Program.SingleOrDefault(b => b.Program_ID == update.Program_ID);
            if (program == null) return null;

            program.Cohort = update.Cohort;

            addLinks(update);
            program.Study.Clear();
            program.Study = studys;

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

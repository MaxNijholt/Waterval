using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
    public class CompetenceRepository : ICompetenceRepository
    {
        Project_WatervalEntities dbContext;

        public CompetenceRepository()
        {
            dbContext = new Project_WatervalEntities();
        }
        public List<Competence> GetAll()
        {
            return dbContext.Competence.ToList();
        }

        public Competence Get(int compentence_id)
        {
            return dbContext.Competence.Find(compentence_id);
        }

        public Competence Create(Competence compentence)
        {

            dbContext.Competence.Add(compentence);
            dbContext.SaveChanges();
            return compentence;
        }

        public void Delete(int compentence_id)
        {
            Competence compentence = dbContext.Competence.SingleOrDefault(c => c.Competence_ID == compentence_id);
            compentence.isDeleted = true;
            compentence.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();
        }

        public Competence Update(Competence update)
        {
            Competence competence = dbContext.Competence.SingleOrDefault(b => b.Competence_ID == update.Competence_ID);
            if (competence == null) return null;

            competence.Definition_Long = update.Definition_Long;
            competence.Definition_Short = update.Definition_Short;
            competence.Title = update.Title;
            dbContext.SaveChanges();
            return competence;
        }

        public Competence GetNewVersion(int id)
        {
            Competence newComp = dbContext.Competence.Where(c => c.PrevCompetence_ID == id).SingleOrDefault();
            return newComp;
        }

        public void CompentenceAndModules(int id, int modid, string level)
        {

            if (dbContext.Level.Any(l => l.Module_ID == modid && l.Competence_ID == id))
                return;

            Level model = new Level();
            model.Competence_ID = id;
            model.Module_ID = modid;
            model.Level1 = level;
            dbContext.Level.Add(model);
            dbContext.SaveChanges();
        }

        public int GetLatest()
        {
            return dbContext.Competence.Last().Competence_ID;
        }

        public void CompentenceAndModulesDelete(int compid)
        {
            var itemsToDelete = dbContext.Level.Where(x => x.Competence_ID == compid);
            dbContext.Level.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }
    }
}

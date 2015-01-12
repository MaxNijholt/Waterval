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

        /// <summary>
        /// Returns a list containing al of the competence in the database.
        /// </summary>
        /// <returns></returns>
        public List<Competence> GetAll()
        {
            return dbContext.Competence.ToList();
        }
        
        //Get a competence based on an id.
        /// <summary>
        /// Get a competence based on an id.
        /// </summary>
        /// <param name="compentence_id">ID of the competence you want to get</param>
        /// <returns></returns>
        public Competence Get(int compentence_id)
        {
            return dbContext.Competence.Find(compentence_id);
        }

        /// <summary>
        /// Add a competence to the database and returns it.
        /// </summary>
        /// <param name="compentence">A competence that has al the needed values</param>
        /// <returns></returns>
        public Competence Create(Competence compentence)
        {
            dbContext.Competence.Add(compentence);
            dbContext.SaveChanges();
            return compentence;
        }


        /// <summary>
        //  Update a chosen competence to mark it as deleted.
        //  Marks the date as now.
        /// </summary>
        /// <param name="compentence_id">The id of the competence you want to delete</param>
        public void Delete(int compentence_id)
        {
            Competence compentence = dbContext.Competence.SingleOrDefault(c => c.Competence_ID == compentence_id);
            compentence.isDeleted = true;
            compentence.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();
        }


        /// <summary>
        //Updates an excisting Competence with a new set of values. 
        //If the competence does not excist a null version will be send back and will give a message.
        //Otherwise the new values get added and the competence will get saved.
        /// </summary>
        /// <param name="update">The new values of the competence</param>
        /// <returns></returns>
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

       
        /// <summary>
        //Gets the new version based on an id of the old one.
        /// </summary>
        /// <param name="prevCompetence_ID">the Id of the old version</param>
        /// <returns></returns>
        public Competence GetNewVersion(int prevCompetence_ID)
        {
            Competence newComp = dbContext.Competence.Where(c => c.PrevCompetence_ID == prevCompetence_ID).SingleOrDefault();
            return newComp;
        }

    
        /// <summary>
        //Creates a new level. 
        //This level is based on the id of the competence of the module_id
        //If a record already exist then the code will return.
        //Otherwise a new level will be created
        /// </summary>
        /// <param name="competence_id">The id of the competence</param>
        /// <param name="module_id">the id of the module</param>
        /// <param name="level"></param>
        public void CompentenceAndModules(int competence_id, int module_id, string level)
        {

            if (dbContext.Level.Any(l => l.Module_ID == module_id && l.Competence_ID == competence_id))
                return;

            Level model = new Level();
            model.Competence_ID = competence_id;
            model.Module_ID = module_id;
            model.Level1 = level;
            dbContext.Level.Add(model);
            dbContext.SaveChanges();
        }


        /// <summary>
        /// Deletes al level record based on the competence_id
        /// </summary>
        /// <param name="competence_id">The id of the competence from which levels you want to delete</param>
        public void CompentenceAndModulesDelete(int competence_id)
        {
            var itemsToDelete = dbContext.Level.Where(x => x.Competence_ID == competence_id);
            dbContext.Level.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }
    }
}

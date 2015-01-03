using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using RepositoryModel.IRepository;

namespace RepositoryModel.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        Project_WatervalEntities dbContext;

        public ModuleRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
        }

        public List<DomainModel.Models.Module> GetAll()
        {
            return dbContext.Module.ToList();
        }

        public DomainModel.Models.Module Get(int module_id)
        {
            return dbContext.Module.Find(module_id);
        }

        public DomainModel.Models.Module Create(DomainModel.Models.Module module)
        {
            dbContext.Module.Add(module);
            dbContext.SaveChanges();
            return module;
        }

        public DomainModel.Models.Module Update(DomainModel.Models.Module update)
        {
            Module module = dbContext.Module.SingleOrDefault(b => b.Module_ID == update.Module_ID);
            if (module == null) return null;
            module.Title = update.Title;
            module.PrevModule_ID = update.PrevModule_ID;
            module.CourseCode = update.CourseCode;
            module.Entry_Level = update.Entry_Level;
            module.Definition_Short = update.Definition_Long;
            module.Definition_Long = update.Definition_Long;
            module.Foreknowledge = update.Foreknowledge;
            module.Account_ID = update.Account_ID;
            
            /*
            module.Course_Code = update.Course_Code;
            module.EC = update.EC;
            module.Method = update.Method;
            module.Entry_Level = update.Entry_Level;
            module.Workload = update.Workload;
            module.GradeType = update.GradeType;
            module.Organization = update.Organization;*/

          
            dbContext.SaveChanges();
            return module;
        }

        public void Delete(int module_id)
        {
            Module module = dbContext.Module.SingleOrDefault(b => b.Module_ID == module_id);

            module.isDeleted = true;
            module.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();

        }

        public void ModulesAndGradeTypes(int module_id)
        {

            if (dbContext.GradeType.Any(l => l.Module_ID == module_id))
                return;

            GradeType model = new GradeType();
            model.Module_ID = module_id;
            dbContext.GradeType.Add(model);
            dbContext.SaveChanges();
        }

        public void ModulesAndWorkForm(int module_id, int workform_id)
        {

            if (dbContext.ModelWithWorkform.Any(l => l.Workform_ID == workform_id && l.Module_ID == module_id))
                return;

            ModelWithWorkform model = new ModelWithWorkform();
            model.Workform_ID = workform_id;
            model.Module_ID = module_id;
            dbContext.ModelWithWorkform.Add(model);
            dbContext.SaveChanges();
        }


        /// <summary>
        /// Deletes al level record based on the competence_id
        /// </summary>
        /// <param name="competence_id">The id of the competence from which levels you want to delete</param>
        public void ModulesAndWorkFormDelete(int module_id)
        {
            var itemsToDelete = dbContext.ModelWithWorkform.Where(x => x.Module_ID == module_id);
            dbContext.ModelWithWorkform.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }



    }
}

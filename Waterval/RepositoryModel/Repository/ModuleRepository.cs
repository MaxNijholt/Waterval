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
            module.Title = update.Title;/*
            module.Course_Code = update.Course_Code;
            module.EC = update.EC;
            module.Method = update.Method;
            module.Entry_Level = update.Entry_Level;
            module.Workload = update.Workload;
            module.GradeType = update.GradeType;
            module.Organization = update.Organization;*/
            module.Definition_Short = update.Definition_Short;
            module.Definition_Long = update.Definition_Long;
            module.Foreknowledge = update.Foreknowledge;
          
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

    }
}

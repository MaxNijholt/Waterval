using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models;
using System.Collections;

namespace RepositoryModel
{
    public class GradeTypeRepository : IGradeTypeRepository
    {
        Project_WatervalEntities dbContext;


        public GradeTypeRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
        }

        public List<GradeType> GetAll()
        {
            return dbContext.GradeType.ToList();

        }

        public GradeType Get(int id)
        {
			return dbContext.GradeType.FirstOrDefault(g => g.Module_ID == id);
        }

        public GradeType Create(GradeType gradetype)
        {

            dbContext.GradeType.Add(gradetype);
            dbContext.SaveChanges();
            return gradetype;
        }


        public void Delete(int module_id)
        {
            var itemsToDelete = dbContext.GradeType.Where(x => x.Module_ID == module_id);
            dbContext.GradeType.RemoveRange(itemsToDelete);
            dbContext.SaveChanges();
        }


    }
}
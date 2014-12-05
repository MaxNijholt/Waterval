using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using RepositoryModel.IRepository;


namespace RepositoryModel.DummyRepository
{
    public class DModuleRepository : IModuleRepository
    {
        public List<DomainModel.Models.Module> GetAll()
        {
            List<Module> fakeModule = new List<Module>()
            {
                new Module{ Module_ID = 1, Title = "Dummy Module 1", Course_Code = "ef", EC = ""+2, Method = "ef", Entry_Level = "ef", Workload = 2, GradeType = "ef"
                    , Organization = "avans", Definition_Short = "w", Definition_Long = "wfewfe", Foreknowledge = "esf", ModuleYear_ID = 2,
                    isDeleted = false, DeleteDate=DateTime.Now},
     
            };
            return fakeModule;

        }

        public IQueryable<Module> Module
        {
            get { return (IQueryable<Module>)GetAll(); }
        }


        public DomainModel.Models.Module Get(int Module_id)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Module Create(DomainModel.Models.Module Module)
        {
            throw new NotImplementedException();
        }

        public void Delete(int module_id)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Module Update(DomainModel.Models.Module module)
        {
            throw new NotImplementedException();
        }
    }
}

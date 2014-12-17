using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;


namespace RepositoryModel.IRepository
{
    public interface IModuleRepository
    {
        List<Module> GetAll();
        Module Get(int module_ID);
        Module Create(Module module);
        void Delete(int module_ID);
        Module Update(Module module);
    }
}

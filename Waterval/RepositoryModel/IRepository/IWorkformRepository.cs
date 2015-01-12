using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace RepositoryModel
{
    public interface IWorkformRepository
    {
        List<Workform> GetAll();
        Workform Get(int workform_id);
        Workform Create(Workform workform);
        void Delete(int workform_id);
        Workform Update(Workform workform);
 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace RepositoryModel
{
    public interface IGradeTypeRepository
    {
        List<GradeType> GetAll();
        List<GradeType> Get(int gradetype_id);
        GradeType Create(GradeType gradetype);
        void Delete(int gradetype_id);
   
    }
}
 
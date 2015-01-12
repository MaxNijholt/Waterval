using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository
{
    public interface IStudyRepository
    {
        List<Study> GetAll();
        Study Get(int study_id);
        Study Create(Study study);
        void Delete(int study_id);
        Study Update(Study study);
    }
}

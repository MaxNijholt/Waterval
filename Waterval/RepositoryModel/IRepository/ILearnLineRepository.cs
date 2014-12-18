using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository
{
    public interface ILearnLineRepository
    {
        List<LearnLine> GetAll();
        LearnLine Get(int learnLine_id);
        LearnLine Create(LearnLine learnLine);
        void Delete(int learnLine_id);
        LearnLine Update(LearnLine learnLine);
    }
}

using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository
{
    public interface ILearningToolRepository
    {
        List<LearningTool> GetAll();
        LearningTool Get(int learningtool_id);
        LearningTool Create(LearningTool learningTool);
        void Delete(int learningtool_id);
        LearningTool Update(LearningTool learningTool);
    }
}

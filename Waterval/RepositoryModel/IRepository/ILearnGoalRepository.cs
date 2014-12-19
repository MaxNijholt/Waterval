using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository
{
    public interface ILearnGoalRepository
    {
        List<LearnGoal> GetAll();
        LearnGoal Get(int learnGoal_id);
        LearnGoal Create(LearnGoal learnGoal);
        void Delete(int learnGoal_id);
        LearnGoal Update(LearnGoal learnGoal);
    }
}

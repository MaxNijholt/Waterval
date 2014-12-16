using DomainModel.Models;
using RepositoryModel;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Processors
{
    class LearnToolProcessor
    {
        private ILearningToolRepository learntoolRepository;

        public List<LearningTool> LearningToolList;

        public LearnToolProcessor(ILearningToolRepository learntoolRepository)
        {
            this.learntoolRepository = learntoolRepository;
            LearningToolList = new List<LearningTool>();
        }

        public void GetAll()
        {
            LearningToolList = learntoolRepository.GetAll();
        }

        public void Delete()
        {
            learntoolRepository.Delete(4);
            LearningToolList = learntoolRepository.GetAll();
        }

        internal void Add()
        {
            throw new NotImplementedException();
        }
    }
}

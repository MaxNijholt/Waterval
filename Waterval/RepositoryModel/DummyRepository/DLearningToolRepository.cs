using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DLearningToolRepository : ILearningToolRepository
    {
        public List<LearningTool> fakeblocks;

        public DLearningToolRepository()
        {
            fakeblocks = new List<LearningTool>()
            {
                new LearningTool{ LearnTool_ID = 1, Description = "Dummy LearningTool 1"},
                new LearningTool{ LearnTool_ID = 2, Description = "Dummy LearningTool 2"},
                new LearningTool{ LearnTool_ID = 3, Description = "Dummy LearningTool 3"},
                new LearningTool{ LearnTool_ID = 4, Description = "Dummy LearningTool 4"},
                new LearningTool{ LearnTool_ID = 5, Description = "Dummy LearningTool 5"},
                new LearningTool{ LearnTool_ID = 6, Description = "Dummy LearningTool 6"},
                new LearningTool{ LearnTool_ID = 7, Description = "Dummy LearningTool 7"},
                new LearningTool{ LearnTool_ID = 8, Description = "Dummy LearningTool 8"}
            };
        }
        public List<LearningTool> GetAll()
        {
           
            return fakeblocks;

        }

        public LearningTool Get(int learningtool_id)
        {
            LearningTool model = fakeblocks.Where(f => f.LearnTool_ID == learningtool_id).Single();
            return model;
        }

        public LearningTool Create(LearningTool learningTool)
        {
            fakeblocks.Add(learningTool);
            return learningTool;
        }

        public void Delete(int learningtool_id)
        {
            LearningTool model = fakeblocks.Where(f => f.LearnTool_ID == learningtool_id).Single();
            fakeblocks.Remove(model);
            
        }

        public LearningTool Update(LearningTool learningTool)
        {
            LearningTool model = fakeblocks.Where(f => f.LearnTool_ID == learningTool.LearnTool_ID).Single();

            model.Description = learningTool.Description;


            return model;
            
        }


    }
}

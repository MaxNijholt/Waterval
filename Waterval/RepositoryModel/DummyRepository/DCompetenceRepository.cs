using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    class DCompetenceRepository:IRepository.IModuleRepository
    {

        private List<Competence> competenceList;
        private List<Module>moduleList;
        public DCompetenceRepository()
        {
            competenceList = new List<Competence>();
            moduleList = new List<Module>();
            CreateList();
        }

        public List<DomainModel.Models.Module> GetAll()
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Module Get(int module_ID)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Module Create(DomainModel.Models.Module module)
        {
            throw new NotImplementedException();
        }

        public void Delete(int module_ID)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Module Update(DomainModel.Models.Module module)
        {
            throw new NotImplementedException();
        }


        public void CreateList()
        {
            for(int i = 1; i <= 10; i++)
                competenceList.Add(new Competence{Competence_ID = i, Definition_Long = "Omschrijving Lang "+i , Definition_Short = "Omschrijving Kort" + i});

            for (int i = 1; i <= 10; i++)
                moduleList.Add(new Module { Module_ID = i, Definition_Long = "Omschrijving Lang " + i, Definition_Short = "Omschrijving Kort" + i });


            for(int i = 1; i <=10;i++)
            {
                for(int j=1; j <= 3; j++)
                {
                    competenceList.ElementAt(i).Level.Add(new Level { Competence_ID = i, Level1 = (i * j).ToString(), Module_ID = i });
                }
            }
        }
    }
}

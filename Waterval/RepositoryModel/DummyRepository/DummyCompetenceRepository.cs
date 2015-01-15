using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DummyCompetenceRepository : ICompetenceRepository
    {

        private List<Competence> competenceList;
        private List<Module> moduleList;
        public DummyCompetenceRepository()
        {
            competenceList = new List<Competence>();
            moduleList = new List<Module>();
            CreateList();
        }


        public void CreateList()
        {
            for (int i = 1; i <= 10; i++)
                competenceList.Add(new Competence { Competence_ID = i, Definition_Long = "Omschrijving Lang " + i, Definition_Short = "Omschrijving Kort " + i });

            for (int i = 1; i <= 10; i++)
                moduleList.Add(new Module { Module_ID = i, Definition_Long = "Omschrijving Lang " + i, Definition_Short = "Omschrijving Kort " + i });


            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    competenceList.ElementAt(i - 1).Level.Add(new Level { Competence_ID = i, Level1 = (i * j).ToString(), Module_ID = i });
                }
            }
        }

        public List<Competence> GetAll()
        {
            return competenceList;
        }

        public Competence Get(int compentence_id)
        {
            return competenceList.Where(x => x.Competence_ID == compentence_id).First();
        }

        public Competence Create(Competence compentence)
        {
            competenceList.Add(compentence);
            return compentence;
        }

        public Competence Update(Competence compentence)
        {
            Competence Orginal = competenceList.Where(x => x.Competence_ID == compentence.Competence_ID).First();

            Orginal.Definition_Long = compentence.Definition_Long;


            return Orginal;
        }


        public void Delete(int compentence_id)
        {
            competenceList.RemoveAll(x => x.Competence_ID == compentence_id);
        }
    }
}

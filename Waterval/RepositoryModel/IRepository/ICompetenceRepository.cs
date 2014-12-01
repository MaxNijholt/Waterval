using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository
{
    public interface ICompetenceRepository
    {
        List<Competence> GetAll();
        Competence Get(int compentence_id);
        Competence Create(Competence compentence);
        void Delete(int compentence_id);
        Competence Update(Competence compentence);
    }
}

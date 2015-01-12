using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace RepositoryModel.IRepository
{
        public interface IProgramRepository
        {
            List<Program> GetAll();
            Program Get(int program_id);
            Program Create(Program program);
            void Delete(int program_id);
            Program Update(Program program);

        }   
}

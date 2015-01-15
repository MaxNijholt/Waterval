using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace RepositoryModel.IRepository {
	interface ISearchRepository {
		 List<Block> GetBlocksWith ( String find );
		 List<Competence> GetCompetencesWith ( String find );
		 List<Module> GetModulesWith ( String find );
		 List<LearnLine> GetLearnLinesWith ( String find );
		 List<Theme> GetThemesWith ( String find );
         List<Study> GetStudiesWith(string find);
	}
}

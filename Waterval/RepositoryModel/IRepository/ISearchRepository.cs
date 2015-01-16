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
         List<Account> GetAccountsWith(String find);
         List<AccountRole> GetAccountRolesWith(String find);
         List<AccountLaw> GetAccountLawsWith(String find);
		 List<Module> GetModulesWith ( String find );
		 List<LearnLine> GetLearnLinesWith ( String find );
		 List<Theme> GetThemesWith ( String find );
	}
}

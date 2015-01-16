using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using RepositoryModel.IRepository;

namespace RepositoryModel.Repository {
	public class SearchRepository : ISearchRepository {

		Project_WatervalEntities dbContext;

		public SearchRepository ( ) {
			dbContext = new DomainModel.Models.Project_WatervalEntities( );
		}

		public List<Block> GetBlocksWith ( String find ) {
			find = find.ToLower( );
			return dbContext.Block.Where( b => b.isDeleted == false && ( b.Title.ToLower( ).Contains( find ) ) ).ToList( );
		}

		public List<Competence> GetCompetencesWith ( String find ) {
			find = find.ToLower( );
			return dbContext.Competence.Where( b => b.isDeleted == false && ( b.Title.ToLower( ).Contains( find ) || b.Definition_Long.ToLower( ).Contains( find ) || b.Definition_Short.ToLower( ).Contains( find ) ) ).ToList( );
		}
        public List<AccountRole> GetAccountRolesWith(String find)
        {
            find = find.ToLower();
            return dbContext.AccountRole.Where(b => b.RoleName.ToLower( ).Contains( find ) || b.Description.ToLower( ).Contains( find )).ToList();
        }

        public List<AccountLaw> GetAccountLawsWith(String find)
        {
            find = find.ToLower();
            return dbContext.AccountLaw.Where(b => b.LawName.ToLower().Contains(find)).ToList();
        }

        public List<Account> GetAccountsWith(String find)
        {
            find = find.ToLower();
            return dbContext.Account.Where(b => b.isActive == true && b.Username.ToLower().Contains(find)).ToList();
        }

		public List<Module> GetModulesWith ( String find ) {
			find = find.ToLower( );
			return dbContext.Module.Where( b => b.isDeleted == false && ( b.Title.ToLower( ).Contains( find ) || b.CourseCode.ToLower( ).Contains( find ) || b.Definition_Long.ToLower( ).Contains( find ) || b.Definition_Short.ToLower( ).Contains( find ) || b.Foreknowledge.ToLower( ).Contains( find ) ) ).ToList( );
		}

		public List<LearnLine> GetLearnLinesWith ( String find ) {
			find = find.ToLower( );
			return dbContext.LearnLine.Where( b => b.isDeleted == false && ( b.Title.ToLower( ).Contains( find ) || b.Definition.ToLower( ).Contains( find ) ) ).ToList( );
		}

		public List<Theme> GetThemesWith ( String find ) {
			find = find.ToLower( );
			return dbContext.Theme.Where( b => b.isDeleted == false && ( b.Title.ToLower( ).Contains( find ) || b.Definition.ToLower( ).Contains( find ) ) ).ToList( );
		}
	}
}

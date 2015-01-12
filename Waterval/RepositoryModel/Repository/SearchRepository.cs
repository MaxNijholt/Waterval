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
			return dbContext.Block.Where( b => b.isDeleted == false && ( b.Title.Contains( find ) || b.Title.Contains( find.ToLower( ) ) ) ).ToList( );
		}

		public List<Competence> GetCompetencesWith ( String find ) {
			return dbContext.Competence.Where( b => b.isDeleted == false && ( b.Title.Contains( find ) || b.Definition_Long.Contains( find ) || b.Definition_Short.Contains( find )
				|| b.Title.Contains( find.ToLower( ) ) || b.Definition_Long.Contains( find.ToLower( ) ) || b.Definition_Short.Contains( find.ToLower( ) ) ) ).ToList( );
		}

		public List<Module> GetModulesWith ( String find ) {
			return dbContext.Module.Where( b => b.isDeleted == false && ( b.Title.Contains( find ) || b.CourseCode.Contains( find ) || b.Definition_Long.Contains( find ) || b.Definition_Short.Contains( find ) || b.Foreknowledge.Contains( find )
				 || b.Title.Contains( find.ToLower( ) ) || b.CourseCode.Contains( find.ToLower( ) ) || b.Definition_Long.Contains( find.ToLower( ) ) || b.Definition_Short.Contains( find.ToLower( ) ) || b.Foreknowledge.Contains( find.ToLower( ) ) ) ).ToList( );
		}

		public List<LearnLine> GetLearnLinesWith ( String find ) {
			return dbContext.LearnLine.Where( b => b.isDeleted == false && ( b.Title.Contains( find ) || b.Definition.Contains( find ) || b.Title.Contains( find.ToLower( ) ) || b.Definition.Contains( find.ToLower( ) ) ) ).ToList( );
		}

		public List<Theme> GetThemesWith ( String find ) {
			return dbContext.Theme.Where( b => b.isDeleted == false && ( b.Title.Contains( find ) || b.Definition.Contains( find ) || b.Title.Contains( find.ToLower( ) ) || b.Definition.Contains( find.ToLower( ) ) ) ).ToList( );
		}
	}
}

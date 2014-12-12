using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel {
	class SearchRepository {

		Project_WatervalEntities dbContext;

		public SearchRepository ( ) {
			dbContext = new DomainModel.Models.Project_WatervalEntities( );
		}

		public List<Block> GetBlocksWith ( String find ) {
			List<Block> notDeleted = dbContext.Block.Where( b => b.isDeleted == false ).ToList( );
			return (List<Block>)( from val in notDeleted where ( val.Title.Contains( "%" + find + "%" ) ) select val );

		}

		public List<Competence> GetCompetencesWith ( String find ) {
			List<Competence> notDeleted = dbContext.Competence.Where( b => b.isDeleted == false ).ToList( );
			return (List<Competence>)( from val in notDeleted
									   where ( val.Definition_Long.Contains( "%" + find + "%" )
										   || val.Definition_Short.Contains( "%" + find + "%" )
										   || val.Title.Contains( "%" + find + "%" ) )
									   select val );
		}

		public List<Module> GetModulesWith ( String find ) {
			List<Module> notDeleted = dbContext.Module.Where( b => b.isDeleted == false ).ToList( );
			return (List<Module>)( from val in notDeleted
								   where ( val.Title.Contains( "%" + find + "%" )
								   || val.CourseCode.Contains( "%" + find + "%" )
								   || val.Definition_Long.Contains( "%" + find + "%" )
								   || val.Definition_Short.Contains( "%" + find + "%" )
								   || val.Foreknowledge.Contains( "%" + find + "%" )
								   )
								   select val );
		}

		public List<LearnLine> GetLearnLinesWith ( String find ) {
			List<LearnLine> notDeleted = dbContext.LearnLine.Where( b => b.isDeleted == false ).ToList( );
			return (List<LearnLine>)( from val in notDeleted
									  where ( val.Title.Contains( "%" + find + "%" )
									  || val.Definition.Contains( "%" + find + "%" )
									  )
									  select val );
		}

		public List<Theme> GetThemesWith ( String find ) {
			List<Theme> notDeleted = dbContext.Theme.Where( b => b.isDeleted == false ).ToList( );
			return (List<Theme>)( from val in notDeleted
								  where ( val.Title.Contains( "%" + find + "%" )
								  || val.Definition.Contains( "%" + find + "%" )
								  )
								  select val );
		}
	}
}

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
			return dbContext.Block.Where( b => b.isDeleted == false && b.Title.Contains(find) ).ToList( );
			//List<Block> returnValues = (List<Block>)( from val in notDeleted where ( val.Title.Contains( "%" + find + "%" ) ) select val ).ToList();
            //return notDeleted;

		}

		public List<Competence> GetCompetencesWith ( String find ) {
			return dbContext.Competence.Where( b => b.isDeleted == false && ( b.Title.Contains(find) || b.Definition_Long.Contains(find) || b.Definition_Short.Contains(find) ) ).ToList( );
			//return (List<Competence>)( from val in notDeleted
			//						   where ( val.Definition_Long.Contains( "%" + find + "%" )
			//							   || val.Definition_Short.Contains( "%" + find + "%" )
			//							   || val.Title.Contains( "%" + find + "%" ) )
			//						   select val );
		}

		public List<Module> GetModulesWith ( String find ) {
			return dbContext.Module.Where( b => b.isDeleted == false && ( b.Title.Contains(find) || b.CourseCode.Contains(find) || b.Definition_Long.Contains(find) || b.Definition_Short.Contains(find) || b.Foreknowledge.Contains(find) )).ToList( );
			//return (List<Module>)( from val in notDeleted
			//					   where ( val.Title.Contains( "%" + find + "%" )
			//					   || val.CourseCode.Contains( "%" + find + "%" )
			//					   || val.Definition_Long.Contains( "%" + find + "%" )
			//					   || val.Definition_Short.Contains( "%" + find + "%" )
			//					   || val.Foreknowledge.Contains( "%" + find + "%" )
			//					   )
			//					   select val );
		}

		public List<LearnLine> GetLearnLinesWith ( String find ) {
            return dbContext.LearnLine.Where(b => b.isDeleted == false && (b.Title.Contains(find) || b.Definition.Contains(find) )).ToList();

			//List<LearnLine> notDeleted = dbContext.LearnLine.Where( b => b.isDeleted == false ).ToList( );
		//	return (List<LearnLine>)( from val in notDeleted
		//							  where ( val.Title.Contains( "%" + find + "%" )
		//							  || val.Definition.Contains( "%" + find + "%" )
		//							  )
		//							  select val );
        //    return notDeleted;
		}

		public List<Theme> GetThemesWith ( String find ) {
			return dbContext.Theme.Where( b => b.isDeleted == false && ( b.Title.Contains(find) || b.Definition.Contains(find))).ToList( );
			//return (List<Theme>)( from val in notDeleted
			//					  where ( val.Title.Contains( "%" + find + "%" )
			//					  || val.Definition.Contains( "%" + find + "%" )
			//					  )
			//					  select val );
		}
	}
}

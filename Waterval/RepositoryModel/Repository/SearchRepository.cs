using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository {
	class SearchRepository : ISearchRepository {

		Project_WatervalEntities dbContext;

		public SearchRepository ( ) {
			dbContext = new DomainModel.Models.Project_WatervalEntities( );
		}
		public List<Module> GetAllModules ( ) {
			return dbContext.Module.Where( b => b.isDeleted == false ).ToList( );
		}

		public Module GetModuleByID ( int module_id ) {
			return dbContext.Module.Find( module_id );
		}

		public List<Competence> GetAllCompetences ( ) {
			return dbContext.Competence.Where( b => b.isDeleted == false ).ToList( );
		}

		public Competence GetCompetenceByID ( int compentence_id ) {
			return dbContext.Competence.Find( compentence_id );
		}

		public List<Block> GetAllBlocks ( ) {
			return dbContext.Block.Where( b => b.isDeleted == false ).ToList( );
		}

		public Block GetBlockByID ( int id ) {
			return dbContext.Block.Find( id );
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
	}
}

using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository {
	public class CompetenceRepository : ICompetenceRepository {
		Project_WatervalEntities dbContext;

		public CompetenceRepository ( ) {
			dbContext = new DomainModel.Models.Project_WatervalEntities( );
		}
		public List<DomainModel.Models.Competence> GetAll ( ) {
			return dbContext.Competence.Where( b => b.isDeleted == false ).ToList( );
		}

		public DomainModel.Models.Competence Get ( int compentence_id ) {
			return dbContext.Competence.Find( compentence_id );
		}

		public DomainModel.Models.Competence Create ( DomainModel.Models.Competence compentence ) {
			dbContext.Competence.Add( compentence );
			dbContext.SaveChanges( );
			return compentence;
		}

		public void Delete ( int compentence_id ) {
			Competence compentence = dbContext.Competence.SingleOrDefault( c => c.Competence_ID == compentence_id );
			compentence.isDeleted = true;
			compentence.DeleteDate = DateTime.UtcNow;
			dbContext.SaveChanges( );
		}

		public DomainModel.Models.Competence Update ( DomainModel.Models.Competence update ) {
			Competence competence = dbContext.Competence.SingleOrDefault( b => b.Competence_ID == update.Competence_ID );
			if ( competence == null )
				return null;

			competence.Definition_Long = update.Definition_Long;
			competence.Definition_Short = update.Definition_Short;
			competence.Title = update.Title;
			dbContext.SaveChanges( );
			return competence;
		}
	}
}

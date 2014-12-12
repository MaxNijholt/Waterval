using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace RepositoryModel {
	interface ISearchRepository {
		List<Module> GetAllModules ( );
		Module GetModuleByID ( int module_id );
		List<Competence> GetAllCompetences ( );
		Competence GetCompetenceByID ( int compentence_id );
		List<Block> GetAllBlocks ( );
		Block GetBlockByID ( int block_id );
	}
}

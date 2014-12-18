using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository
{
	public interface IPhasingRepository
	{
		List<Phasing> GetAll();
		Phasing Get(int phasing_id);
		Phasing Create(Phasing phasing);
		void Delete(int phasing_id);
		Phasing Update(Phasing phasing);
	}
}

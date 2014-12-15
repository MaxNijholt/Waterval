using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
	public class DPhasingRepository
	{
		public List<Phasing> GetAll()
		{
			List<Phasing> fakePhasings = new List<Phasing>()
			{
			new Phasing{Phasing_ID = 1, DeleteDate = DateTime.Now, isDeleted = false, Title = "Propedeuse"},
			new Phasing{Phasing_ID  = 2, DeleteDate  = DateTime.Now, isDeleted = false, Title = "Minor"}
			};
			return fakePhasings;
		}
	}
}

using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository
{
	class PhasingRepository : IPhasingRepository
	{
		Project_WatervalEntities dbContext;
		public PhasingRepository()
		{
			dbContext = new
				DomainModel.Models.Project_WatervalEntities();
		}
		public List<DomainModel.Models.Phasing> GetAll()
		{
			return dbContext.Phasing.Where(p => p.isDeleted == false).ToList();
		}

		public DomainModel.Models.Phasing Get(int phasing_id)
		{
			return dbContext.Phasing.Find(phasing_id);
		}

		public DomainModel.Models.Phasing Create(DomainModel.Models.Phasing phasing)
		{
			if(dbContext.Phasing.Any(o => o.Phasing_ID == phasing.Phasing_ID && !o.isDeleted))
				return null;
			dbContext.Phasing.Add(phasing);
			dbContext.SaveChanges();
			return phasing;
		}

		public void Delete(int phasing_id)
		{
			Phasing phasing = dbContext.Phasing.SingleOrDefault(b => b.Phasing_ID == phasing_id);

			phasing.isDeleted = true;
			phasing.DeleteDate = DateTime.UtcNow;
			dbContext.SaveChanges();
		}

		public DomainModel.Models.Phasing Update(DomainModel.Models.Phasing update)
		{
			Phasing phasing = dbContext.Phasing.SingleOrDefault(b => b.Phasing_ID == update.Phasing_ID);
			if(phasing == null)
				return null;
			phasing.Title = update.Title;
			dbContext.SaveChanges();
			return phasing;
		}
	}
}

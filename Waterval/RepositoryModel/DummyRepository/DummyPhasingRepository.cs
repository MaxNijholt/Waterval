using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DummyPhasingRepository: IPhasingRepository
    {
		List<Phasing> phasings;

		public DummyPhasingRepository()
        {
            FillList();
        }

		public List<Phasing> GetAll()
        {
            return phasings;
        }

		public Phasing Get(int phasing_id)
        {
			return phasings.Where(x => x.Phasing_ID == phasing_id).First();
        }

		public Phasing Create(Phasing phasing)
        {
            phasings.Add(phasing);
            return phasing;
        }

        public void Delete(int phasing_id)
        {
            Phasing f = phasings.Where(x => x.Phasing_ID == phasing_id).First();
            f.isDeleted = true;
            f.DeleteDate = DateTime.UtcNow;
        }

		public Phasing Update(Phasing phasing)
        {
			Phasing f = phasings.Where(x => x.Phasing_ID == phasing.Phasing_ID).First();
            f.Title = phasing.Title;
            return f;
        }

        private void FillList()
        {
			phasings = new List<Phasing>();

            for(int i = 1; i <= 10; i++)
                phasings.Add(new Phasing{ Phasing_ID = i, Title  = "Phasing " + i });
        }
    }
}

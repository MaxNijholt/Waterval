using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DummyWorkformRepository: IWorkformRepository
    {
        List<Workform> workforms;

        public DummyWorkformRepository()
        {
            FillList();
        }

        public List<Workform> GetAll()
        {
            return workforms;
        }

        public Workform Get(int workform_id)
        {
            return workforms.Where(x => x.Workform_ID == workform_id).First();
        }

        public Workform Create(Workform workform)
        {
            workforms.Add(workform);
            return workform;
        }

        public void Delete(int workform_id)
        {
            Workform f =workforms.Where(x => x.Workform_ID == workform_id).First();
            f.isDeleted = true;
            f.DeleteDate = DateTime.UtcNow;
        }

        public Workform Update(Workform workform)
        {
            Workform f = workforms.Where(x => x.Workform_ID == workform.Workform_ID).First();

            f.Description = workform.Description;

            return f;
        }

        private void FillList()
        {
            workforms = new List<Workform>();

            for(int i = 1; i <= 10; i++)
                workforms.Add(new Workform { Workform_ID = i, Description = "Workform " + i });
        }
    }
}

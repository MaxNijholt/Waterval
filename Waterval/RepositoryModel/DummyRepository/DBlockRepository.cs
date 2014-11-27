using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
  public  class DBlockRepository : IBlockRepository
    {
        public List<DomainModel.Models.Block> GetAll()
        {
            List<Block> fakeblocks = new List<Block>()
            {
                new Block{ Block_ID = 1, Title = "Dummy Blok 1",isDeleted = false, DeleteDate=null},
                new Block{ Block_ID = 2, Title = "Dummy Blok 2",isDeleted = false, DeleteDate=null},
                new Block{ Block_ID = 3, Title = "Dummy Blok 3",isDeleted = false, DeleteDate=null},
                new Block{ Block_ID = 4, Title = "Dummy Blok 4",isDeleted = false, DeleteDate=null},
                new Block{ Block_ID = 5, Title = "Dummy Blok 5",isDeleted = false, DeleteDate=null},
                new Block{ Block_ID = 6, Title = "Dummy Blok 6",isDeleted = false, DeleteDate=null},
                new Block{ Block_ID = 7, Title = "Dummy Blok 7",isDeleted = false, DeleteDate=null},
                new Block{ Block_ID = 8, Title = "Dummy Blok 8",isDeleted = false, DeleteDate=null}
            };
            return fakeblocks;

        }

        public IQueryable<Block> Block
        {
            get { return (IQueryable<Block>)GetAll(); }
        }


        public DomainModel.Models.Block Get(int block_id)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Block Create(DomainModel.Models.Block block)
        {
            throw new NotImplementedException();
        }

        public void Delete(int block_id)
        {
            throw new NotImplementedException();
        }

        public DomainModel.Models.Block Update(DomainModel.Models.Block block)
        {
            throw new NotImplementedException();
        }
    }
}

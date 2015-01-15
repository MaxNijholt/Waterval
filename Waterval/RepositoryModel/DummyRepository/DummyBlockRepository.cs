using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
  public  class DummyBlockRepository : IBlockRepository
    {
      List<Block> fakeblocks;
      public DummyBlockRepository()
      {
          CreateList();
          
      }
        public List<DomainModel.Models.Block> GetAll()
        {
          
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

      private void CreateList()
        {  fakeblocks = new List<Block>();

          for(int i = 1; i <= 10; i++)
              fakeblocks.Add( new Block{ Block_ID = i, Title = "Dummy Blok " + i});
         
        }
    }
}

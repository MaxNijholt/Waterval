using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.DummyRepository
{
    public class DummyBlockRepository : IBlockRepository
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


        public Block Get(int block_id)
        {
            return fakeblocks.Where(x => x.Block_ID==block_id).First();
        }

        public Block Create(Block block)
        {
            fakeblocks.Add(block);

            return block;
        }

        public void Delete(int block_id)
        {
            Block delete = fakeblocks.Where(x => x.Block_ID == block_id).First();

            delete.isDeleted = true;
            delete.DeleteDate = DateTime.UtcNow;


        }

        public Block Update(Block block)
        {
            Block update = fakeblocks.Where(x => x.Block_ID == block.Block_ID).First();

            update.Title = block.Title;

            return update;
        }

        private void CreateList()
        {
            fakeblocks = new List<Block>();

            for (int i = 1; i <= 10; i++)
                fakeblocks.Add(new Block { Block_ID = i, Title = "Dummy Blok " + i });

        }
    }
}

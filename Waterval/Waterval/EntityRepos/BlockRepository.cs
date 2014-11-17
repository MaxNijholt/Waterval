using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Waterval.Models;
/*using DomainModel.Repository;
using DomainModel.Models;*/
namespace Waterval.EntityRepos
{
    public class BlockRepository //: IBlockRepository
    {
        Project_WatervalEntities1 dbContext;
        

        public BlockRepository()
        {
            dbContext = new Project_WatervalEntities1(); ;
        }

        public List<Block> GetAll()
        {
            return dbContext.Block.ToList();
        }

        public Block Get(int id)
        {
            return dbContext.Block.Find(id);
        }

        public Block Create(Block block)
        {
            throw new NotImplementedException();
        }

        public Block Update(Block block)
        {
            throw new NotImplementedException();
        }

        public void Delete(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
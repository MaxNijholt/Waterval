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
            if (dbContext.Block.Any(o => o.Block_ID == block.Block_ID && !o.isDeleted))
                return null;
            dbContext.Block.Add(block);
            dbContext.SaveChanges();
            return block;
        }

        public Block Update(Block update)
        {
       
            Block blok = dbContext.Block.SingleOrDefault(b => b.Block_ID == update.Block_ID);
            if (blok == null) return null;
            blok.Title = update.Title;
            dbContext.SaveChanges();
            return blok;
        }

        public void Delete(int id)
        {
            Block blok = dbContext.Block.SingleOrDefault(b => b.Block_ID == id);
         
            blok.isDeleted = true;
            dbContext.SaveChanges();
      
        }
    }
}
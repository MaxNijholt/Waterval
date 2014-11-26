using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Models;
using System.Collections;

namespace RepositoryModel
{
    public class BlockRepository : IBlockRepository
    {
        Project_WatervalEntities dbContext;


        public BlockRepository()
        {
            dbContext = new DomainModel.Models.Project_WatervalEntities();
            dbContext.Database.Initialize(true);
        }

        public List<Block> GetAll()
        {
            return dbContext.Block.Where(b => b.isDeleted == false).ToList();

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
            blok.DeleteDate = DateTime.UtcNow;
            dbContext.SaveChanges();

        }

    }
}
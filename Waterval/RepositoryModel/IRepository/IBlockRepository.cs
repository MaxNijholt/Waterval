using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;

namespace RepositoryModel
{
    public interface IBlockRepository
    {
        List<Block> GetAll();
        Block Get(int block_id);
        Block Create(Block block);
        void Delete(int block_id);
        Block Update(Block block);
 
    }
}

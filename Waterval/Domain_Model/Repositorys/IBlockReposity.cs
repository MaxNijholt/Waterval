using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Model.Repositorys
{
    public interface IBlockReposity
    {
        Block Get(int id);
        List<Block> GetAll();
        Block Create(Block block);
        Block Update(Block block);
        void Delete(Block block);
    }
}

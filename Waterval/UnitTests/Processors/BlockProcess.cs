using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryModel;
using DomainModel.Models;
namespace UnitTests
{

    public class BlockProcessor
    {
        private IBlockRepository blockRepository;

        public BlockProcessor(IBlockRepository blockRepository)
        {
            this.blockRepository = blockRepository;
        }

        public List<Block> GetCurrentBlock()
        {
            return blockRepository.GetAll();
        }
    }

}


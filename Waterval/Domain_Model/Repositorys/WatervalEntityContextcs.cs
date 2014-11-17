using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Model.Repositorys
{
   public  class WatervalEntityContext: Project_WatervalEntities1
    {
        public DbSet<Block> Blocks { get; set; }
    }
}

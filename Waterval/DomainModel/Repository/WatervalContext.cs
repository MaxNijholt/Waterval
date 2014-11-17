using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Models;
using System.Data.Entity;
namespace DomainModel.Repository
{
public    class WatervalContext: Project_WatervalEntities
    {
        public Block Blocks { get; set; }
    }
}

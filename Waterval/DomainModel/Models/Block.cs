//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Block
    {
        public Block()
        {
            this.ModuleStudyPhasingBlock = new HashSet<ModuleStudyPhasingBlock>();
        }
    
        public int Block_ID { get; set; }
        public string Title { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    
        public virtual ICollection<ModuleStudyPhasingBlock> ModuleStudyPhasingBlock { get; set; }
    }
}

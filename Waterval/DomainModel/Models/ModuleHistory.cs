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
    
    public partial class ModuleHistory
    {
        public ModuleHistory()
        {
            this.Module = new HashSet<Module>();
        }
    
        public int ModuleYear_ID { get; set; }
    
        public virtual ICollection<Module> Module { get; set; }
    }
}

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
    
    public partial class Study
    {
        public Study()
        {
            this.ModuleStudyPhasingBlock = new HashSet<ModuleStudyPhasingBlock>();
            this.Module = new HashSet<Module>();
            this.Program = new HashSet<Program>();
        }
    
        public int Study_ID { get; set; }
        public string Title { get; set; }
        public string Definition { get; set; }
        public string StudyCode { get; set; }
        public string level { get; set; }
        public bool isDeleted { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    
        public virtual ICollection<ModuleStudyPhasingBlock> ModuleStudyPhasingBlock { get; set; }
        public virtual ICollection<Module> Module { get; set; }
        public virtual ICollection<Program> Program { get; set; }
    }
}

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
    
    public partial class Account
    {
        public Account()
        {
            this.Module = new HashSet<Module>();
        }
    
        public int Account_ID { get; set; }
        public string Username { get; set; }
        public int Role_ID { get; set; }
        public bool isActive { get; set; }
    
        public virtual AccountRole AccountRole { get; set; }
        public virtual ICollection<Module> Module { get; set; }
    }
}

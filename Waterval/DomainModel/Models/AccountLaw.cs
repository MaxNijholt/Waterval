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
    
    public partial class AccountLaw
    {
        public AccountLaw()
        {
            this.AccountRole = new HashSet<AccountRole>();
        }
    
        public int Law_ID { get; set; }
        public string LawName { get; set; }
    
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}

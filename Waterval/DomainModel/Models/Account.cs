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
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isSuperUser { get; set; }
        public bool isDeleted { get; set; }
        public string Salt { get; set; }
    }
}

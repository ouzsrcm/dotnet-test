//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yesil.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerCredential
    {
        public int CustomerCredentialId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}

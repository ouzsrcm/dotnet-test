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
    
    public partial class FacilityImage
    {
        public int FacilityImageId { get; set; }
        public Nullable<int> FacilityId { get; set; }
        public string Image { get; set; }
    
        public virtual Facility Facility { get; set; }
    }
}
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
    
    public partial class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<decimal> SoldPrice { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Unit Unit { get; set; }
    }
}

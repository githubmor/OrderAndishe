//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrdersAndisheh.DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pallet
    {
        public Pallet()
        {
            this.Product = new HashSet<Product>();
        }
    
        public byte Id { get; set; }
        public string Name { get; set; }
        public Nullable<byte> Vazn { get; set; }
    
        public virtual ICollection<Product> Product { get; set; }
    }
}

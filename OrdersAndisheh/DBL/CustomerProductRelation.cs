namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerProductRelation")]
    public class CustomerProductRelation
    {

        public CustomerProductRelation()
        {
            Customers = new HashSet<Customer>();
            Products = new HashSet<Product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool HasOracle { get; set; }
        public bool HasBarnameDasti { get; set; }
        public bool HasBarchasb { get; set; }
        public bool HasDasteTolidi { get; set; }
        public bool HasLabelPrint { get; set; }
        public bool HasHamloNaghl { get; set; }
        public bool HasMoshtariASN { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

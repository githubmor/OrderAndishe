namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public  class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            CustomerRelations = new HashSet<CustomerProductRelation>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public double? Weight { get; set; }

        public bool IsImenKala { get; set; }

        
        public string FaniCode { get; set; }

        
        public string CodeJense { get; set; }

        public int? TedadDarPallet { get; set; }

        public int? TedadDarSabad { get; set; }

        public int PalletId { get; set; }

        public int Bazres_Id { get; set; }

        public virtual Bazres Bazre { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        //public int RelationId { get; set; }
        public virtual ICollection<CustomerProductRelation> CustomerRelations { get; set; }

        public virtual Pallet Pallet { get; set; }
    }
}

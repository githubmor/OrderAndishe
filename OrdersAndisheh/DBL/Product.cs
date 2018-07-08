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
            //CustomerRelations = new HashSet<CustomerProductRelation>();
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

        [StringLength(150)]
        public string FaniCode { get; set; }

        [StringLength(150)]
        public string CodeJense { get; set; }

        public string BasteBandi { get; set; }

        public short? TedadDarPallet { get; set; }

        public short? TedadDarSabad { get; set; }

        public int PalletId { get; set; }
        public int? BasteId { get; set; }

        public int Bazres_Id { get; set; }

        public virtual Bazres Bazre { get; set; }
        public virtual ICollection<KhodroProductRelation> KhodrosRelation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        //public int? RelationId { get; set; }
        public virtual ICollection<CustomerProductRelation> Relations { get; set; }
        public virtual ICollection<OracleRelation> OracleRelations { get; set; }

        public virtual Pallet Pallet { get; set; }
        public virtual Baste Baste { get; set; }
        [InverseProperty("Product")]
        public virtual Amount Amount { get; set; }

        
    }
}

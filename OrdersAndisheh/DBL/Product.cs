namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public short Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Nam { get; set; }

        public double? Weight { get; set; }

        public bool IsImenKala { get; set; }

        [StringLength(15)]
        public string FaniCode { get; set; }

        [StringLength(20)]
        public string CodeJense { get; set; }

        public short? TedadDarPallet { get; set; }

        public short? TedadDarSabad { get; set; }

        public byte PalletId { get; set; }

        public byte Bazres_Id { get; set; }

        public virtual Bazre Bazre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Pallet Pallet { get; set; }
    }
}

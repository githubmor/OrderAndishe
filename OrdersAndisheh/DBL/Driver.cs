namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Driver")]
    public class Driver
    {
        public Driver()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [Required]
        public string Nam { get; set; }

        [StringLength(50)]
        public string Tel1 { get; set; }

        [StringLength(50)]
        public string Tel2 { get; set; }

        [StringLength(10)]
        public string Pelak { get; set; }

        [StringLength(20)]
        public string Car { get; set; }

        public byte? Ton { get; set; }

        public byte? Tol { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

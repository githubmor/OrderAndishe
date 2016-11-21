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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Tel1 { get; set; }

        public string Tel2 { get; set; }

        [StringLength(10)]
        public string Pelak { get; set; }

        [StringLength(20)]
        public string Car { get; set; }

        public int? Ton { get; set; }

        public int? Tol { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        //public int? TempDriverId { get; set; }
        public virtual TempDriver TempDriver { get; set; }

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}

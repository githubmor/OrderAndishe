namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public class OrderDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Tedad { get; set; }

        public int TahvilForosh { get; set; }

        public string Description { get; set; }

        public short ProductId { get; set; }

        public short CustomerId { get; set; }

        public int DriverId { get; set; }

        public int OrderId { get; set; }

        public bool HasOracle { get; set; }

        public int ItemType { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}

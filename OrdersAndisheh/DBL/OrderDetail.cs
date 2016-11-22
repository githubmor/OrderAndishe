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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ushort Tedad { get; set; }

        public ushort TahvilForosh { get; set; }

        public string Description { get; set; }

        public int ProductId { get; set; }


        public int? Customer_Id { get; set; }

        
        public int? Driver_Id { get; set; }

        public int OrderId { get; set; }

        public byte ItemType { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
        public virtual MOracle MOracle { get; set; }
    }
}

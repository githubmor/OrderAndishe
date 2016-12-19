namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("MOracle")]
    public class MOracle
    {
       
        public int Id { get; set; }

        [StringLength(10)]
        public string MNumber { get; set; }
        [StringLength(500)]
        public string Descrioption { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}

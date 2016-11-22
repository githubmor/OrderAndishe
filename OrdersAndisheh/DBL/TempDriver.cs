namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TempDriver")]
    public class TempDriver
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
        public virtual Driver Driver { get; set; }
    }
}

namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhodroProductRelation")]
    public class KhodroProductRelation
    {

        public KhodroProductRelation()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int KhodroId { get; set; }
        public virtual Khodro Khodro { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

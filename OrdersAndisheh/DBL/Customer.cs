namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public class Customer
    {
        
        public Customer()
        {
            OrderDetails = new HashSet<OrderDetail>();
            //ProductRelations = new HashSet<CustomerProductRelation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsTolidLine { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CustomerProductRelation> Relations { get; set; }
        public virtual ICollection<OracleRelation> OracleRelations { get; set; }

        //public int? TarafId { get; set; }
        //public virtual Taraf Tamin { get; set; }

       
    }
}

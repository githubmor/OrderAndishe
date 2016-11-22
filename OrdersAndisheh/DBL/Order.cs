namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int FullName
        {
            get { return calId(); }
            private set
            {
                //Just need this here to trick EF
            }
        }
        

        [StringLength(10)]
        public string Tarikh { get; set; }
        public bool Accepted { get; set; }
        public string Description { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        private int calId()
        {
            if (!string.IsNullOrEmpty(Tarikh))
            {
                var p = PersianDateTime.Parse(Tarikh);
                return (p.Year * 10000) + (p.Month * 100) + (p.Day);
            }
            else
            {
                return 0;
            }
            
            
        }
    }
}

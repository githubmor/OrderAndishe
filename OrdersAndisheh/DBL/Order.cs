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

        
        public int Id { get; set; }
        //{
        //    get;
        //    private set;
        //}

        [StringLength(10)]
        public string Tarikh { get; set; }
        //{
        //    get { return ;}
        //    set
        //    {
        //        var t = PersianDateTime.Parse(value);
        //        Id = t.Year + t.Month + t.Day;
        //    }
        //}

        public bool Accepted { get; set; }

        public string Description { get; set; }

       
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

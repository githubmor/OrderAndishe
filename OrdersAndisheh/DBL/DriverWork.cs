
namespace OrdersAndisheh.DBL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("DriverWork")]
    public class DriverWork
    {
       
        [ForeignKey("OrderDetail")]
        public int Id { get; set; }

        public string Works { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}

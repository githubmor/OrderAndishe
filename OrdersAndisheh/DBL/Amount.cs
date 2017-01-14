using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.DBL
{
    public class Amount
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }
        public double LastAmount { get; set; }
        public virtual Product Product { get; set; }
    }
}

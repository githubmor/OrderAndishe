using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersAndisheh.DBL
{
    
    public class Amount
    {
        
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        public double LastAmount { get; set; }
        [ForeignKey("ProductId")]
        [InverseProperty("Amount")]
        public virtual Product Product { get; set; }
    }
}

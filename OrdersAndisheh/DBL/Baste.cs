namespace OrdersAndisheh.DBL
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Baste")]
    public class Baste
    {
        public Baste()
        {
            Products = new HashSet<Product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}

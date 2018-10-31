namespace OrdersAndisheh.DBL
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Taraf")]
    public class Taraf
    {
        public Taraf()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        //public virtual ICollection<Customer> Anbars { get; set; }
        public virtual ICollection<Khodro> Khodros { get; set; }
    }
}

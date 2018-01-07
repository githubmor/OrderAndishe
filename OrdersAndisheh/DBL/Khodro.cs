namespace OrdersAndisheh.DBL
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Khodro")]
    public class Khodro
    {

        public Khodro()
        {
            Tolids = new HashSet<AmarTolidKhodro>();
            //ProductRelations = new HashSet<CustomerProductRelation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<KhodroProductRelation> KhodroProductRelation { get; set; }

        public virtual ICollection<AmarTolidKhodro> Tolids { get; set; }
    }
}

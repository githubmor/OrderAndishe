namespace OrdersAndisheh.DBL
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AmarTolidKhodro")]
    public class AmarTolidKhodro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TedadTolid { get; set; }
        [StringLength(10)]
        public string Tarikh { get; set; }
        public virtual Khodro Khodro { get; set; }
    }
}

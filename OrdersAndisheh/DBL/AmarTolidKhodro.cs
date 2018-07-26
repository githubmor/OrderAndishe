namespace OrdersAndisheh.DBL
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AmarTolidKhodro")]
    public class AmarTolidKhodro
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }
        public int TedadTolid { get; set; }
        public virtual Khodro Khodro { get; set; }
        //[Key, Column(Order = 0)]
        //public int Id { get; set; }
        //public int TedadTolid { get; set; }
        [Key, Column(Order = 0)]
        [StringLength(6)]
        public string SalMah { get; set; }
        //public string Tarikh { get; set; }
        [Key, Column(Order = 1)]
        public int KhodroId { get; set; }
        //public virtual Khodro Khodro { get; set; }
    }
}

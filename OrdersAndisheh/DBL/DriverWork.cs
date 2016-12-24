
namespace OrdersAndisheh.DBL
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DriverWork")]
    public class DriverWork
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverWorkId { get; set; }

        public string Works { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}

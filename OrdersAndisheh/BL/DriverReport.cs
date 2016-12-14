using BL;
using OrdersAndisheh.DBL;
using System.Collections.Generic;

namespace OrdersAndisheh.BL
{
    public class DriverReport
    {

        public DriverReport(string tarikh,Driver driver,List<ItemSefaresh> items)
        {
            this.Tarikh = tarikh;
            this.Driver = driver;
            //this.Items = items; 
        }

        private string tarikh;

        public string Tarikh
        {
            get { return tarikh; }
            set { tarikh = value; }
        }

        private Driver Driver;

        public string DriverName
        {
            get { return Driver.Name ; }
            //set { myVar = value; }
        }
        
        
        //public string Tarikh { get; set; }

        //public Driver Driver { get; set; }

        //public List<ItemSefaresh> Items { get; set; }
    }
}

using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class DriverWorksDto
    {
        private DriverWork driverWorks;
        
        public DriverWorksDto(DriverWork dr)
        {
            this.driverWorks = dr;
        }

        //private string myVar;

        public string Ranande
        {
            get { return driverWorks.Driver.Name; }
            //set { myVar = value; }
        }

        public string Works
        {
            get { return driverWorks.Works; }
            //set { myVar = value; }
        }

        
    }
}

using GalaSoft.MvvmLight;
using OrdersAndisheh.DBL;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{
    
    public class DriverWorksViewModel : ViewModelBase
    {
        
        public DriverWorksViewModel(List<Driver> drivers)
        {
            this.Drivers = drivers;
        }

        public List<Driver> Drivers { get; set; }

        private Driver myVar;   

        public Driver SelectedDriver
        {
            get { return myVar; }
            set { myVar = value; }
        }

        ////private string myVar;

        //public string Works
        //{
        //    get { return SelectedDriver.; }
        //    set { myVar = value; }
        //}
        
    }
}
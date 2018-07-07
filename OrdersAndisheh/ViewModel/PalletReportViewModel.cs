using BL;
using GalaSoft.MvvmLight;
using OrdersAndisheh.BL;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{

    public class PalletReportViewModel : ViewModelBase
    {
        public PalletReportViewModel(int year)
        {
            SefareshService s = new SefareshService();
            Pallets = s.LoadAllPalletReport(year);
        }
                
        private List<PalletItem> myVar;

        public List<PalletItem> Pallets
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
    }
}
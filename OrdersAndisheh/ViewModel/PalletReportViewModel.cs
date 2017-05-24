using BL;
using GalaSoft.MvvmLight;
using OrdersAndisheh.BL;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{

    public class PalletReportViewModel : ViewModelBase
    {
        public PalletReportViewModel()
        {
            SefareshService s = new SefareshService();
            Pallets = s.LoadAllPalletReport(PersianDateTime.Now.Year);
        }
                
        private List<PalletItem> myVar;

        public List<PalletItem> Pallets
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
    }
}
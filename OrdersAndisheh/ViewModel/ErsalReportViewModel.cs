using BL;
using GalaSoft.MvvmLight;
using OrdersAndisheh.BL;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{

    public class ErsalReportViewModel : ViewModelBase
    {
        public ErsalReportViewModel()
        {
            SefareshService s = new SefareshService();
            ItemSefareshes = s.LoadAllSefaresh(PersianDateTime.Now.Year);
        }
                
        private List<ErsalItem> myVar;

        public List<ErsalItem> ItemSefareshes
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
    }
}
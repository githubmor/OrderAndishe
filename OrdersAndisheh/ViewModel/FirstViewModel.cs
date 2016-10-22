using BL;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{
    public class FirstViewModel : ViewModelBase
    {
        SefareshService ss;
        public FirstViewModel()
        {
            ss = new SefareshService();

        }

        
        
    }

    public class NodeSerfaresh
    {
        public string Month { get; set; }
        public List<string> Roz { get; set; }
    }
}
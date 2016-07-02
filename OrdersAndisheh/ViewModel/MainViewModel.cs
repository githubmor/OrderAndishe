using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrdersAndisheh.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
       
        public MainViewModel()
        {
            Messenger.Default.Register<string>(this, LoadThisDateSefaresh);
            LoadThisDateSefaresh("1395/04/12");
        }

        private void LoadThisDateSefaresh(string tarikh)
        {
            SefareshService ss = new SefareshService();
            sefaresh = ss.LoadSefaresh(tarikh);
            Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items);
        }

       
        public ObservableCollection<ItemSefaresh> Items { get; set; }

        public Sefaresh sefaresh { get; set; }
    }
}
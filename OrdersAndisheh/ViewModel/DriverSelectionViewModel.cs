using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.CommandWpf;
using OrdersAndisheh.DBL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OrdersAndisheh.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DriverSelectionViewModel : ViewModelBase
    {
        ISefareshService service;
        /// <summary>
        /// Initializes a new instance of the DriverSelection class.
        /// </summary>
        public DriverSelectionViewModel(ISefareshService _s)
        {
            service = _s;
            Messenger.Default.Register<string>(this, "ThisSefaresh", ThisSefaresh);
            ErsalItems = new ObservableCollection<ItemSefaresh>();
            DriverItems1 = new ObservableCollection<ItemSefaresh>();
        }

        private void ThisSefaresh(string obj)
        {
            ErsalItems = new ObservableCollection<ItemSefaresh>(service.LoadSefareshItems(obj));
        }

        private ObservableCollection<ItemSefaresh> ersalItems =  new ObservableCollection<ItemSefaresh>();
        public ObservableCollection<ItemSefaresh> ErsalItems
        {
            get { return ersalItems; }
            set 
            { 
                ersalItems = value;
                RaisePropertyChanged(() => this.ErsalItems);
            }
        }
        
        

        public ObservableCollection<ItemSefaresh> DriverItems1 { get; set; }
        //public ObservableCollection<ItemSefaresh> item2 { get; set; }
        //public ObservableCollection<ItemSefaresh> item3 { get; set; }
        //public ObservableCollection<ItemSefaresh> item4 { get; set; }
        //public ObservableCollection<ItemSefaresh> item5 { get; set; }
        //public ObservableCollection<ItemSefaresh> item6 { get; set; }
        //public ObservableCollection<ItemSefaresh> item7 { get; set; }
        //public ObservableCollection<ItemSefaresh> item8 { get; set; }
    }
}
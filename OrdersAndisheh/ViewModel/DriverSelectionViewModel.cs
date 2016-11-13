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
            ErsalItems = new ObservableCollection<ItemSefaresh>();
            DriverViewModels = new ObservableCollection<DriverContainerViewModel>();
            Messenger.Default.Register<string>(this, "ThisSefaresh", ThisSefaresh);
        }

        private void ThisSefaresh(string obj)
        {
            ErsalItems = new ObservableCollection<ItemSefaresh>(service.LoadSefareshItems(obj));

            var ma = ErsalItems.Select(p => p.Maghsad).Distinct();

            foreach (var item in ma)
            {
                var p = new ObservableCollection<ItemSefaresh>(ErsalItems.Where(o => o.Maghsad == item).ToList());
                DriverViewModels.Add(new DriverContainerViewModel(p));
            }
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


        public ObservableCollection<DriverContainerViewModel> DriverViewModels { get; set; }
       
    }
}
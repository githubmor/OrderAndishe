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
    
    public class DriverSelectionViewModel : ViewModelBase
    {
        ISefareshService service;
        int pos = 1;
        public DriverSelectionViewModel(ISefareshService _s)
        {
            service = _s;
            ErsalItems = new ObservableCollection<ItemSefaresh>();
            DriverViewModels = new ObservableCollection<DriverContainerViewModel>();
            Messenger.Default.Register<string>(this, "ThisSefaresh", ThisSefaresh);
        }

        private void ThisSefaresh(string obj)
        {
            ErsalItems = new ObservableCollection<ItemSefaresh>(service.LoadNoDriverSefareshItems(obj));

            //آیتم هایی که راننده موقت دارند باید در کانتین خود قرار گرفته و راننده آنها انتخاب شود
            var tempDriver = ErsalItems.Where(t => t.Driver != null).Select(p => p.Ranande).Distinct();
            foreach (var item in tempDriver)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var pe = new ObservableCollection<ItemSefaresh>(ErsalItems.Where(o => o.Ranande == item).ToList());
                    DriverViewModels.Add(new DriverContainerViewModel(pe, pos));
                    pos += 1;
                }
            }

            //لیست آیتم هایی که راننده موقت ندارند بر اساس مقصد کانتین بندی شوند
            var ma = ErsalItems.Where(i=>i.Driver==null).Select(p => p.Maghsad).Distinct();

            foreach (var item in ma)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var p = new ObservableCollection<ItemSefaresh>(ErsalItems.Where(o => o.Maghsad == item).ToList());
                    DriverViewModels.Add(new DriverContainerViewModel(p, pos));
                    pos += 1;
                }
            }


            var ooo = ErsalItems.Where(p=>p.Maghsad!="").ToList();
            for (int i = 0; i < ooo.Count; i++)
            {
                ErsalItems.Remove(ooo[i]);
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

        private RelayCommand _myCommand1;

        /// <summary>
        /// Gets the AddNewContainer.
        /// </summary>
        public RelayCommand AddNewContainer
        {
            get
            {
                return _myCommand1
                    ?? (_myCommand1 = new RelayCommand(ExecuteAddNewContainer));
            }
        }

        private void ExecuteAddNewContainer()
        {
            DriverViewModels.Add(new DriverContainerViewModel(pos));
            pos += 1;
        }

        private RelayCommand _myCommand2;

        /// <summary>
        /// Gets the SaveDrivers.
        /// </summary>
        public RelayCommand SaveDrivers
        {
            get
            {
                return _myCommand2
                    ?? (_myCommand2 = new RelayCommand(ExecuteSaveDrivers));
            }
        }

        private void ExecuteSaveDrivers()
        {
            foreach (var item in DriverViewModels)
            {
                if (item.SelectedDriver==null)
                {
                    Driver p = new Driver() { Name = "راننده " + item.DriverNumber, Tol = item.VaznKol, 
                        TempDriver = new TempDriver() { Name = item.DriverNumber.ToString() } };
                    service.AddDriver(p);
                    item.SelectedDriver = p;
                    
                }

                item.AssignDriver();
                service.Save();
                
                
            }
            
        }
       
    }
}
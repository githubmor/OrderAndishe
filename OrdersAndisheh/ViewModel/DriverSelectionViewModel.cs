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
        SefareshService service;
        int pos = 1;
        public DriverSelectionViewModel(SefareshService _s)
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
                //if (!string.IsNullOrEmpty(item))
                //{

                var pe = new ObservableCollection<ItemSefaresh>(ErsalItems.Where(o => o.Ranande == item).ToList());
                DriverViewModels.Add(new DriverContainerViewModel(service,pe, pos));
                pos += 1;
                
                //}
            }

            //لیست آیتم هایی که راننده موقت ندارند بر اساس مقصد کانتین بندی شوند
            var ma = ErsalItems.Where(i=>i.Driver==null).Select(p => p.Maghsad).Distinct();

            foreach (var item in ma)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var p = new ObservableCollection<ItemSefaresh>(ErsalItems.Where(o => o.Maghsad == item).ToList());
                    DriverViewModels.Add(new DriverContainerViewModel(service, p, pos));
                    pos += 1;
                }
            }

            var ooo1 = ErsalItems.Where(p => p.Driver != null).ToList();
            for (int i = 0; i < ooo1.Count; i++)
            {
                ErsalItems.Remove(ooo1[i]);
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
            DriverViewModels.Add(new DriverContainerViewModel(service, pos));
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
            List<Driver> te = new List<Driver>();
            foreach (var item in DriverViewModels)
            {
                if (item.SelectedDriver==null)
                {
                    Driver p = new Driver() { Name = getDriverName(item.VaznKol,item.JaigahCount) + item.DriverNumber, Tol = 0, 
                        TempDriver = new TempDriver() { Name = item.DriverNumber.ToString() } };
                    service.AddDriver(p);
                    item.SelectedDriver = p;
                }
                if (item.TempDriverForDel!=null)
                {
                    te.Add(item.TempDriverForDel);
                }

                item.AssignDriver();
                
                service.Save();
                
            }
            service.DelNoUsedTempDrivers(te);
            
        }

        private string getDriverName(int vaznBar,string jaigah)
        {
            int jaigahcount = int.Parse(jaigah);
            if (vaznBar<2000 & jaigahcount<2)
            {
                return "نیسان ";
            }
            else if (vaznBar < 3200 & jaigahcount<6)
            {
                return "مینی خاور ";
            }
            else if (vaznBar < 4200 & jaigahcount < 8)
            {
                return "مینی خاور ";
            }
            else if (vaznBar < 5700 & jaigahcount < 8)
            {
                return "911 ";
            }
            else if (vaznBar < 10000 & jaigahcount < 8)
            {
                return "تک ";
            }
            else
            {
                return "ماشین ";
            }
        }
       
    }
}
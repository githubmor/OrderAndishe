using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using OrdersAndisheh.DBL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OrdersAndisheh.ViewModel
{
    
    public class DriverWorksViewModel : ViewModelBase
    {
        //private Driver selectedDriver;
        private Order order;
        //ObservableCollection<DriverWork> driverWorks;
        SefareshService service;
        private DriverWork selectedDriverWork;
        public DriverWorksViewModel(string sefareshtarikh)
        {
            service = new SefareshService();
            order = service.LoadThisSefareshWithAllDriverWork(sefareshtarikh);
            DriverWorks =  new ObservableCollection<DriverWork>(order.DriverWorks);
            RaisePropertyChanged(() => DriverWorks);
            Drivers = service.LoadDriversForThisSefaresh(order);
            SelectedDriverWork = new DriverWork();
            SelectedDriverWork.OrderId = order.Id;
        }

        public List<Driver> Drivers { get; set; }

        public ObservableCollection<DriverWork> DriverWorks
        {
            get { return new ObservableCollection<DriverWork>(order.DriverWorks); }
            set 
            {
                order.DriverWorks = value;
                RaisePropertyChanged(() => DriverWorks);
            }
        }
       

        public Driver SelectedDriver
        {
            get { return SelectedDriverWork.Driver; }
            set 
            {
                SelectedDriverWork.Driver = value;
                RaisePropertyChanged(() => SelectedDriver);
            }
        }

        public DriverWork SelectedDriverWork
        {
            get { return selectedDriverWork; }
            set
            {
                selectedDriverWork = value;
                RaisePropertyChanged(() => SelectedDriverWork);
            }
        }

        //private string works;

        public string Works
        {
            get { return SelectedDriverWork.Works; }
            set { SelectedDriverWork.Works = value; }
        }


        private RelayCommand _myCommand1;

        /// <summary>
        /// Gets the SaveDriverWork.
        /// </summary>
        public RelayCommand SaveDriverWork
        {
            get
            {
                return _myCommand1 ?? (_myCommand1 = new RelayCommand(
                    ExecuteSaveDriverWork,
                    CanExecuteSaveDriverWork));
            }
        }

        private void ExecuteSaveDriverWork()
        {
            try
            {
                //if (SelectedDriverWork.Order == null)
                //{
                //    SelectedDriverWork.Order = order;
                //}
                service.SaveDriverWorks(SelectedDriverWork);
                RaisePropertyChanged(() => DriverWorks);
            }
            catch (System.Exception rr)
            {

                System.Windows.Forms.MessageBox.Show(rr.Message.ToString());
            }
        }

        private bool CanExecuteSaveDriverWork()
        {
            return true;
        }
    }
}
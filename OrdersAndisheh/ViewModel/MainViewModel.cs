using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.DBL;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrdersAndisheh.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        SefareshService ss;
        public MainViewModel()
        {
            ss = new SefareshService();
            Messenger.Default.Register<string>(this, LoadThisDateSefaresh);
            
            Items = new ObservableCollection<ItemSefaresh>();
            sefaresh = new Sefaresh();
            Drivers = ss.LoadDrivers();
            Goods = ss.LoadGoods();
            Destinations = ss.LoadDestinations();
        }

        private void LoadThisDateSefaresh(string tarikh)
        {
            try
            {
                sefaresh = ss.LoadSefaresh(tarikh);
                Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items);
            }
            catch (System.Exception r)
            {
                System.Windows.Forms.MessageBox.Show(r.Message.ToString());
            }
        }

        private string tedad;

        public string Tedad
        {
            get { return tedad; }
            set 
            { 
                tedad = value;
                RaisePropertyChanged(()=>Tedad);
            }
        }
        

        private ReportRow selectedItem;

        public ReportRow SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }
        

        public List<Customer> Destinations { get; set; }
        public List<Product> Goods { get; set; }
        public List<Driver> Drivers { get; set; }
        public ObservableCollection<ItemSefaresh> Items { get; set; }
        public Sefaresh sefaresh { get; set; }

        private Customer selectedDestenation;

        public Customer SelectedDestenation
        {
            get { return selectedDestenation; }
            set
            {
                selectedDestenation = value;
                RaisePropertyChanged(() => SelectedDestenation);
            }
        }

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                RaisePropertyChanged(() => SelectedProduct);
            }
        }

        private Driver selectedDriver;

        public Driver SelectedDriver
        {
            get { return selectedDriver; }
            set
            {
                selectedDriver = value;
                RaisePropertyChanged(() => SelectedDriver);
            }
        }

        private RelayCommand addNewItem;

        /// <summary>
        /// Gets the AddNewItem.
        /// </summary>
        public RelayCommand AddNewItem
        {
            get
            {
                return addNewItem ?? (addNewItem = new RelayCommand(
                    ExecuteAddNewItem,
                    CanExecuteAddNewItem));
            }
        }

        private void ExecuteAddNewItem()
        {
            selectedItem.Tedad = tedad;
        }

        private bool CanExecuteAddNewItem()
        {
            return selectedProduct!=null & string.IsNullOrEmpty(tedad) ;
        }

        private RelayCommand addDriverDestenation;

        /// <summary>
        /// Gets the ADDDriverDestenation.
        /// </summary>
        public RelayCommand ADDDriverDestenation
        {
            get
            {
                return addDriverDestenation ?? (addDriverDestenation = new RelayCommand(
                    ExecuteADDDriverDestenation,
                    CanExecuteADDDriverDestenation));
            }
        }

        private void ExecuteADDDriverDestenation()
        {
            
        }

        private bool CanExecuteADDDriverDestenation()
        {
            return selectedItem!=null & selectedDriver!=null | selectedDestenation!=null;
        }

       
    }
}
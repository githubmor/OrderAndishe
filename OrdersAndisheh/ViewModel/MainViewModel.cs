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

        private int tedad;

        public int Tedad
        {
            get { return tedad; }
            set 
            {
                tedad = value;
                RaisePropertyChanged(()=>Tedad);
                AddNewItem.RaiseCanExecuteChanged();
            }
        }


        private ItemSefaresh selectedItem;

        public ItemSefaresh SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
                ADDDriverDestenation.RaiseCanExecuteChanged();
                AddNewItem.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<ItemSefaresh> selectedItems;

        public ObservableCollection<ItemSefaresh> SelectedItems
        {
            get { return selectedItems; }
            set
            {
                selectedItems = value;
                RaisePropertyChanged(() => SelectedItems);
                ADDDriverDestenation.RaiseCanExecuteChanged();
                AddNewItem.RaiseCanExecuteChanged();
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
                ADDDriverDestenation.RaiseCanExecuteChanged();
                AddNewItem.RaiseCanExecuteChanged();
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
                AddNewItem.RaiseCanExecuteChanged();
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
                ADDDriverDestenation.RaiseCanExecuteChanged();
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
            ItemSefaresh ss = new ItemSefaresh(selectedProduct);
            ss.Tedad = tedad;
            ss.Customer = selectedDestenation;
            ss.Driver = selectedDriver;
            Items.Add(ss);

            SelectedProduct = null;
            Tedad = 0;
            selectedDriver = null;
            selectedDestenation = null;
            
        }

        private bool CanExecuteAddNewItem()
        {
            return selectedProduct!=null & tedad>0 ;
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
            SelectedItems = SelectedItems;
            SelectedItem.Customer = selectedDestenation;
            SelectedItem.Driver = selectedDriver;
            SelectedProduct = null;
            Tedad = 0;
            selectedDriver = null;
            selectedDestenation = null;
        }

        private bool CanExecuteADDDriverDestenation()
        {
            return SelectedItems==null & selectedItem!=null & (selectedDriver!=null | selectedDestenation!=null);
        }

       
    }
}
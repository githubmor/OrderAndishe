using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.DBL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;


namespace OrdersAndisheh.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        SefareshService ss;
        public MainViewModel()
        {
            ss = new SefareshService();
            Messenger.Default.Register<string>(this, LoadThisDateSefaresh);
            //LoadThisDateSefaresh("1395/05/05");
            
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
        public string Tarikh
        {
            get { return sefaresh.Tarikh; }
            set
            {
                sefaresh.Tarikh = value;
                RaisePropertyChanged(() => Tarikh);
                SaveSefaresh.RaiseCanExecuteChanged();
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

        #region Command
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
            SelectedDriver = null;
            SelectedDestenation = null;

        }

        private bool CanExecuteAddNewItem()
        {
            return SelectedProduct != null & tedad > 0;
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
            foreach (var item in Items.Where(p => p.IsSelected))
            {
                if (SelectedDestenation != null)
                {
                    if (string.IsNullOrEmpty(item.Maghsad))
                    {
                        item.Customer = selectedDestenation;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید مقصد را تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            item.Customer = selectedDestenation;
                        }


                    }
                }

                if (SelectedDriver != null)
                {
                    if (string.IsNullOrEmpty(item.Ranande))
                    {
                        item.Driver = selectedDriver;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید راننده را تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            item.Driver = selectedDriver;
                        }


                    }
                }

            }
            SelectedProduct = null;
            Tedad = 0;
            SelectedDriver = null;
            SelectedDestenation = null;
        }

        private bool CanExecuteADDDriverDestenation()
        {
            return Items.Where(p => p.IsSelected).Count() > 0 & (SelectedDriver != null | SelectedDestenation != null);
        }

        private RelayCommand _saveSefares;

        /// <summary>
        /// Gets the SaveSefaresh.
        /// </summary>
        public RelayCommand SaveSefaresh
        {
            get
            {
                return _saveSefares ?? (_saveSefares = new RelayCommand(
                    ExecuteSaveSefaresh,
                    CanExecuteSaveSefaresh));
            }
        }

        private void ExecuteSaveSefaresh()
        {
            try
            {
                sefaresh.Items = Items.ToList();
                ss.SaveSefaresh(sefaresh);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    MessageBox.Show("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + "\n" +
                        eve.Entry.Entity.GetType().Name + "\n" + eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show("- Property: \"{0}\", Error: \"{1}\"" + "\n" +
                            ve.PropertyName + "\n" + ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private bool CanExecuteSaveSefaresh()
        {
            return Items.Count > 0 & !string.IsNullOrEmpty(Tarikh);
        }
        #endregion

       
    }
}
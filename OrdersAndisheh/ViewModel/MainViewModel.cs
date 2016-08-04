using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;


namespace OrdersAndisheh.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        SefareshService ss;
        bool IsEdit;
        private List<ItemType> enumList;
        public MainViewModel()
        {
            ss = new SefareshService();
            Messenger.Default.Register<string>(this, LoadThisDateSefaresh);
            
            
            sefaresh = new Sefaresh();
            Drivers = ss.LoadDrivers();
            Goods = ss.LoadGoods();
            Destinations = ss.LoadDestinations();
            enumList = Enum.GetValues(typeof(ItemType)).OfType<ItemType>().ToList();
            //Items.CollectionChanged += this.OnCollectionChanged;
            
            //LoadThisDateSefaresh("1395/05/05");
        }

        //private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    ObservableCollection<ItemSefaresh> obsSender = sender as ObservableCollection<ItemSefaresh>;

        //    foreach (var item in obsSender)
        //    {
        //        if (item.IsSelected)
        //        {
        //            MessageBox.Show(item.Kala);
        //        }
        //    }

        //}

        

        private void LoadThisDateSefaresh(string tarikh)
        {
            try
            {
                sefaresh = ss.LoadSefaresh(tarikh);
                Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items);
                IsEdit = true;
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

        public List<ItemType> TypeList
        {
            get { return enumList; }
            set
            {
                enumList = value;
                RaisePropertyChanged(() => TypeList);
                //SaveSefaresh.RaiseCanExecuteChanged();
            }
        }

        public List<Customer> Destinations { get; set; }
        public List<Product> Goods { get; set; }
        public List<Driver> Drivers { get; set; }

        public ObservableCollection<ItemSefaresh> Items 
        {
            get { return sefaresh.Items; }
            set
            {
                sefaresh.Items = value;
                RaisePropertyChanged(() => Items);
            }
        }
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
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
            ADDDriverDestenation.RaiseCanExecuteChanged();
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
                if (IsEdit)
                {
                    //sefaresh.Items = Items.ToList();
                    ss.UpdateSefaresh(sefaresh);
                    MessageBox.Show("اطلاعات سفارش روز " + Tarikh + " ویرایش شد");
                }
                else
                {
                    //sefaresh.Items = Items.ToList();
                    ss.SaveSefaresh(sefaresh);
                    MessageBox.Show("اطلاعات سفارش روز "+Tarikh+" ذخیره شد");
                }
                
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

        //private RelayCommand _foriCommand;

        ///// <summary>
        ///// Gets the SetAsFori.
        ///// </summary>
        //public RelayCommand SetAsFori
        //{
        //    get
        //    {
        //        return _foriCommand ?? (_foriCommand = new RelayCommand(
        //            ExecuteSetAsFori,
        //            CanExecuteSetAsFori));
        //    }
        //}

        //private void ExecuteSetAsFori()
        //{
        //    foreach (var item in Items.Where(p => p.IsSelected))
        //    {
        //        item.ItemKind = ItemType.فوری.ToString();
        //    }
        //}

        //private bool CanExecuteSetAsFori()
        //{
        //    return Items.Where(p => p.IsSelected).Count() > 0 ;
        //}


        //private RelayCommand _agarshodCommand;

        ///// <summary>
        ///// Gets the SetAsAgarShod.
        ///// </summary>
        //public RelayCommand SetAsAgarShod
        //{
        //    get
        //    {
        //        return _agarshodCommand ?? (_agarshodCommand = new RelayCommand(
        //            ExecuteSetAsAgarShod,
        //            CanExecuteSetAsAgarShod));
        //    }
        //}

        //private void ExecuteSetAsAgarShod()
        //{
        //    foreach (var item in Items.Where(p => p.IsSelected))
        //    {
        //        item.ItemKind = "نامشخص";
        //    }
        //}

        //private bool CanExecuteSetAsAgarShod()
        //{
        //    return Items.Where(p => p.IsSelected).Count() > 0 ;
        //}

        ////private RelayCommand _govahiCommand;

        /////// <summary>
        /////// Gets the SetAzGovahi.
        /////// </summary>
        ////public RelayCommand SetAzGovahi
        ////{
        ////    get
        ////    {
        ////        return _govahiCommand ?? (_govahiCommand = new RelayCommand(
        ////            ExecuteSetAzGovahi,
        ////            CanExecuteSetAzGovahi));
        ////    }
        ////}

        ////private void ExecuteSetAzGovahi()
        ////{
        ////    foreach (var item in Items.Where(p => p.IsSelected))
        ////    {
        ////        item.ItemKind = ItemType.Govahi.ToString();
        ////    }
        ////}

        ////private bool CanExecuteSetAzGovahi()
        ////{
        ////    return Items.Where(p => p.IsSelected).Count() > 0 ;
        ////}

        //private ICommand _govahiCommand;
        
        //public ICommand SetAzGovahi
        //{
        //    get
        //    {
        //        if (_govahiCommand == null)
        //        {
        //            _govahiCommand = new RelayCommand(() => this.OpenEditDialog(), () => this.editcan());
        //        }
        //        return _govahiCommand;
        //    }
        //}

        //private bool editcan()
        //{
        //    return Items.Where(p => p.IsSelected).Count() > 0;
        //}
        //public void OpenEditDialog()
        //{
        //    foreach (var item in Items.Where(p => p.IsSelected))
        //    {
        //        item.ItemKind = ItemType.گواهی.ToString();
        //    }
        //}

        #endregion



        
    }
}
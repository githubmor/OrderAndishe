﻿using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
using OrdersAndisheh.DBL;
using OrdersAndisheh.View;
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
        ISefareshService ss;
        bool IsEdit,IsEditItem,IsDirty;
        private List<ItemType> enumList;
        public MainViewModel(ISefareshService service)
        {
            ss = service;
            Messenger.Default.Register<string>(this,"Editsefaresh",LoadThisDateSefaresh);

            sefaresh = new Sefaresh();
            Drivers = ss.LoadDrivers();
            Goods = ss.LoadGoods();
            Destinations = ss.LoadDestinations();
            enumList = Enum.GetValues(typeof(ItemType)).OfType<ItemType>().ToList();
            IsDirty = true;
            //TODO  باید یک قسمت توضیحات برای هر راننده اضافه کنیم مثل شب تحویل - انبار  87 - دیجیتال صنعت
            //TODO قفل های بدون اورکل چی شد 
            //TODO تثبیت ارسال چی شد ؟
            //TODO ثبت تحویل فروش از روی خروجی برهان و ایرور هایی که باید بدهد
            //TODO چرا در پی دی اف حروف انگلیسی رو بصورت جعبه نشان میدهد
            //TODO رنگ بندی کردن لیست ارسال داخل خود نرم افزار
            //TODO حذف سفارش یک روز 
            //TODO
            //TODO
            //TODO


            //LoadThisDateSefaresh("1395/07/22");
        }

        

        public void LoadThisDateSefaresh(string tarikh)
        {
            try
            {
                sefaresh = ss.LoadSefaresh(tarikh);
                RaisePropertyChanged(() => Items);
                IsEdit = true;
                IsDirty = false;
                MessageBox.Show("سفارش تاریخ " + Tarikh + "بار گذاری شد" ) ;
                RaisePropertyChanged(() => this.Tarikh);
                //CreateAnbarList.RaiseCanExecuteChanged();
                //CreateBazresLists.RaiseCanExecuteChanged();
                //CreateImensazanList.RaiseCanExecuteChanged();
                //CreateKontrolList.RaiseCanExecuteChanged();
                //CreatListErsal.RaiseCanExecuteChanged();
            }
            catch (System.Exception r)
            {
                System.Windows.Forms.MessageBox.Show(r.Message.ToString());
            }
        }

        private int tempTedad;

        public int Tedad
        {
            get { return (SelecteddItem != null ? SelecteddItem.Tedad : 0); }
            set 
            {
                if (SelecteddItem != null)
                {
                    SelecteddItem.Tedad = value;
                }
                else
                {
                    tempTedad = value;
                }
                
                RaisePropertyChanged(()=>Tedad);
                //AddNewItem.RaiseCanExecuteChanged();
            }
        }

        private string tempDes;

        public string Description
        {
            get { return (SelecteddItem != null ? SelecteddItem.Des : ""); }
            set
            {
                if (SelecteddItem != null)
                {
                    SelecteddItem.Des = value;
                }
                else
                {
                    tempDes = value;
                }

                RaisePropertyChanged(() => Description);
                //AddNewItem.RaiseCanExecuteChanged();
            }
        }
        public string Tarikh
        {
            get { return sefaresh.Tarikh; }
            set
            {
                //if (value.Length>9)
                //{
                    sefaresh.Tarikh = value;
                //}
                RaisePropertyChanged(() => Tarikh);
                IsDirty = true;
                //SaveSefaresh.RaiseCanExecuteChanged();
                //LoadSefaresh.RaiseCanExecuteChanged();
            }
        }

        public List<ItemType> TypeList
        {
            get { return enumList; }
            set
            {
                enumList = value;
                RaisePropertyChanged(() => TypeList);
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
        private ItemSefaresh selectedItem;
        public ItemSefaresh SelecteddItem
        {
            get { return selectedItem; }
            set
            {
               
                selectedItem = value;
                RaisePropertyChanged(() => GoodCode);
                RaisePropertyChanged(() => GoodName);
                RaisePropertyChanged(() => SelectedDriver);
                RaisePropertyChanged(() => SelectedDestenation);
                RaisePropertyChanged(() => Tedad);
                RaisePropertyChanged(() => Description);
                //AddNewItem.RaiseCanExecuteChanged();
                //DeleteItem.RaiseCanExecuteChanged();
            }
        }

        private ItemSefaresh clickedItem;
        public ItemSefaresh ClickedItem
        {
            get { return clickedItem; }
            set
            {
                if (value!=null)
                {
                    SelecteddItem = value;
                }
                clickedItem = value;
                IsEditItem = true;
                //CanReport = false;
                //CreateAnbarList.RaiseCanExecuteChanged();
                //CreateBazresLists.RaiseCanExecuteChanged();
                //CreateImensazanList.RaiseCanExecuteChanged();
                //CreateKontrolList.RaiseCanExecuteChanged();
                //CreatListErsal.RaiseCanExecuteChanged();
            }
        }


        private Sefaresh sefaresh { get; set; }

        private Customer tempDestenation;

        public Customer SelectedDestenation
        {
            get { return (SelecteddItem != null ? SelecteddItem.Customer : null); }
            set
            {
                if (SelecteddItem != null)
                {
                    SelecteddItem.Customer = value;
                    //SelecteddItem.OrderDetail.Customer_Id = value.Id;
                }
                else if (value != null)
                {
                    tempDestenation = value;
                }
                
                RaisePropertyChanged(() => SelectedDestenation);
                //ADDDriverDestenation.RaiseCanExecuteChanged();
                //AddNewItem.RaiseCanExecuteChanged();
            }
        }

        

        public string GoodCode
        {
            get { return (SelecteddItem != null ? SelecteddItem.CodeKala : null); }
            set 
            {
                if (value.Length>=8)
                {
                    Product gg = Goods.Where(p => p.Code == value).FirstOrDefault();
                    if (gg!=null)
                    {
                        //باید تفاوتی بین ویرایش یک آیتم و ساخت آیتم جدید قایل شد
                        SelecteddItem = new ItemSefaresh(gg);
                        //SelecteddItem.OrderDetail.ProductId = gg.Id;
                        //IsEditItem = false;
                    }
                    if (tempDestenation != null)
                    {
                        SelecteddItem.Customer = tempDestenation;
                        //SelecteddItem.OrderDetail.Customer_Id = tempDestenation.Id;
                    }
                    if (tempDriver != null)
                    {
                        SelecteddItem.Driver = tempDriver;
                        //SelecteddItem.OrderDetail.Driver_Id = tempDriver.Id;
                    }
                    if (tempTedad > 0)
                    {
                        SelecteddItem.Tedad = tempTedad;
                    }
                    if (!string.IsNullOrEmpty(tempDes))
                    {
                        SelecteddItem.Des = tempDes;
                    }
                }
                else
                {
                    SelecteddItem = null;
                }
                
                RaisePropertyChanged(() => GoodCode);
                RaisePropertyChanged(() => GoodName);
                RaisePropertyChanged(() => Tedad);
                RaisePropertyChanged(() => Description);
                //AddNewItem.RaiseCanExecuteChanged();

            }
        }


        public string GoodName
        {
            get { return (SelecteddItem != null ? SelecteddItem.Kala : ""); }
        }
        


        private Driver tempDriver;

        public Driver SelectedDriver
        {
            get { return (SelecteddItem != null ? SelecteddItem.Driver : null); }
            set
            {
                if (SelecteddItem != null)
                {
                    SelecteddItem.Driver = value;
                    //SelecteddItem.OrderDetail.Driver_Id = value.Id;
                }
                else if (value != null)
                {
                    tempDriver = value;
                }
                RaisePropertyChanged(() => SelectedDriver);
                //ADDDriverDestenation.RaiseCanExecuteChanged();
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
            if (!IsEditItem)
            {
                Items.Add(SelecteddItem);
            }
            else if (ClickedItem!=null)
            {
                if (GoodCode!=ClickedItem.CodeKala)
                {
                    Items.Remove(Items.Where(p => p.CodeKala == ClickedItem.CodeKala).FirstOrDefault());
                    Items.Add(SelecteddItem);
                }
            }
            else
            {
                Items.Add(SelecteddItem);
            }
            IsDirty = true;
            //IsEdit = true;
            IsEditItem = false;
            SelecteddItem = null;
        }

        private bool CanExecuteAddNewItem()
        {
            return (SelecteddItem != null?SelecteddItem.Product!=null:false);
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
                        item.Customer = SelectedDestenation;
                    }
                    else if(item.Maghsad != SelectedDestenation.Name)
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید مقصد را از " +
                            item.Maghsad + " به " + SelectedDestenation.Name + " تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            item.Customer = SelectedDestenation;
                        }


                    }
                }

                if (SelectedDriver != null)
                {
                    if (string.IsNullOrEmpty(item.Ranande))
                    {
                        item.Driver = SelectedDriver;
                    }
                    else if (item.Ranande != SelectedDriver.Name)
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید راننده را از " +
                            item.Ranande + " به " + SelectedDriver.Name + " تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            item.Driver = SelectedDriver;
                        }


                    }
                }

            }
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
            IsDirty = true;
            //IsEdit = true;
            SelecteddItem = null;
            
        }

        private bool CanExecuteADDDriverDestenation()
        {
            return Items.Where(p => p.IsSelected).Count() > 0 & (SelectedDriver != null | SelectedDestenation != null);
        }

        private RelayCommand delItem;

        /// <summary>
        /// Gets the ADDDriverDestenation.
        /// </summary>
        public RelayCommand DeleteItem
        {
            get
            {
                return delItem ?? (delItem = new RelayCommand(
                    ExecuteDelItem,
                    CanExecuteDelItem));
            }
        }

        private void ExecuteDelItem()
        {
           
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].IsSelected)
                {
                    Items.Remove(Items[i]);
                }
            }

            IsDirty = false;
            //IsEdit = true;
            SelecteddItem = null;

        }

        private bool CanExecuteDelItem()
        {
            return Items.Where(p => p.IsSelected).Count() > 0;
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
                    ss.UpdateSefaresh(sefaresh);
                    MessageBox.Show("اطلاعات سفارش روز " + Tarikh + " ویرایش شد");
                }
                else 
                {
                    ss.SaveSefaresh(sefaresh);
                    IsEdit = true;
                    MessageBox.Show("اطلاعات سفارش روز "+Tarikh+" ذخیره شد");
                }
                IsDirty = false;
                //CreateAnbarList.RaiseCanExecuteChanged();
                //CreateBazresLists.RaiseCanExecuteChanged();
                //CreateImensazanList.RaiseCanExecuteChanged();
                //CreateKontrolList.RaiseCanExecuteChanged();
                //CreatListErsal.RaiseCanExecuteChanged();
                
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
            return Items.Count > 0 & !string.IsNullOrEmpty(Tarikh) & IsDirty;
        }

        private RelayCommand createBazresList;

        /// <summary>
        /// Gets the CreateBazresLists.
        /// </summary>
        public RelayCommand CreateBazresLists
        {
            get
            {
                return createBazresList ?? (createBazresList = new RelayCommand(
                    ExecuteCreateBazresLists,
                    CanExecuteCreateBazresLists));
            }
        }

        private void ExecuteCreateBazresLists()
        {
            ReportManager rp = new ReportManager(sefaresh);
            rp.CreatAllBazresReportOnDeskTop();
        }

        private bool CanExecuteCreateBazresLists()
        {
            return !IsDirty;
        }


        private RelayCommand _myCommand7;

        /// <summary>
        /// Gets the CreatListErsal.
        /// </summary>
        public RelayCommand CreatListErsal
        {
            get
            {
                return _myCommand7 ?? (_myCommand7 = new RelayCommand(
                    ExecuteCreatListErsal,
                    CanExecuteCreatListErsal));
            }
        }

        private void ExecuteCreatListErsal()
        {
            ReportManager rp = new ReportManager(sefaresh);
            rp.CreatListErsalReportOnDeskTop();
        }

        private bool CanExecuteCreatListErsal()
        {
            return !IsDirty;
        }

        private RelayCommand _myCommand8;

        /// <summary>
        /// Gets the CreateAnbarList.
        /// </summary>
        public RelayCommand CreateAnbarList
        {
            get
            {
                return _myCommand8 ?? (_myCommand8 = new RelayCommand(
                    ExecuteCreateAnbarList,
                    CanExecuteCreateAnbarList));
            }
        }

        private void ExecuteCreateAnbarList()
        {
            try
            {
                ReportManager rp = new ReportManager(sefaresh);
                rp.CreatAnbarReportOnDeskTop();
            }
            catch (Exception rt)
            {

                MessageBox.Show(rt.Message.ToString());
            }
        }

        private bool CanExecuteCreateAnbarList()
        {
            return !IsDirty;
        }

        private RelayCommand _myCommand9;

        /// <summary>
        /// Gets the CreateImensazanList.
        /// </summary>
        public RelayCommand CreateImensazanList
        {
            get
            {
                return _myCommand9 ?? (_myCommand9 = new RelayCommand(
                    ExecuteCreateImensazanList,
                    CanExecuteCreateImensazanList));
            }
        }

        private void ExecuteCreateImensazanList()
        {
             ReportManager rp = new ReportManager(sefaresh);
            rp.CreatImenSazanReportOnDeskTop();
        }

        private bool CanExecuteCreateImensazanList()
        {
            return !IsDirty;
        }

        private RelayCommand _myCommand10;

        /// <summary>
        /// Gets the CreateKontrolList.
        /// </summary>
        public RelayCommand CreateKontrolList
        {
            get
            {
                return _myCommand10 ?? (_myCommand10 = new RelayCommand(
                    ExecuteCreateKontrolList,
                    CanExecuteCreateKontrolList));
            }
        }

        private void ExecuteCreateKontrolList()
        {
            try
            {
                ReportManager rp = new ReportManager(sefaresh);
                rp.CreatKontrolReportOnDeskTop();
            }
            catch (Exception rrr)
            {

                MessageBox.Show(rrr.Message.ToString()); ;
            }
        }

        private bool CanExecuteCreateKontrolList()
        {
            //TODO باید اینجا فقط زمانی اینا فعال باشن که اولا ثبت شده باشد یعنی در حال ویرایش و دوما هیچ تغییر ثبت نشده جدیدی وجود نداشته باشد
            return !IsDirty;
        }

        private RelayCommand _myCommand111;

        /// <summary>
        /// Gets the LoadSefaresh.
        /// </summary>
        public RelayCommand LoadSefaresh
        {
            get
            {
                return _myCommand111 ?? (_myCommand111 = new RelayCommand(
                    ExecuteLoadSefaresh,
                    CanExecuteLoadSefaresh));
            }
        }

        private void ExecuteLoadSefaresh()
        {
            if (IsDirty)
            {
                DialogResult result = MessageBox.Show("آيا مطمئن هستيد ميخواهيد اطلاعات را دوباره بارگذاري كنيد ؟ اطلاعات ذخيره نشده حذف خواهد شد" ,
                            "اطلاع", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.LoadThisDateSefaresh(Tarikh);
                }
            }
            else
            {
                this.LoadThisDateSefaresh(Tarikh);
            }
        }

        private bool CanExecuteLoadSefaresh()
        {
            return !string.IsNullOrEmpty(Tarikh) ;
        }

        private RelayCommand _myCommand465;

        /// <summary>
        /// Gets the SelectDriver.
        /// </summary>
        public RelayCommand SelectDriver
        {
            get
            {
                return _myCommand465 ?? (_myCommand465 = new RelayCommand(
                    ExecuteSelectDriver,
                    CanExecuteSelectDriver));
            }
        }

        private void ExecuteSelectDriver()
        {
            DriverSelectionView v = new DriverSelectionView();
            Messenger.Default.Send<string>(Tarikh, "ThisSefaresh");
            v.ShowDialog();
            LoadThisDateSefaresh(Tarikh);
        }

        private bool CanExecuteSelectDriver()
        {
            return !IsDirty;
        }

        #endregion

        

        
    }
}
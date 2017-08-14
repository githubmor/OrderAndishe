using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
using OrdersAndisheh.DBL;
using OrdersAndisheh.View;
using System;
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
        bool IsEdit,IsEditItem,IsDirty;
        private List<ItemType> enumList;
        //static bool isRegistered = false;
        public MainViewModel(ISefareshService service)
        {
            ss = new SefareshService();
            //''''

            CanEdit = true;
            sefaresh = new Sefaresh();
            Drivers = ss.LoadDrivers();
            Goods = ss.LoadGoods();
            Destinations = ss.LoadDestinations();
            enumList = Enum.GetValues(typeof(ItemType)).OfType<ItemType>().ToList();
            IsDirty = true;
            var t = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
            Tarikh = t;
            Messenger.Default.Register<string>(this, "Editsefaresh", LoadThisDateSefaresh);
            //TODO  باید یک قسمت توضیحات برای هر راننده اضافه کنیم مثل شب تحویل - انبار  87 - دیجیتال صنعت
            //TODO قفل های بدون اورکل چی شد 
            //TODO تثبیت ارسال چی شد ؟
            //TODO
            //TODO
            //TODO
        }

        

        public void LoadThisDateSefaresh(string tarikh)
        {
            try
            {
                sefaresh = ss.LoadSefaresh(tarikh);
                RaisePropertyChanged(() => Items);
                IsEdit = true;
                IsDirty = false;
                CanEdit = !sefaresh.Accepted;
                KalaChanged = true;
                TedadChanged = true;
                CustomerChanged = true;
                DriverChanged = true;
                //MessageBox.Show("سفارش تاریخ " + Tarikh + "بار گذاری شد");
                RaisePropertyChanged(() => this.Tarikh);
                RaisePropertyChanged(() => this.SaveText);
                RaisePropertyChanged(() => this.DriversVazn);
                RaisePropertyChanged(() => this.MaghsadVazn);
                RaisePropertyChanged(() => this.palletHa);

            }
            catch (System.Exception r)
            {
                System.Windows.Forms.MessageBox.Show(r.Message.ToString());
            }
        }

        public bool KalaChanged { get; set; }
        public bool TedadChanged { get; set; }
        public bool CustomerChanged { get; set; }
        public bool DriverChanged { get; set; }

        //public bool CanEdit { get; set; }

        private bool myVar;

        public bool CanEdit
        {
            get { return myVar; }
            set { 
                myVar = value;
                RaisePropertyChanged(() => CanEdit);
            }
        }
        

        //private string myVar;

        public string SaveText
        {
            get { return (CanEdit?"ذخیره":"فعال کردن"); }
            //set { myVar = value; }
        }
        

        private short tempTedad;

        public short Tedad
        {
            get { return (SelecteddItem != null ? SelecteddItem.Tedad : short.Parse("0")); }
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
                RaisePropertyChanged(() => PalletCount);
            }
        }

        private int tempPalletCount;

        public int PalletCount
        {
            get { return (SelecteddItem != null ? SelecteddItem.PalletCount : 0); }
            set
            {
                if (SelecteddItem != null)
                {
                    SelecteddItem.PalletCount = value;
                }
                else
                {
                    tempPalletCount = value;
                }

                RaisePropertyChanged(() => PalletCount);
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
            }
        }

        //private string myVar;

        public string palletHa
        {
            get { return getPalletaha(); }
            //set { myVar = value; }
        }

        private string getPalletaha()
        {
            string re = "";
            //var t = from b in Items
                    //select new {palet=b.Product.Pallet.Name,tedadpalet = b.PalletCount };
            var t = Items.Select(p => p.Product.Pallet.Name).Distinct();

            foreach (var item in t)
            {
                int sum = 0;
                
                foreach (var d in Items.Where(p=>p.Product.Pallet.Name==item).Select(o=>o.PalletCount))
                {
                    sum += d;
                }
                re += item + " = " + sum + "   -   ";
            }
            //return t.ToString();      
            return re;
            //string sezeh = Items.Where(p=>p.Product.)
        }
        

        public string DriversVazn 
        {
            get 
            {
                var drss = Items.Select(o => o.Ranande).Distinct();
                string ft = "";
                foreach (var item in drss)
                {
                    ft += item + " : ";
                    int sum = 0;
                    var yu =  Items.Where(ik=>ik.Ranande==item).ToList();
                    foreach (var wew in yu)
                    {
                        sum += wew.Vazn;
                    }
                    ft += sum + " - ";
                    
                }    
                return ft;
            } 
        }
        public string MaghsadVazn
        {
            get
            {
                var drss = Items.Select(o => o.Maghsad).Distinct();
                string ft = "";
                foreach (var item in drss)
                {
                    ft += item + " : ";
                    int sum = 0;
                    var yu = Items.Where(ik => ik.Maghsad == item).ToList();
                    foreach (var wew in yu)
                    {
                        sum += wew.Vazn;
                    }
                    ft += sum + " - ";

                }
                return ft;
            }
        }

        //private string tarikh = "";
        public string Tarikh
        {
            get { return sefaresh.Tarikh; }
            set
            {

                try
                {
                    sefaresh.Tarikh = value;
                    RaisePropertyChanged(() => Tarikh);
                    IsDirty = true; 
                }
                catch (Exception r)
                {

                    MessageBox.Show(r.Message.ToString());
                }
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
                RaisePropertyChanged(() => PalletCount);
                RaisePropertyChanged(() => Description);
                RaisePropertyChanged(() => SelectedItemDriverIsTempDriver);
            }
        }

        //private int myVar;

        public bool  SelectedItemDriverIsTempDriver
        {
            get 
            {
                bool f = true;
                if (SelecteddItem!=null)
                {
                    if (SelecteddItem.Driver!=null)
                    {
                        if (SelecteddItem.Driver.TempDriver != null)
                            f = false;
                    }
                }
                return f;
            }
            //set { myVar = value; }
        }
        

        private ItemSefaresh clickedItem;
        public ItemSefaresh ClickedItem
        {
            get { return clickedItem; }
            set
            {
                SelecteddItem = value;
                clickedItem = value;
                IsEditItem = true;
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
                }
                else if (value != null)
                {
                    tempDestenation = value;
                }
                
                RaisePropertyChanged(() => SelectedDestenation);
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
                        RaisePropertyChanged(() => this.PalletCount);
                    }
                    if (tempDestenation != null)
                    {
                        SelecteddItem.Customer = tempDestenation;
                    }
                    if (tempDriver != null)
                    {
                        SelecteddItem.Driver = tempDriver;
                    }
                    if (tempTedad > 0)
                    {
                        SelecteddItem.Tedad = tempTedad;
                        RaisePropertyChanged(() => this.PalletCount);
                    }
                    if (!string.IsNullOrEmpty(tempDes))
                    {
                        SelecteddItem.Des = tempDes;
                    }
                }
                else
                {
                    ClickedItem = null;
                }
                
                RaisePropertyChanged(() => GoodCode);
                RaisePropertyChanged(() => GoodName);
                RaisePropertyChanged(() => Tedad);
                RaisePropertyChanged(() => this.PalletCount);
                RaisePropertyChanged(() => Description);
                RaisePropertyChanged(() => SelectedItemDriverIsTempDriver);

            }
        }


        public string GoodName
        {
            get { return (SelecteddItem != null ? SelecteddItem.Kala : ""); }
        }

        //List<Driver> TempDriverForDelete = new List<Driver>();

        private Driver tempDriver;

        public Driver SelectedDriver
        {
            get { return (SelecteddItem != null ? SelecteddItem.Driver : null); }
            set
            {
                
                if (SelecteddItem != null)
                {
                    //if (SelecteddItem.Driver!=null)
                    //{
                    //    if (SelecteddItem.Driver.TempDriver!=null)
                    //    {
                    //        TempDriverForDelete.Add(SelecteddItem.Driver);
                    //    }
                    //}
                    SelecteddItem.Driver = value;
                }
                else if (value != null)
                {
                    //if (value.TempDriver != null)//اگر راننده موقت انتخاب شد ببینه نگذاشته باشه برای حذف
                    //{
                    //    TempDriverForDelete.Remove(value);
                    //}
                    tempDriver = value;
                }
                RaisePropertyChanged(() => SelectedDriver);
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
            else if (ClickedItem != null)
            {
                if (GoodCode != ClickedItem.CodeKala)
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
            ClickedItem = null;
            RaisePropertyChanged(() => this.DriversVazn);
            RaisePropertyChanged(() => this.MaghsadVazn);
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
            string lastMaghsadChanged = "";
            string lastRanandeChanged = "";
            string _newMaghsad = (SelectedDestenation != null ? SelectedDestenation.Name : "تهی");
            string _newRanande = (SelectedDriver != null ? SelectedDriver.Name : "تهی");
            foreach (var item in Items.Where(p => p.IsSelected))
            {
                if (string.IsNullOrEmpty(item.Maghsad))
                {
                    item.Customer = SelectedDestenation;
                }
                else if (item.Maghsad != _newMaghsad)
                {
                    //اگر بازم میخواد همون سوال قبلی رو بپرسه دیگه تکرار نمیشه
                    if (item.Maghsad != lastMaghsadChanged)
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید مقصد را از " +
                            item.Maghsad + " به " + _newMaghsad + " تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            lastMaghsadChanged = item.Maghsad;
                            item.Customer = SelectedDestenation;
                        }
                    }
                    else
                    {
                        lastMaghsadChanged = item.Maghsad;
                        item.Customer = SelectedDestenation;
                    }
                        
                }

                
                if (string.IsNullOrEmpty(item.Ranande))
                {
                    item.Driver = SelectedDriver;
                }
                else if (item.Ranande != _newRanande)
                {
                    if (item.Ranande != lastRanandeChanged)
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید راننده را از " +
                            item.Ranande + " به " + _newRanande + " تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            lastRanandeChanged = item.Ranande;
                            item.Driver = SelectedDriver;
                        }
                    }
                    else
                    {
                        lastRanandeChanged = item.Ranande;
                        item.Driver = SelectedDriver;
                    }
                }

            }
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
            IsDirty = true;
            //IsEdit = true;
            ClickedItem = null;
            DriverChanged = true;
            CustomerChanged = true;
            RaisePropertyChanged(() => this.DriversVazn);
            RaisePropertyChanged(() => this.MaghsadVazn);
            
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
            Items.RemoveAll(p => p.IsSelected);
           
            IsDirty = true;
            ClickedItem = null;

            RaisePropertyChanged(() => this.DriversVazn);
            RaisePropertyChanged(() => this.MaghsadVazn);

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
                if (CanEdit)
                {
                    if (IsEdit)
                    {
                        changeState();

                        ss.UpdateSefaresh(sefaresh);

                        MessageBox.Show("اطلاعات سفارش روز " + Tarikh + " ویرایش شد");
                    }
                    else
                    {
                        ss.SaveSefaresh(sefaresh);
                        IsEdit = true;
                        changeState();
                        MessageBox.Show("اطلاعات سفارش روز " + Tarikh + " ذخیره شد");
                    }
                    ReportManager rp = new ReportManager(sefaresh);
                    rp.CreatAllReport();
                }
                else
                {
                    CanEdit = true;
                    ss.UnAcceptSefaresh(sefaresh);
                    MessageBox.Show("سفارش از حالت تثبیت در آمد");
                }
                IsDirty = false;
                ClickedItem = null;
                RaisePropertyChanged(() => this.DriversVazn);
                RaisePropertyChanged(() => this.MaghsadVazn);
                RaisePropertyChanged(() => this.SaveText);
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
            catch(Exception r)
            {
                MessageBox.Show(r.Message.ToString());
            }
        }

        private void changeState()
        {
            KalaChanged = sefaresh.Items.Any(p => p.IsNew);
            TedadChanged = sefaresh.Items.Any(p => p.IsTedadChanged);
            CustomerChanged = sefaresh.Items.Any(p => p.IsCustomerChanged);
            DriverChanged = sefaresh.Items.Any(p => p.IsDriverChanged);
        }

        private bool CanExecuteSaveSefaresh()
        {
            if (CanEdit)
            {
                return Items.Count > 0 & !string.IsNullOrEmpty(Tarikh) & IsDirty;
            }
            else
            {
                return true;
            }
            
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
            changeState();
            //RaisePropertyChanged(() => Items);
        }

        private bool CanExecuteCreateBazresLists()
        {
            return !IsDirty & (KalaChanged | TedadChanged);
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
            changeState();
        }

        private bool CanExecuteCreatListErsal()
        {
            return !IsDirty & (KalaChanged | TedadChanged | DriverChanged | CustomerChanged);
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
                changeState();
                //RaisePropertyChanged(() => Items);
            }
            catch (Exception rt)
            {

                MessageBox.Show(rt.Message.ToString());
            }
        }

        private bool CanExecuteCreateAnbarList()
        {
            return !IsDirty & (KalaChanged | TedadChanged);
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
             changeState();
        }

        private bool CanExecuteCreateImensazanList()
        {
            return !IsDirty & (KalaChanged | TedadChanged);
        }







        private RelayCommand _myCommand79;

        /// <summary>
        /// Gets the CreateAndishehList.
        /// </summary>
        public RelayCommand CreateAndishehList
        {
            get
            {
                return _myCommand79 ?? (_myCommand79 = new RelayCommand(
                    ExecuteCreateAndishehList,
                    CanExecuteCreateAndishehList));
            }
        }

        private void ExecuteCreateAndishehList()
        {
            ReportManager rp = new ReportManager(sefaresh);
            rp.CreatAndishehReportOnDeskTop();
            changeState();

            //RaisePropertyChanged(() => Items);
        }

        private bool CanExecuteCreateAndishehList()
        {
            return !IsDirty & ( KalaChanged | TedadChanged );
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
                changeState();
            }
            catch (Exception rrr)
            {

                MessageBox.Show(rrr.Message.ToString()); ;
            }
        }

        private bool CanExecuteCreateKontrolList()
        {
            //TODO باید اینجا فقط زمانی اینا فعال باشن که اولا ثبت شده باشد یعنی در حال ویرایش و دوما هیچ تغییر ثبت نشده جدیدی وجود نداشته باشد
            return !IsDirty & (KalaChanged | CustomerChanged);
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
            RaisePropertyChanged(() => this.DriversVazn);
            RaisePropertyChanged(() => this.MaghsadVazn);
        }

        private bool CanExecuteLoadSefaresh()
        {
            return !string.IsNullOrEmpty(Tarikh);
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
            DriverSelectionViewModel vm = new DriverSelectionViewModel();
            DriverSelectionView v = new DriverSelectionView();
            v.DataContext = vm;
            Messenger.Default.Send<string>(Tarikh, "ThisSefaresh");
            v.ShowDialog();
            ExecuteLoadSefaresh();
            //var i = sefaresh;
            //RaisePropertyChanged(() => Items);
            changeState();

        }

        private bool CanExecuteSelectDriver()
        {
            return !IsDirty;
        }

        #endregion


        private RelayCommand _myCommand74656;

        /// <summary>
        /// Gets the ShowGoods.
        /// </summary>
        public RelayCommand ShowGoods
        {
            get
            {
                return _myCommand74656
                    ?? (_myCommand74656 = new RelayCommand(ExecuteShowGoods));
            }
        }

        private void ExecuteShowGoods()
        {
            ProductsView p = new ProductsView();
            p.Show();
            //RaisePropertyChanged(() => Items);
        }

        private RelayCommand _myCommand746567;

        /// <summary>
        /// Gets the ShowDrivers.
        /// </summary>
        public RelayCommand ShowDrivers
        {
            get
            {
                return _myCommand746567
                    ?? (_myCommand746567 = new RelayCommand(ExecuteShowDrivers));
            }
        }

        private void ExecuteShowDrivers()
        {
            DriversView p = new DriversView();
            p.Show();
            //RaisePropertyChanged(() => Items);
        }


        private RelayCommand _myCommand659658785;

        /// <summary>
        /// Gets the CreatDriverReport.
        /// </summary>
        public RelayCommand CreatDriverReport
        {
            get
            {
                return _myCommand659658785
                    ?? (_myCommand659658785 = new RelayCommand(ExecuteCreatDriverReport, CanExecuteCreatDriverReport));
            }
        }

        private bool CanExecuteCreatDriverReport()
        {
            return KalaChanged | TedadChanged | CustomerChanged | DriverChanged; 
        }

        private void ExecuteCreatDriverReport()
        {
            try
            {
                ReportManager rp = new ReportManager(sefaresh);
                rp.CreatDriverReport();
                changeState();
                //RaisePropertyChanged(() => Items);
            }
            catch (Exception rrr)
            {

                MessageBox.Show(rrr.Message.ToString()); ;
            }
        }


        private RelayCommand _myCommand55666;

        /// <summary>
        /// Gets the CreatDriverErsalListReport.
        /// </summary>
        public RelayCommand CreatDriverErsalListReport
        {
            get
            {
                return _myCommand55666
                    ?? (_myCommand55666 = new RelayCommand(ExecuteCreatDriverErsalListReport, CanExecuteCreatDriverErsalListReport));
            }
        }

        private bool CanExecuteCreatDriverErsalListReport()
        {
            return KalaChanged | TedadChanged | CustomerChanged | DriverChanged;
        }

        private void ExecuteCreatDriverErsalListReport()
        {
            try
            {
                ReportManager rp = new ReportManager(sefaresh);
                rp.CreatDriverErsalListReport();
                changeState();
                //RaisePropertyChanged(() => Items);
            }
            catch (Exception rrr)
            {

                MessageBox.Show(rrr.Message.ToString()); ;
            }
        }

        private RelayCommand _myCommand465666;

        /// <summary>
        /// Gets the DriverWorksSet.
        /// </summary>
        public RelayCommand DriverWorksSet
        {
            get
            {
                return _myCommand465666 ?? (_myCommand465666 = new RelayCommand(
                    ExecuteDriverWorksSet,
                    CanExecuteDriverWorksSet));
            }
        }

        private void ExecuteDriverWorksSet()
        {
            DriverWorksViewModel vm = new DriverWorksViewModel(sefaresh.Tarikh);
            DriverWorksView v = new DriverWorksView();
            v.DataContext = vm;
            v.ShowDialog();
            //RaisePropertyChanged(() => Items);
        }

        private bool CanExecuteDriverWorksSet()
        {
            return Items.Any(p=>p.Driver!=null);
        }

        private RelayCommand _myCommand565656;

        /// <summary>
        /// Gets the ShowCustomer.
        /// </summary>
        public RelayCommand ShowCustomer
        {
            get
            {
                return _myCommand565656
                    ?? (_myCommand565656 = new RelayCommand(ExecuteShowCustomer));
            }
        }

        private void ExecuteShowCustomer()
        {
            CustomersView p = new CustomersView();
            p.ShowDialog();
            //RaisePropertyChanged(() => Items);
        }

        private RelayCommand _myCommand5565656952;

        /// <summary>
        /// Gets the CreatCheckLists.
        /// </summary>
        public RelayCommand CreatCheckLists
        {
            get
            {
                return _myCommand5565656952 ?? (_myCommand5565656952 = new RelayCommand(
                    ExecuteCreatCheckLists,
                    CanExecuteCreatCheckLists));
            }
        }

        private void ExecuteCreatCheckLists()
        {
            try
            {
                ReportManager rp = new ReportManager(sefaresh);
                rp.CreatCheckListErsalOnDeskTop();
                //RaisePropertyChanged(() => Items);
            }
            catch (Exception rrr)
            {

                MessageBox.Show(rrr.Message.ToString()); ;
            }
        }

        private bool CanExecuteCreatCheckLists()
        {
            return true;
        }

        private RelayCommand _myCommand54664751;

        /// <summary>
        /// Gets the LastAmountSet.
        /// </summary>
        public RelayCommand LastAmountSet
        {
            get
            {
                return _myCommand54664751
                    ?? (_myCommand54664751 = new RelayCommand(ExecuteLastAmountSet));
            }
        }

        private void ExecuteLastAmountSet()
        {
            LastAmountView v = new LastAmountView();
            v.ShowDialog();
            RaisePropertyChanged(() => Items);
        }

        private RelayCommand _myCommand5654652542244;

        /// <summary>
        /// Gets the MontagReciving.
        /// </summary>
        public RelayCommand MontagReciving
        {
            get
            {
                return _myCommand5654652542244
                    ?? (_myCommand5654652542244 = new RelayCommand(ExecuteMontagReciving, CanExecuteMontagReciving));
            }
        }

        private bool CanExecuteMontagReciving()
        {
            return KalaChanged | TedadChanged;
        }

        private void ExecuteMontagReciving()
        {
            MessageBox.Show(ss.MontagReciving(sefaresh.SefareshId));
        }

        private RelayCommand _myCommand56566865;

        /// <summary>
        /// Gets the OracleSet.
        /// </summary>
        public RelayCommand OracleSet
        {
            get
            {
                return _myCommand56566865 ?? (_myCommand56566865 = new RelayCommand(
                    ExecuteOracleSet,
                    CanExecuteOracleSet));
            }
        }

        private void ExecuteOracleSet()
        {
            OracleView v = new OracleView();
            Messenger.Default.Send<string>(sefaresh.Tarikh, "SefareshTarikh");
            v.ShowDialog();
            changeState();
        }

        private bool CanExecuteOracleSet()
        {
            return !IsDirty & !Items.Any(p=>string.IsNullOrEmpty(p.Maghsad)) & (TedadChanged | KalaChanged | CustomerChanged );
        }

        private RelayCommand _myCommand96595;

        /// <summary>
        /// Gets the CreateMaliReport.
        /// </summary>
        public RelayCommand CreateMaliReport
        {
            get
            {
                return _myCommand96595 ?? (_myCommand96595 = new RelayCommand(
                    ExecuteCreateMaliReport,
                    CanExecuteCreateMaliReport));
            }
        }

        private void ExecuteCreateMaliReport()
        {
            ReportManager rp = new ReportManager(sefaresh);
            rp.CreatMaliReport();
            changeState();
        }

        private bool CanExecuteCreateMaliReport()
        {
            return !IsDirty & (KalaChanged | TedadChanged | CustomerChanged);
        }
    }

    public static class ObservableCollectionExtensions
    {
        public static void RemoveAll<T>(this ObservableCollection<T> collection,
                                                           Func<T, bool> condition)
        {
            for (int i = collection.Count - 1; i >= 0; i--)
            {
                if (condition(collection[i]))
                {
                    collection.RemoveAt(i);
                }
            }
        }
    }
}
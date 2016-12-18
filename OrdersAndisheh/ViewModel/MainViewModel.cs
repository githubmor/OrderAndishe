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
            
            

            sefaresh = new Sefaresh();
            Drivers = ss.LoadDrivers();
            Goods = ss.LoadGoods();
            Destinations = ss.LoadDestinations();
            enumList = Enum.GetValues(typeof(ItemType)).OfType<ItemType>().ToList();
            IsDirty = true;
            Tarikh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
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
                //MessageBox.Show("سفارش تاریخ " + Tarikh + "بار گذاری شد");
                RaisePropertyChanged(() => this.Tarikh);
                RaisePropertyChanged(() => this.DriversVazn);
                RaisePropertyChanged(() => this.MaghsadVazn);
            }
            catch (System.Exception r)
            {
                System.Windows.Forms.MessageBox.Show(r.Message.ToString());
            }
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
            }
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

            }
        }


        public string GoodName
        {
            get { return (SelecteddItem != null ? SelecteddItem.Kala : ""); }
        }

        List<Driver> TempDriverForDelete = new List<Driver>();

        private Driver tempDriver;

        public Driver SelectedDriver
        {
            get { return (SelecteddItem != null ? SelecteddItem.Driver : null); }
            set
            {
                
                if (SelecteddItem != null)
                {
                    if (SelecteddItem.Driver!=null)
                    {
                        if (SelecteddItem.Driver.TempDriver!=null)
                        {
                            TempDriverForDelete.Add(SelecteddItem.Driver);
                        }
                    }
                    SelecteddItem.Driver = value;
                }
                else if (value != null)
                {
                    if (value.TempDriver != null)//اگر راننده موقت انتخاب شد ببینه نگذاشته باشه برای حذف
                    {
                        TempDriverForDelete.Remove(value);
                    }
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
                ss.DelNoUsedTempDrivers(TempDriverForDelete);
                IsDirty = false;
                ClickedItem = null;
                RaisePropertyChanged(() => this.DriversVazn);
                RaisePropertyChanged(() => this.MaghsadVazn);
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
             RaisePropertyChanged(() => Items);
        }

        private bool CanExecuteCreateImensazanList()
        {
            return !IsDirty;
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
        }

        private bool CanExecuteCreateAndishehList()
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
            RaisePropertyChanged(() => this.DriversVazn);
            RaisePropertyChanged(() => this.MaghsadVazn);
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
            DriverSelectionViewModel vm = new DriverSelectionViewModel(ss);
            DriverSelectionView v = new DriverSelectionView();
            v.DataContext = vm;
            Messenger.Default.Send<string>(Tarikh, "ThisSefaresh");
            v.ShowDialog();
            //LoadThisDateSefaresh(Tarikh);
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
            p.ShowDialog();
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
            p.ShowDialog();
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
                    ?? (_myCommand659658785 = new RelayCommand(ExecuteCreatDriverReport));
            }
        }

        private void ExecuteCreatDriverReport()
        {
            try
            {
                ReportManager rp = new ReportManager(sefaresh);
                rp.CreatDriverReport();
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
                    ?? (_myCommand55666 = new RelayCommand(ExecuteCreatDriverErsalListReport));
            }
        }

        private void ExecuteCreatDriverErsalListReport()
        {
            try
            {
                ReportManager rp = new ReportManager(sefaresh);
                rp.CreatDriverErsalListReport();
            }
            catch (Exception rrr)
            {

                MessageBox.Show(rrr.Message.ToString()); ;
            }
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
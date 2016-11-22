using BL;
using GalaSoft.MvvmLight;
using OrdersAndisheh.DBL;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OrdersAndisheh.ViewModel
{
    
    public class DriverContainerViewModel : ViewModelBase
    {
        int felaziPalletCount = 0;
        int chobiPalletCount = 0;
        ISefareshService ss;
        List<Driver> driver;
        public DriverContainerViewModel(ISefareshService _ss,int position)
        {
            ss = _ss;
            Mahmole = new ObservableCollection<ItemSefaresh>();
            Mahmole.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(() => this.VaznKol);
                RaisePropertyChanged(() => this.FeleziPalletCount);
                RaisePropertyChanged(() => this.ChobiPalletCount);
                RaisePropertyChanged(() => this.JaigahCount);
                RaisePropertyChanged(() => this.Maghased);
            };
            DriverNumber = position;
            //ss = new SefareshService();
            driver = ss.LoadDrivers();
        }

        private int driverNum;

        public int DriverNumber
        {
            get { return driverNum; }
            set 
            {
                driverNum = value;
                RaisePropertyChanged(() => this.DriverNumber);
            }
        }



        public DriverContainerViewModel(ISefareshService _ss,ObservableCollection<ItemSefaresh> items, int position)
            : this(_ss,position)
        {
            Mahmole = items;
            Mahmole.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(() => this.VaznKol);
                RaisePropertyChanged(() => this.JaigahCount);
                RaisePropertyChanged(() => this.FeleziPalletCount);
                RaisePropertyChanged(() => this.ChobiPalletCount);
                RaisePropertyChanged(() => this.Maghased);
            };
            if (Mahmole[0].Driver!=null)
            {
                SelectedDriver = Mahmole[0].Driver;
            }
        }



        public List<Driver> Drivers
        {
            get 
            { 
                return DriverCal(); 
            }
        }

        private List<Driver> DriverCal()
        {
            //TODO باید اینجا راننده ای که به وزن محموله بخورد فقط بیاید
            return driver;//.Where(p => p.Ton < VaznKol).ToList();
        }

        private Driver myVar;

        public Driver SelectedDriver
        {
            get { return myVar; }
            set 
            { 
                myVar = value;
                RaisePropertyChanged(() => this.SelectedDriver);
            }
        }
        

        public ObservableCollection<ItemSefaresh> Mahmole { get; set; }

        private int vaznCal()
        {
            //string re = "وزن ";
            int s = 0;
            foreach (var item in Mahmole)
            {
                s += item.Vazn ;
            }
            return s;
        }

        public int VaznKol
        {
            get { return vaznCal(); }
        }
        public string JaigahCount
        {
            get { return JaigahCountCal(); }
        }

        private string JaigahCountCal()
        {
           
            return (Math.Ceiling((double)FeleziPalletCount / 2) + ChobiPalletCount).ToString();
        }

        public string Maghased
        {
            get { return MaghasedCal(); }
        }

        private string MaghasedCal()
        {
            string re = "";
            var o = Mahmole.Select(p => p.Maghsad).Distinct().ToList();
            for (int i = 0; i < o.Count; i++)
            {
                if (!string.IsNullOrEmpty(o[i]))
                {
                    re += o[i] + (i+1 < o.Count ? " - " : null);
                }
            }
            //foreach (var item in o)
            //{
            //    if (string.IsNullOrEmpty(item))
            //    {
            //        re += item + " - ";
            //    }
            //}
            return re;
        }

        public int FeleziPalletCount //{ get; set; }
        {
            get 
            {
                return PalletFelezCount();
            }
        }

        private int PalletFelezCount()
        {
            felaziPalletCount = 0;
            foreach (var item in Mahmole)
            {
                if (item.Product.Pallet.Vazn > 30)
                {
                    felaziPalletCount += int.Parse(item.Pallet);
                }
            }

            return felaziPalletCount;
        }
        public int ChobiPalletCount //{ get; set; }
        {
            get
            {
                return PalletChobiCount();
            }
        }

        private int PalletChobiCount()
        {
            chobiPalletCount = 0;
            foreach (var item in Mahmole)
            {
                if (item.Product.Pallet.Vazn < 30)
                {
                    chobiPalletCount += int.Parse(item.Pallet);
                }
            }

            return chobiPalletCount;
        }

        //براي اينه كه اگه كاربر نخواست سيو كنه الكي تغييرات نره داخل ابجكت
        public void AssignDriver()
        {
            foreach (var item in Mahmole)
            {
                item.Driver = SelectedDriver;
            }
        }
    }
}
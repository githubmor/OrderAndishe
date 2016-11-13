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
        SefareshService ss;
        List<Driver> driver;
        public DriverContainerViewModel(int position)
        {
            Mahmole = new ObservableCollection<ItemSefaresh>();
            Mahmole.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(() => this.VaznKol);
                RaisePropertyChanged(() => this.FeleziPalletCount);
                RaisePropertyChanged(() => this.ChobiPalletCount);
                RaisePropertyChanged(() => this.JaigahCount);
            };
            DriverNumber = position;
            ss = new SefareshService();
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
        


        public DriverContainerViewModel(ObservableCollection<ItemSefaresh> items,int position):this(position)
        {
            Mahmole = items;
            Mahmole.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(() => this.VaznKol);
                RaisePropertyChanged(() => this.JaigahCount);
                RaisePropertyChanged(() => this.FeleziPalletCount);
                RaisePropertyChanged(() => this.ChobiPalletCount);
            };
            //DriverNumber = position;
        }


        //private int myVar;

        public List<Driver> Drivers
        {
            get 
            { 
                return DriverCal(); 
            }
            //set { myVar = value; }
        }

        private List<Driver> DriverCal()
        {
            return driver;//.Where(p => p.Ton < VaznKol).ToList();
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
        //public string ChobiPalletCount { get; set; }
    }
}
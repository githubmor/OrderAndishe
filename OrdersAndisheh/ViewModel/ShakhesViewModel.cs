using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
using OrdersAndisheh.BL.Importing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace OrdersAndisheh.ViewModel
{
    public class ShakhesViewModel : ViewModelBase
    {
        ShakhesService ss;

        private string selectedSal;

        public ShakhesViewModel()
        {
            ss = new ShakhesService();

            Sal = ss.LoadYears();
            amar = new List<AmartolidKhodro>();
           
        }

        public List<string> Sal { get; set; }

        public string SelectedSal
        {
            get { return selectedSal; }
            private set
            {
                selectedSal = value;
                RaisePropertyChanged(() => SelectedSal);
                RaisePropertyChanged(() => Mah);
            }
        }

        public List<string> Mah
        {
            get { return ss.LoadMonth(SelectedSal); }

        }


        private string selectedMah;
        public string SelectedMah
        {
            get { return selectedMah; }
            private set
            {
                selectedMah = value;
                RaisePropertyChanged(() => SelectedMah);
                Recalculate();
            }
        }

        private void Recalculate()
        {
            AmarTolidList = ss.GetAmarTolid(SelectedSal, SelectedMah);
            RaisePropertyChanged(() => AmarTolidList);

            AmarErsal = ss.GetAmarErsal(SelectedSal, SelectedMah);
            RaisePropertyChanged(() => AmarErsal);


            TolidSazandeh = "تعداد كل توليد سازندگان : ";
            var y = amar.GroupBy(p => p.Sazandeh).ToList();

            foreach (var item in y)
	        {
                TolidSazandeh += item.Key + " : " + item.Sum(p => p.Tadad) + " - ";
	        }

            TolidSazandeh += "مجموع : " + amar.Sum(p=>p.Tadad);

            RaisePropertyChanged(() => TolidSazandeh);

            DarsadSahm = ersal.Get_Sazandeh_Sherkat_Avreg();

            TedadErsal = ersal.Get_Sazandeh_Sherkat_Tedad();
            
            RaisePropertyChanged(() => DarsadSahm);
            RaisePropertyChanged(() => TedadErsal);

        }

        private List<AmartolidKhodro> amar;

        public List<AmartolidKhodro> AmarTolidList
        {
            get
            {
                return amar;
            }
            private set
            {
                amar = value;
                RaisePropertyChanged(() => AmarTolidList);
            }
        }

        private AmarErsals ersal;

        public AmarErsals AmarErsal
        {
            get
            {
                return ersal;
            }
            private set
            {
                ersal = value;
                RaisePropertyChanged(() => AmarErsal);
            }
        }

        private string tolidSazandeh;

        public string TolidSazandeh
        {
            get
            {
                return tolidSazandeh;
            }
            private set
            {
                tolidSazandeh = value;
                RaisePropertyChanged(() => TolidSazandeh);
            }
        }

        private string darsadSahm;

        public string DarsadSahm
        {
            get
            {
                return darsadSahm;
            }
            private set
            {
                darsadSahm = value;
                RaisePropertyChanged(() => DarsadSahm);
            }
        }

        private string tedadErsal;

        public string TedadErsal
        {
            get
            {
                return tedadErsal;
            }
            private set
            {
                tedadErsal = value;
                RaisePropertyChanged(() => TedadErsal);
            }
        }
        
    }

    
}

public class AmarErsals : List<AmarErsalMahsol>
{
    public string Get_Sazandeh_Sherkat_Tedad()
    {
        string re = "تعداد ارسال شركت به سازندگان  : ";

        var e = this.GroupBy(p => new { p.Sazandeh, p.Sherkat })
            .Select(ww => new
            {
                Sazandeh = ww.Key.Sazandeh,
                Sherkat = ww.Key.Sherkat,
                Sum = ww.Sum(p => p.Tadad )
            })
            .ToList();

        foreach (var item in e)
        {
            re += item.Sazandeh +
               "/" + item.Sherkat + " : " + item.Sum + " - ";
            //Math.Round(item.Avreg.Where(p => p.Tadad > 0 & p.Sazandeh == item.Sazandeh).Average(p=>p.Sahm)) + " - ";
        }
        return re;
    }
    public string Get_Sazandeh_Sherkat_Avreg()
    {
        string re = "ميانگين درصد سهم سازندگان به شركت : ";
        
            var e = this.GroupBy(p => new { p.Sazandeh, p.Sherkat })
                .Select(ww => new
                {
                    Sazandeh = ww.Key.Sazandeh,
                    Sherkat = ww.Key.Sherkat,
                    Avreg = (ww.Where(p => p.Sahm > 0).Count()>0?ww.Where(p => p.Sahm > 0).Average(p=>p.Sahm_100_Percent):0)
                })
                .ToList();

            foreach (var item in e)
            {
                 re += item.Sazandeh +
                    "/" + item.Sherkat + " : " + Math.Round(item.Avreg) + " - ";
                    //Math.Round(item.Avreg.Where(p => p.Tadad > 0 & p.Sazandeh == item.Sazandeh).Average(p=>p.Sahm)) + " - ";
            }
        return re;
    }
    
    

}

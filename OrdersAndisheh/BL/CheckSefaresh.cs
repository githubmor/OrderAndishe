using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class CheckSefaresh : INotifyPropertyChanged
    {

        //Sefaresh sefaresh;
        //public CheckSefaresh(Sefaresh s)
        //{
        //    this.sefaresh = s;
        //    NotifyPropertyChanged("TarikhSefaresh");
        //    NotifyPropertyChanged("HasItemWithNoTedad");
        //    NotifyPropertyChanged("HasItemWithNoMaghsad");
        //    NotifyPropertyChanged("HasItemWithNoRanande");
        //    NotifyPropertyChanged("HasItemWithNoTahvilFrosh");
        //}

        public CheckSefaresh(string tarikh,
            bool hasAnyZeroTedad, bool hasAnyEmptyMaghsad, bool hasAnyEmptyRanande,bool hasAnyTempRanande,
            bool hasAnyZeroTahvilFrosh,
            bool hasAnyEmptyOracle)
        {
            TarikhSefaresh = tarikh;
            IsTedadOk = !hasAnyZeroTedad;
            IsMaghsadOk = !hasAnyEmptyMaghsad;
            IsRanandeOk = !hasAnyEmptyRanande & !hasAnyTempRanande;
            IsTahvilFroshOk = !hasAnyZeroTahvilFrosh;
            IsOracleOk = !hasAnyEmptyOracle;

        }

        //private Sefaresh myVar;

        //public Sefaresh Sefaresh
        //{
        //    get { return sefaresh; }
        //    set { sefaresh = value; }
        //}
        

        public string TarikhSefaresh {get; set;}
        //{
        //    get { return sefaresh.Tarikh; }
        //}

        //private int myVar;

        public bool IsEveryThingOk
        {
            get { return IsMaghsadOk & IsOracleOk & IsRanandeOk & IsTahvilFroshOk & IsTedadOk; }
            //set { myVar = value; }
        }
        

        public bool IsTedadOk { get; set; }
        public bool HasItemWithNoOracle { get; set; }
        //{
        //    get { return !sefaresh.Items.Any(p => p.Tedad == 0); }
        //}
        public bool IsMaghsadOk { get; set; }
        //{
        //    get { return !sefaresh.Items.Any(p => p.Maghsad==""); }
        //}
        public bool IsRanandeOk { get; set; }
        //{
        //    get 
        //    {
        //        if (!sefaresh.Items.Any(p => p.Ranande == ""))
        //        {
        //            return !sefaresh.Items.Any(p => p.Driver.TempDriver != null);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //        //return !sefaresh.Items.Any(p => p.Ranande == "") && !sefaresh.Items.Any(p => p.Driver.TempDriver !=null); 
        //    }
        //}
        public bool IsTahvilFroshOk { get; set; }
        public bool IsOracleOk { get; set; }
        //{
        //    get { return !sefaresh.Items.Any(p => p.TahvilFrosh == 0); }
        //}
        //public bool AllItemHasNotTempDriver
        //{
        //    get
        //    {
        //        foreach (var item in sefaresh.Items)
        //        {
        //            if (item.Driver == null)
        //            {
        //                return false;
        //            }
        //            else if(item.Driver.TempDriver!=null)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
                
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        
    }
}

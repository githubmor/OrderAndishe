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
        Sefaresh sefaresh;
        public CheckSefaresh(Sefaresh s)
        {
            this.sefaresh = s;
            NotifyPropertyChanged("TarikhSefaresh");
            NotifyPropertyChanged("HasItemWithNoTedad");
            NotifyPropertyChanged("HasItemWithNoMaghsad");
            NotifyPropertyChanged("HasItemWithNoRanande");
            NotifyPropertyChanged("HasItemWithNoTahvilFrosh");
        }

        public string TarikhSefaresh
        {
            get { return sefaresh.Tarikh; }
        }

        public bool HasItemWithNoTedad
        {
            get { return !sefaresh.Items.Any(p=>p.Tedad==0); }
        }
        public bool HasItemWithNoMaghsad
        {
            get { return !sefaresh.Items.Any(p => p.Maghsad==""); }
        }
        public bool HasItemWithNoRanande
        {
            get 
            {
                if (!sefaresh.Items.Any(p => p.Ranande == ""))
                {
                    return !sefaresh.Items.Any(p => p.Driver.TempDriver != null);
                }
                else
                {
                    return false;
                }
                //return !sefaresh.Items.Any(p => p.Ranande == "") && !sefaresh.Items.Any(p => p.Driver.TempDriver !=null); 
            }
        }
        public bool HasItemWithNoTahvilFrosh
        {
            get { return !sefaresh.Items.Any(p => p.TahvilFrosh == 0); }
        }
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

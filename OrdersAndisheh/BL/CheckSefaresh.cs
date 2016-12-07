using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class CheckSefaresh
    {
        Sefaresh sefaresh;
        public CheckSefaresh(Sefaresh s)
        {
            this.sefaresh = s;
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
            get { return !sefaresh.Items.Any(p => p.Ranande == ""); }
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
        //            if (item.Driver==null)
        //            {
        //                return null;
        //            }
        //        }
        //        return sefaresh.Items.Where(i=>i.Driver!=null).Any(p => p.Driver.TempDriver == null); 
        //    }
        //}
    }
}

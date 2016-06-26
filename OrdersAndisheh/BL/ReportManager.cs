using BL;
using OrdersAndisheh.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class ReportManager
    {
        private Sefaresh sefaresh;

        public ReportManager(Sefaresh sefaresh)
        {
            if (sefaresh==null)
            {
                throw new ApplicationException("");
            }
            if (sefaresh.Items==null)
            {
                throw new ApplicationException("");
            }
            this.sefaresh = sefaresh;
            اینجا باید سفارشات یک بازرس را جدا کنیم
        }

       
        public void CreatBazresReport()
        {
            //foreach (var item in sefaresh.Items)
            //{
            //    if (item.Product.Bazres)
            //    {
                    
            //    }
            //}

        }

        public void CreatAnbarReport()
        {
            throw new NotImplementedException();
        }

        public void CreatImenSazanReport()
        {
            throw new NotImplementedException();
        }

        public void CreatKontrolReport()
        {
            throw new NotImplementedException();
        }

        public void CreatListErsalReport()
        {
            throw new NotImplementedException();
        }

        
    }
}

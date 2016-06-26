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
        }

       
        public void CreatAllBazresReport()
        {
            var bazres = sefaresh.Items.Select(p => p.BazresName).Distinct();
            foreach (var item in bazres)
            {
                List<ReportRow> reportRows = new List<ReportRow>();
                var r = sefaresh.Items.Where(p=>p.BazresName==item).ToList();
                foreach (var b in r)
                {
                    reportRows.Add(new ReportRow(b.Kala, b.Tedad.ToString(),"", "", b.Maghsad, "", ""));
                }
                FileManagar fg = new FileManagar(reportRows);
                fg.CreatDocFile(item);
            }

        }

        public void CreatAnbarReport()
        {
            //FileManagar fg = new FileManagar();

            //fg.CreatDocFile("انبار");
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

﻿using BL;
using OrdersAndisheh.DBL;
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
                throw new ApplicationException("سفارش نمیتواند تهی باشد");
            }
            if (sefaresh.Items==null)
            {
                throw new ApplicationException("سفارش نمیتواند بدون آیتم باشد");
            }
            this.sefaresh = sefaresh;
        }

       
        public void CreatAllBazresReportOnDeskTop()
        {
            var bazres = sefaresh.Items.Select(p => p.BazresName).Distinct();
            foreach (var name in bazres)
            {
                List<ReportRow> reportRows = new List<ReportRow>();
                var allBazres = sefaresh.Items.Where(p=>p.BazresName==name).ToList();
                foreach (var b in allBazres)
                {
                    reportRows.Add(new ReportRow() { Kala = b.Kala, Tedad = b.Tedad.ToString(), Maghsad = b.Maghsad });
                }
                FileManagar fg = new FileManagar(reportRows,sefaresh.Tarikh);
                fg.CreatFile(name);
            }

        }

        public void CreatAnbarReport()
        {
            List<ReportRow> reportRows = new List<ReportRow>();
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow() { Kala = b.Kala, Tedad = b.Tedad.ToString(), Karton = b.Karton.ToString(), Pallet = b.Pallet.ToString(), Maghsad = b.Maghsad, Ranande = b.Ranande });
            }
            FileManagar fg = new FileManagar(reportRows,sefaresh.Tarikh);
            fg.CreatFile("Anbar");
           
        }

        public void CreatImenSazanReport()
        {
            List<ReportRow> reportRows = new List<ReportRow>();
            var ImenKalas = sefaresh.Items.Where(p => p.IsImenKala).ToList();
            foreach (var b in ImenKalas)
            {
                reportRows.Add(new ReportRow() { Kala = b.Kala, Tedad = b.Tedad.ToString(), Maghsad = b.Maghsad });
            }
            FileManagar fg = new FileManagar(reportRows, sefaresh.Tarikh);
            fg.CreatFile("ImenSazan");
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

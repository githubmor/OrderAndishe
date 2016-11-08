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
            this.sefaresh.Items.OrderBy(p => p.Maghsad).ToList();
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
                    reportRows.Add(new ReportRow() { Kala = b.Kala, Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), Maghsad = b.Maghsad });
                }
                FileManagar fg = new FileManagar(reportRows,sefaresh.Tarikh);
                if (name == "فهامه")
                {
                    fg.CreatDocFile(name);
                }
                else
                {
                    fg.CreatFile(name);
                }
                
            }

        }

        public void CreatAnbarReportOnDeskTop()
        {
            List<ReportRow> reportRows = new List<ReportRow>();
            sefaresh.Items.OrderBy(o => o.Ranande).OrderBy(p => p.Maghsad).ToList();
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow() { Kala = b.Kala, Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), Karton = b.Karton.ToString(), Pallet = b.Pallet.ToString(), Maghsad = b.Maghsad, Ranande = b.Ranande });
            }
            
            FileManagar fg = new FileManagar(reportRows,sefaresh.Tarikh);
            fg.CreatFile("Anbar");
           
        }

        public void CreatImenSazanReportOnDeskTop()
        {
            List<ReportRow> reportRows = new List<ReportRow>();
            var ImenKalas = sefaresh.Items.Where(p => p.IsImenKala).ToList();
            foreach (var b in ImenKalas)
            {
                reportRows.Add(new ReportRow() { Kala = b.Kala, Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), Maghsad = b.Maghsad });
            }
            FileManagar fg = new FileManagar(reportRows, sefaresh.Tarikh);
            fg.CreatFile("ImenSazan");
        }

        public void CreatKontrolReportOnDeskTop()
        {
            List<ReportRow> reportRows = new List<ReportRow>();
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow() { Kala = b.Kala, Maghsad = b.Maghsad, Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : "") ,Karton = b.CodeKala}); //TODO اینو باید بعدا حذف کنیم 
            }
            FileManagar fg = new FileManagar(reportRows, sefaresh.Tarikh);
            fg.CreatFile("Kontrol");
        }

        public void CreatListErsalReportOnDeskTop()
        {
            List<ReportRow> reportRows = new List<ReportRow>();
            string lastDriver = "";//, lastDes = "";
            foreach (var b in sefaresh.Items)
            {
                if (lastDriver!="")
                {
                    if ( b.Ranande!=lastDriver)
                    {
                        reportRows.Add(new ReportRow() { Kala = "khali" });
                    }
                }
                reportRows.Add(new ReportRow() 
                { 
                    Kala = b.Kala, 
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), 
                    Karton = b.Karton.ToString(), 
                    Pallet = b.Pallet.ToString(), 
                    Vazn = (b.Vazn > 0 ? b.Vazn.ToString() : ""), 
                    Maghsad = b.Maghsad, 
                    Ranande = b.Ranande 
                });
                //lastDes = b.Maghsad;
                lastDriver = b.Ranande;
                
                
            }
            FileManagar fg = new FileManagar(reportRows, sefaresh.Tarikh);
            fg.CreatFile("Ersal");
        }

        
    }
}

using BL;
using OrdersAndisheh.DBL;
using OrdersAndisheh.View;
using OrdersAndisheh.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.Where(p => p.ItemKind != (byte)ItemType.ارسال).OrderBy(p => p.Maghsad).ToList());
        }

        public void CreatDriverReport()
        {
            int pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad
                    ,Ranande = (b.Driver.TempDriver == null ? b.Ranande : "       ")
                    ,Pallet = b.PalletCount.ToString(),
                    Karton = b.Karton
                });
                    pos += 1;
            }
            //foreach (var item in sefaresh.Items)
            //{
            //    if (item.PalletCount>0)
            //    {
            //        if (item.Product.Pallet.Vazn>120)
            //        {
            //            item.Des = item.PalletCount + "پالت فلزی";
            //        }
            //        else
            //        {
            //            item.Des = item.PalletCount + "پالت چوبی";
            //        }

            //    }
            //    else
            //    {
            //        item.Des = item.Karton + "کارتن / سبد";
            //    }
            //}
            FileManagar fg = new FileManagar(reportRows,"");
            fg.CreatDriverFile("Driver");
        }

       
        public void CreatAllBazresReportOnDeskTop()
        {
            var bazres = sefaresh.Items.Select(p => p.BazresName).Distinct();
            foreach (var name in bazres)
            {
                int pos = 0;
                List<ReportRow> reportRows = new List<ReportRow>();
                var allBazres = sefaresh.Items.Where(p=>p.BazresName==name).ToList();
                foreach (var b in allBazres)
                {
                    reportRows.Add(new ReportRow() { Position = pos, Kala = b.Kala, 
                        Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), Maghsad = b.Maghsad
                        ,Ranande=(b.ItemKind==(byte)ItemType.فوری?"فوری":"")});
                    pos += 1;
                }

                FileManagar fg = NewMethod(reportRows);
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

        private FileManagar NewMethod(List<ReportRow> reportRows)
        {
            ReportPreviewViewModel vm = new ReportPreviewViewModel(reportRows);
            ReportPerviewView v = new ReportPerviewView();
            v.DataContext = vm;
            v.ShowDialog();


            FileManagar fg = new FileManagar(vm.reportRows, sefaresh.Tarikh);
            return fg;
        }

        public void CreatAnbarReportOnDeskTop()
        {
            int pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.Ranande).ThenBy(p => p.ItemKind).ThenBy(p => p.Maghsad).ToList());
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Karton = b.Karton.ToString(),
                    Pallet = b.PalletCount.ToString(),
                    Maghsad = b.Maghsad
                    ,Ranande = (b.ItemKind != (byte)ItemType.ارسال & b.ItemKind != (byte)ItemType.عادی ?
                        GetEnumValue<ItemType>(b.ItemKind).ToString() + " - " + b.Ranande : b.Ranande)
                });
                pos += 1;
            }

            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("Anbar");
           
        }

        public void CreatImenSazanReportOnDeskTop()
        {
            int pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            var ImenKalas = sefaresh.Items.Where(p => p.IsImenKala).OrderBy(p => p.Ranande).ThenBy(p => p.ItemKind).ThenBy(p => p.Maghsad).ToList();
            foreach (var b in ImenKalas)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad ,
                    Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : "")
                });
                pos += 1;
            }
            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("ImenSazan");
        }
        public void CreatAndishehReportOnDeskTop()
        {
            int pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            var ImenKalas = sefaresh.Items.Where(p => !p.IsImenKala).OrderBy(p => p.Ranande).ThenBy(p => p.ItemKind).ThenBy(p => p.Maghsad).ToList();
            foreach (var b in ImenKalas)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad,
                    Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : "")
                });
                pos += 1;
            }
            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("Andisheh");
        }

        public void CreatKontrolReportOnDeskTop()
        {
            int pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.Ranande).ThenBy(p => p.ItemKind).ThenBy(p => p.Maghsad).ToList());
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow() { Position = pos, Kala = b.Kala, Maghsad = b.Maghsad
                    ,Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : "")
                });
                pos += 1;
            }
            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("Kontrol");
        }

        public void CreatListErsalReportOnDeskTop()
        {
            int pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            string lastDriver = "";//, lastDes = "";
            sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.Ranande)
                .ThenBy(p => p.Maghsad).ThenBy(p => p.ItemKind).ToList());
            foreach (var b in sefaresh.Items)
            {
                if (lastDriver!="")
                {
                    if ( b.Ranande!=lastDriver)
                    {
                        reportRows.Add(new ReportRow() { Position = pos, Kala = "khali" });
                        pos += 1;
                    }
                }
                reportRows.Add(new ReportRow() 
                { 
                    Position = pos,
                    Kala = b.Kala, 
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), 
                    Karton = b.Karton.ToString(), 
                    Pallet = b.PalletCount.ToString(), 
                    Maghsad = b.Maghsad,
                    Ranande = (b.Driver.TempDriver == null ? b.Ranande : "")
                });
                pos += 1;
                //lastDes = b.Maghsad;
                lastDriver = b.Ranande;
                
                
            }


            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("Ersal");
        }

        public T GetEnumValue<T>(byte intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToByte(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }
        
    }
}

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
        private Sefaresh sefaresh2;
        SefareshService service;
        int pos = 0;
        //public ReportManager(string sefareshTarikh)
        //{
        //    service = new SefareshService();
        //    this.sefaresh = service.LoadSefaresh(sefareshTarikh);
        //    this.sefaresh2 = service.LoadSefaresh(sefareshTarikh);
        //    this.sefaresh.Items = new ObservableCollection<ItemSefaresh>(
        //        sefaresh.Items.Where(p => p.ItemKind != (byte)ItemType.ارسال)
        //        .OrderBy(p => p.Ranande)
        //        .ThenBy(p => p.ItemKind)
        //        .ThenBy(p => p.Maghsad).ToList());
        //}
        public ReportManager(Sefaresh s)
        {
            service = new SefareshService();

            this.sefaresh = service.LoadSefaresh(s.Tarikh);

            foreach (var item in sefaresh.Items)
            {
                item.IsCustomerChanged = s.Items.Single(p => p.OrderDetail.Id == item.OrderDetail.Id).IsCustomerChanged;
                item.IsDriverChanged = s.Items.Single(p => p.OrderDetail.Id == item.OrderDetail.Id).IsDriverChanged;
                item.IsNew = s.Items.Single(p => p.OrderDetail.Id == item.OrderDetail.Id).IsNew;
                item.IsTedadChanged = s.Items.Single(p => p.OrderDetail.Id == item.OrderDetail.Id).IsTedadChanged;
                item.IsSelected = s.Items.Single(p => p.OrderDetail.Id == item.OrderDetail.Id).IsSelected;
            }

            this.sefaresh2 = service.LoadSefaresh(s.Tarikh);
            this.sefaresh.Items = new ObservableCollection<ItemSefaresh>(
                sefaresh.Items.Where(p => p.ItemKind != (byte)ItemType.ارسال)
                .OrderBy(p => p.Ranande)
                .ThenBy(p => p.ItemKind)
                .ThenBy(p => p.Maghsad).ToList());
            
        }

        public void CreatDriverReport()
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            var DrsWorks = service.LoadDriverWorksForThisSefaresh(sefaresh.SefareshId);
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad
                    ,Ranande = (b.Driver.TempDriver == null ? b.Ranande : "           ")
                    ,Pallet = b.PalletCount.ToString(),
                    Karton = b.Karton,
                    Pelak = b.Driver.Pelak,
                    IsDriverChanged = b.IsDriverChanged,
                    IsKalaChanged = b.IsNew,
                    IsCustomerChanged = b.IsCustomerChanged,
                    IsTedadChanged = b.IsTedadChanged
                });
                    pos += 1;
            }
            FileManagar fg = new FileManagar(reportRows, "");
            fg.CreatDriverFile(DrsWorks, "Driver",false);
        }

        

       
        public void CreatAllBazresReportOnDeskTop(bool showPreview = true)
        {
            var bazres = sefaresh.Items.Select(p => p.BazresName).Distinct();
            foreach (var name in bazres)
            {
                pos = 0;
                List<ReportRow> reportRows = new List<ReportRow>();
                var allBazres = sefaresh.Items.Where(p=>p.BazresName==name).ToList();
                foreach (var b in allBazres)
                {
                    reportRows.Add(new ReportRow() { Position = pos, Kala = b.Kala, 
                        Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), Maghsad = b.Maghsad
                        ,
                        IsDriverChanged = b.IsDriverChanged,
                        IsKalaChanged = b.IsNew,
                        IsCustomerChanged = b.IsCustomerChanged,
                        IsTedadChanged = b.IsTedadChanged,Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : "")
                    });
                    pos += 1;
                }

                FileManagar fg = GetDataAfterPreview(reportRows, showPreview);
                if (name == "فهامه")
                {
                    fg.CreatDocFile(name,false);
                }
                else
                {
                    fg.CreatFile(name, false);
                }
                
            }

        }

        private FileManagar GetDataAfterPreview(List<ReportRow> reportRows,bool showReportPreview)
        {
            

            if (showReportPreview)
            {
               ReportPreviewViewModel vm = new ReportPreviewViewModel(reportRows);
                ReportPerviewView v = new ReportPerviewView();
                v.DataContext = vm;
                v.ShowDialog();
                reportRows = vm.reportRows;
            }


            FileManagar fg = new FileManagar(reportRows, sefaresh.Tarikh);
            return fg;
        }

        public void CreatAnbarReportOnDeskTop(bool showPreview = true)
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            //sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.Ranande)
            //    .ThenBy(p => p.ItemKind).ThenBy(p => p.Maghsad).ToList());
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Karton = b.Karton.ToString(),
                    Pallet = (b.PalletCount > 0 ? b.PalletCount.ToString() : ""),
                    Maghsad = b.Maghsad
                    ,
                    IsDriverChanged = b.IsDriverChanged,
                    IsKalaChanged = b.IsNew,
                    IsCustomerChanged = b.IsCustomerChanged,
                    IsTedadChanged = b.IsTedadChanged,
                    Ranande = (b.ItemKind != (byte)ItemType.ارسال & b.ItemKind != (byte)ItemType.عادی ?
                        GetEnumValue<ItemType>(b.ItemKind).ToString() + " - " + b.Ranande : b.Ranande)
                });
                pos += 1;
            }

            FileManagar fg = NewMethod(reportRows,true);
            fg.CreatFile("Anbar");
           
            FileManagar fg = GetDataAfterPreview(reportRows, showPreview);
            fg.CreatPalletTabloFile("PalletTablo", false);

        }

        public void CreatImenSazanReportOnDeskTop(bool showPreview = true)
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            var ImenKalas = sefaresh.Items.Where(p => p.IsImenKala);
            foreach (var b in ImenKalas)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad ,
                    Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : ""),
                    IsDriverChanged = b.IsDriverChanged,
                    IsKalaChanged = b.IsNew,
                    IsCustomerChanged = b.IsCustomerChanged,
                    IsTedadChanged = b.IsTedadChanged
                });
                pos += 1;
            }
            FileManagar fg = GetDataAfterPreview(reportRows, showPreview);
            fg.CreatFile("ImenSazan", false);
        }
        public void CreatAndishehReportOnDeskTop(bool showPreview = true)
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            var ImenKalas = sefaresh.Items.Where(p => !p.IsImenKala);
            foreach (var b in ImenKalas)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad,
                    Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : ""),
                    IsDriverChanged = b.IsDriverChanged,
                    IsKalaChanged = b.IsNew,
                    IsCustomerChanged = b.IsCustomerChanged,
                    IsTedadChanged = b.IsTedadChanged
                });
                pos += 1;
            }
            FileManagar fg = NewMethod(reportRows,true);
            fg.CreatFile("Andisheh");
        }
        public void CreatMaliReport()
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.IsNew)
                .ThenBy(p => p.Product.FaniCode).ToList());
            //var ImenKalas = sefaresh.Items.Where(p => !p.IsImenKala);
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Karton = b.Product.FaniCode,
                    Pallet = b.Des,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad,
                    IsDriverChanged = b.IsDriverChanged,
                    IsKalaChanged = b.IsNew,
                    IsCustomerChanged = b.IsCustomerChanged,
                    IsTedadChanged = b.IsTedadChanged
                    
                });
                pos += 1;
            }
            FileManagar fg = GetDataAfterPreview(reportRows, showPreview);
            fg.CreatFileMali("Mali", false);
        }

        public void CreatKontrolReportOnDeskTop(bool showPreview = true)
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            //sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.Ranande)
            //    .ThenBy(p => p.ItemKind).ThenBy(p => p.Maghsad).ToList());
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow() { Position = pos, Kala = b.Kala, Maghsad = b.Maghsad
                    ,Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : "")
                });
                pos += 1;
            }
            FileManagar fg = GetDataAfterPreview(reportRows, showPreview);
            fg.CreatFile("Kontrol", false);
        }

        public void CreatCheckListErsalOnDeskTop(bool showPreview = true)
        {
            List<CheckList> cs = new List<CheckList>();
            sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.Maghsad)
                .ThenBy(p => p.Ranande).ToList());
            List<ItemSefaresh> dd = sefaresh.Items.ToList();
            if (sefaresh.Items.Any(p=>p.IsSelected))
            {
                dd = sefaresh.Items.Where(p => p.IsSelected).ToList();
            }
            foreach (var item in dd)
            {
                cs.Add(new CheckList(item, sefaresh.Tarikh));
            }

            FileManagar f = new FileManagar(cs, sefaresh.Tarikh);
            f.CreatCheckListFile();
            
        }
        public void CreatListErsalReportOnDeskTop(bool showPreview = true)
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow() 
                { 
                    Position = pos,
                    Kala = b.Kala, 
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""), 
                    Karton = b.Karton.ToString(), 
                    Pallet = (b.PalletCount > 0 ? b.PalletCount.ToString() : ""),
                    Maghsad = b.Maghsad,
                    Ranande = (b.Driver !=null?(b.Driver.TempDriver == null ? b.Ranande : ""):""),
                    IsDriverChanged = b.IsDriverChanged,
                    IsKalaChanged = b.IsNew,
                    IsCustomerChanged = b.IsCustomerChanged,
                    IsTedadChanged = b.IsTedadChanged
                });
                pos += 1;
            }


            FileManagar fg = GetDataAfterPreview(reportRows, showPreview);
            fg.CreatFile("Ersal", false);
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


        public void CreatDriverErsalListReport()
        {
            pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
            foreach (var b in sefaresh.Items)
            {
                reportRows.Add(new ReportRow()
                {
                    Position = pos,
                    Kala = b.Kala,
                    Tedad = (b.Tedad > 0 ? b.Tedad.ToString() : ""),
                    Maghsad = b.Maghsad,
                    Ranande = (b.Driver.TempDriver == null ? b.Ranande : "           "),
                    Pallet = b.PalletCount.ToString(),
                    Karton = b.Karton,
                    Pelak = b.Driver.Pelak,
                    IsDriverChanged = b.IsDriverChanged,
                    IsKalaChanged = b.IsNew,
                    IsCustomerChanged = b.IsCustomerChanged,
                    IsTedadChanged = b.IsTedadChanged
                });
                pos += 1;
            }

            FileManagar fg = new FileManagar(reportRows, "");
            fg.CreatDriverErsalFile("DriverErsal", false);
        }



        public void CreatAllReport()
        {
            
            this.CreatAllBazresReportOnDeskTop(false);
            this.CreatAnbarReportOnDeskTop(false);
            this.CreatAndishehReportOnDeskTop(false);
            this.CreatImenSazanReportOnDeskTop(false);
            this.CreatKontrolReportOnDeskTop(false);
            this.CreatMaliReport(false);
        }
    }
}

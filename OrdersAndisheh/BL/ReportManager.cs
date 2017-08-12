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
        public ReportManager(string sefareshTarikh)
        {
            service = new SefareshService();
            this.sefaresh = service.LoadSefaresh(sefareshTarikh);
            this.sefaresh2 = service.LoadSefaresh(sefareshTarikh);
            this.sefaresh.Items = new ObservableCollection<ItemSefaresh>(
                sefaresh.Items.Where(p => p.ItemKind != (byte)ItemType.ارسال)
                .OrderBy(p => p.Ranande)
                .ThenBy(p => p.ItemKind)
                .ThenBy(p => p.Maghsad).ToList());
        }

        public void CreatDriverReport()
        {
            int pos = 0;
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
            fg.CreatDriverFile(DrsWorks, "Driver");
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
                        ,
                        IsDriverChanged = b.IsDriverChanged,
                        IsKalaChanged = b.IsNew,
                        IsCustomerChanged = b.IsCustomerChanged,
                        IsTedadChanged = b.IsTedadChanged,Ranande = (b.ItemKind == (byte)ItemType.فوری ? "فوری" : "")
                    });
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

            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("Anbar");
           
        }

        public void CreatImenSazanReportOnDeskTop()
        {
            int pos = 0;
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
            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("ImenSazan");
        }
        public void CreatAndishehReportOnDeskTop()
        {
            int pos = 0;
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
            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("Andisheh");
        }
        public void CreatMaliReport()
        {
            int pos = 0;
            List<ReportRow> reportRows = new List<ReportRow>();
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
            FileManagar fg = NewMethod(reportRows);
            fg.CreatFileMali("Mali");
        }

        public void CreatKontrolReportOnDeskTop()
        {
            int pos = 0;
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
            FileManagar fg = NewMethod(reportRows);
            fg.CreatFile("Kontrol");
        }

        public void CreatCheckListErsalOnDeskTop()
        {
            List<CheckList> cs = new List<CheckList>();
            sefaresh.Items = new ObservableCollection<ItemSefaresh>(sefaresh.Items.OrderBy(p => p.Maghsad)
                .ThenBy(p => p.Ranande).ToList());
            foreach (var item in sefaresh2.Items)
            {
                cs.Add(new CheckList(item, sefaresh2.Tarikh));
            }

            FileManagar f = new FileManagar(cs, sefaresh2.Tarikh);
            f.CreatCheckListFile();
            
        }
        public void CreatListErsalReportOnDeskTop()
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


        public void CreatDriverErsalListReport()
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
            fg.CreatDriverErsalFile("DriverErsal");
        }
    }
}

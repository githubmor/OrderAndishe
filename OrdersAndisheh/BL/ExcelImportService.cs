using BL;
using OfficeOpenXml;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class ExcelImportService
    {
        ISefareshService service;
        ExcelPackage package;
        public ExcelImportService(ISefareshService service)
        {
            this.service = service;
            
        }




        public ObservableCollection<TahvilItem> GetTahvilfroshData(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            package = new ExcelPackage(f);
            ObservableCollection<TahvilItem> items = new ObservableCollection<TahvilItem>();
            ExcelWorksheet WorkSheet = package.Workbook.Worksheets[1];
            var start = WorkSheet.Dimension.Start;
            var end = WorkSheet.Dimension.End;

            int CodeKalaCol = 5;
            int KalaNameCol = 4;
            int TarafeMoghabelCol = 2;
            int TahvilFroshNumCol = 3;
            int TedadCol = 6;
            int TarikhSanadCol = 12;

            for (int i = 1; i < end.Column; i++)
			{
                if (WorkSheet.Cells[1, i].Text == "کد کالا")
                {
                    CodeKalaCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "طرف مقابل")
                {
                    TarafeMoghabelCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "مقدار")
                {
                    TedadCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "شماره سند")
                {
                    TahvilFroshNumCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "تاریخ سند")
                {
                    TarikhSanadCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "عنوان کالا")
                {
                    KalaNameCol = i;
                }
			}

            //ExcelAddress dd= WorkSheet.

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                TahvilItem s = new TahvilItem();
                s.KalaName = WorkSheet.Cells[row, KalaNameCol].Text;
                s.CodeKala = WorkSheet.Cells[row, CodeKalaCol].Text;
                s.TarafeMoghabel = WorkSheet.Cells[row, TarafeMoghabelCol].Text;
                s.TahvilFroshNum = short.Parse(WorkSheet.Cells[row, TahvilFroshNumCol].Text);
                s.Tedad = Math.Abs(WorkSheet.Cells[row, TedadCol].Text != "" ? int.Parse(WorkSheet.Cells[row, TedadCol].Text) : 0);
                s.TarikhSanad = WorkSheet.Cells[row, TarikhSanadCol].Text;
                items.Add(s);
            }

            return items;
        }

        public ObservableCollection<Asn> GetAsnData(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            package = new ExcelPackage(f);
            ObservableCollection<Asn> items = new ObservableCollection<Asn>();
            ExcelWorksheet WorkSheet = package.Workbook.Worksheets[1];
            var start = WorkSheet.Dimension.Start;
            var end = WorkSheet.Dimension.End;

            int FaniCol = 5;
            int KalaNameCol = 4;
            int AnbarNumCol = 2;
            int AsnNumCol = 3;
            int TedadCol = 6;
            int VaziatAsnCol = 12;
            int RanandeCol = 5;
            int RanandePelakCol = 1;
            int RanandeTelCol = 6;

            for (int i = 1; i <= end.Column; i++)
            {
                string t = WorkSheet.Cells[1, i].Text;
                if (WorkSheet.Cells[1, i].Text == "شماره فني")
                {
                    FaniCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "محل تحويل")
                {
                    AnbarNumCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "ASN مقدار")
                {
                    TedadCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "ASN ش")
                {
                    AsnNumCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "ASN وضعيت")
                {
                    VaziatAsnCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "شرح قطعه")
                {
                    KalaNameCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "تلفن راننده")
                {
                    RanandeCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "بازه تحو?ل")
                {
                    RanandePelakCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "شماره ماش?ن")
                {
                    RanandeTelCol = i;
                }
            }

            //ExcelAddress dd= WorkSheet.

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                Asn s = new Asn();
                s.KalaName = WorkSheet.Cells[row, KalaNameCol].Text;
                s.FaniCode = WorkSheet.Cells[row, FaniCol].Text;
                s.AnbarNumber = WorkSheet.Cells[row, AnbarNumCol].Text;
                s.AsnNumber = WorkSheet.Cells[row, AsnNumCol].Text;
                s.Tedad = (WorkSheet.Cells[row, TedadCol].Text != "" ? int.Parse(WorkSheet.Cells[row, TedadCol].Text) : 0);
                s.VaziatAsn = WorkSheet.Cells[row, VaziatAsnCol].Text;
                s.RanandeName = WorkSheet.Cells[row, RanandeCol].Text;
                s.RanandePelak = WorkSheet.Cells[row, RanandePelakCol].Text;
                s.RanandeTel = WorkSheet.Cells[row, RanandeTelCol].Text;
                items.Add(s);
            }

            return items;


        }

        public ObservableCollection<AmountDto> GetAmountData(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            package = new ExcelPackage(f);
            ObservableCollection<AmountDto> items = new ObservableCollection<AmountDto>();
            ExcelWorksheet WorkSheet = package.Workbook.Worksheets[1];
            var start = WorkSheet.Dimension.Start;
            var end = WorkSheet.Dimension.End;

            int CodeKalaCol = 5;
            int KalaNameCol = 4;
            int TedadCol = 6;
            
            for (int i = 1; i < end.Column; i++)
            {
                if (WorkSheet.Cells[1, i].Text == "کد کالا")
                {
                    CodeKalaCol = i;
                }
                
                if (WorkSheet.Cells[1, i].Text == "موجودی")
                {
                    TedadCol = i;
                }
                
                if (WorkSheet.Cells[1, i].Text == "کالا")
                {
                    KalaNameCol = i;
                }
            }

            //ExcelAddress dd= WorkSheet.

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                AmountDto a = new AmountDto();
                a.CodeKala = WorkSheet.Cells[row, CodeKalaCol].Text;
                a.KalaName = WorkSheet.Cells[row, KalaNameCol].Text;
                a.LastAmount = double.Parse(WorkSheet.Cells[row, TedadCol].Text);

                items.Add(a);
            }

            return items;


        }
    }
}

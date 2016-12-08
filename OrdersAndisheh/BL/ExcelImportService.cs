using BL;
using OfficeOpenXml;
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
        public ExcelImportService(ISefareshService service,string filePath)
        {
            this.service = service;
            FileInfo f = new FileInfo(filePath);
            package = new ExcelPackage(f);
        }




        public ObservableCollection<TahvilItem> GetTahvilfroshData()
        {
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
    }
}

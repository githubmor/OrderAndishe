using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.ImportFromExcel
{
    
    public class TahvilFroshImporter : AbstractExcelImporter
    {
        
        public override void CalculateData(List<IDataColumn> columns)
        {
            //TODO اینجا باید اطلاعات رو از ورکشیت بگیریم و بخونیم بزاریم تو tahvil
            foreach (var item in columns)
            {
                
            }
        }
        public TahvilFroshImporter()
        {

        }
       
       
        //public void GetData(string filePath)
        //{
        //    FileInfo f = new FileInfo(filePath);
        //    ExcelPackage package = new ExcelPackage(f);
        //    ExcelWorksheet WorkSheet = package.Workbook.Worksheets[1];
        //    var start = WorkSheet.Dimension.Start;
        //    var end = WorkSheet.Dimension.End;

        //    int CodeKalaCol = 5;
        //    int KalaNameCol = 4;
        //    int TarafeMoghabelCol = 2;
        //    int TahvilFroshNumCol = 3;
        //    int TedadCol = 6;
        //    int TarikhSanadCol = 12;

        //    for (int i = 1; i < end.Column; i++)
        //    {
        //        if (WorkSheet.Cells[1, i].Text == "کد کالا")
        //        {
        //            CodeKalaCol = i;
        //        }
        //        if (WorkSheet.Cells[1, i].Text == "طرف مقابل")
        //        {
        //            TarafeMoghabelCol = i;
        //        }
        //        if (WorkSheet.Cells[1, i].Text == "مقدار")
        //        {
        //            TedadCol = i;
        //        }
        //        if (WorkSheet.Cells[1, i].Text == "شماره سند")
        //        {
        //            TahvilFroshNumCol = i;
        //        }
        //        if (WorkSheet.Cells[1, i].Text == "تاریخ سند")
        //        {
        //            TarikhSanadCol = i;
        //        }
        //        if (WorkSheet.Cells[1, i].Text == "عنوان کالا")
        //        {
        //            KalaNameCol = i;
        //        }
        //    }

        //    for (int row = start.Row + 1; row <= end.Row; row++)
        //    {
        //        TahvilItem s = new TahvilItem();
        //        s.KalaName = WorkSheet.Cells[row, KalaNameCol].Text;
        //        s.CodeKala = WorkSheet.Cells[row, CodeKalaCol].Text;
        //        s.TarafeMoghabel = WorkSheet.Cells[row, TarafeMoghabelCol].Text;
        //        s.TahvilFroshNum = short.Parse(WorkSheet.Cells[row, TahvilFroshNumCol].Text);
        //        s.Tedad = Math.Abs(WorkSheet.Cells[row, TedadCol].Text != "" ? int.Parse(WorkSheet.Cells[row, TedadCol].Text) : 0);
        //        s.TarikhSanad = WorkSheet.Cells[row, TarikhSanadCol].Text;
        //        Tahvils.Add(s);
        //    }
        //}

        //private List<TahvilItem> tahvils;

        //public List<TahvilItem> Tahvils
        //{
        //    get { return tahvils; }
        //    set { tahvils = value; }
        //}
        
    }
}

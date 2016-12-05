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

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                TahvilItem s = new TahvilItem();
                s.KalaName = WorkSheet.Cells[row, 5].Text;
                s.CodeKala = WorkSheet.Cells[row, 4].Text;
                s.Maghsad = WorkSheet.Cells[row, 2].Text;
                s.TahvilFroshNum = short.Parse(WorkSheet.Cells[row, 3].Text);
                s.Tedad = Math.Abs(WorkSheet.Cells[row, 6].Text!=""?int.Parse(WorkSheet.Cells[row, 6].Text):0);
                s.TarikhSanad = WorkSheet.Cells[row, 12].Text;
                items.Add(s);
            }

            return items;
        }
    }
}

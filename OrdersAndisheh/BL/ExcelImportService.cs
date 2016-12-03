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
                items.Add(new TahvilItem()
                {
                    KalaName = WorkSheet.Cells[row, 6].Text,
                    CodeKala = WorkSheet.Cells[row, 5].Text,
                    Maghsad = WorkSheet.Cells[row, 4].Text,
                    TahvilFroshNum = int.Parse(WorkSheet.Cells[row, 2].Text),
                    Tedad = int.Parse(WorkSheet.Cells[row, 7].Text),
                    TarikhSanad = WorkSheet.Cells[row, 16].Text
                });
            }

            return items;
        }
    }
}

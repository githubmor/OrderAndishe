using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public abstract class FactoryImporter
    {
        //ExcelPackage package;
        protected ExcelWorksheet WorkSheet;
        public FactoryImporter(string path)
        {
            FileInfo f = new FileInfo(path);
            ExcelPackage package = new ExcelPackage(f);
            WorkSheet = package.Workbook.Worksheets[1];
            FileAddress = f.FullName;
            FileName = f.Name;
            ColumnCount = WorkSheet.Dimension.Columns;
            start = WorkSheet.Dimension.Start;
            end = WorkSheet.Dimension.End;
        }
        public string FileAddress { get; set; }
        public string FileName { get; set; }
        public int ColumnCount { get; set; }
        protected ExcelCellAddress start;
        protected ExcelCellAddress end;
        public abstract List<IImportData> GetDataFromPath();
    }
}

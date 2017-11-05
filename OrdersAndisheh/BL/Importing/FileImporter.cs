using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public class FileImporter
    {
        protected ExcelWorksheet WorkSheet;
        public FileImporter(string path)
        {
            FileInfo f = new FileInfo(path);
            ExcelPackage package = new ExcelPackage(f);
            WorkSheet = package.Workbook.Worksheets[1];
            //FileAddress = f.FullName;
            //FileName = f.Name;
            //ColumnCount = WorkSheet.Dimension.Columns;
            //start = WorkSheet.Dimension.Start;
            //end = WorkSheet.Dimension.End;
        }
        public ImportDataKind GetSample()
        {
            throw new System.NotImplementedException();
        }

        public IBaseImportData GetImportdata()
        {
            throw new System.NotImplementedException();
        }
    }
}

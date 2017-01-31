using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.ImportFromExcel
{
    public abstract class AbstractExcelImporter
    {
        private string path;
        protected ExcelWorksheet WorkSheet;
        private void GetData(string path)
        {
            FileInfo f = new FileInfo(path);
            ExcelPackage package = new ExcelPackage(f);
            WorkSheet = package.Workbook.Worksheets[1];
            //var start = WorkSheet.Dimension.Start;
            //var end = WorkSheet.Dimension.End;
        }

        public AbstractExcelImporter(string FilePath)
        {
            this.GetData(FilePath);
        }

        public AbstractExcelImporter()
        {

        }
        
        public abstract void CalculateData(List<IDataColumn> columns);
    }
}

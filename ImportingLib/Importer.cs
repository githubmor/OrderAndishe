

using EPPlus.DataExtractor;
using OfficeOpenXml;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace ImportingLib
{
    public class Importer
    {
        protected ExcelWorksheet WorkSheet;
        int dataRowCount = 0;
        public Importer(string path)
        {
            FileInfo f = new FileInfo(path);
            var p = f.Exists;
            ExcelPackage package = new ExcelPackage(f);
            WorkSheet = package.Workbook.Worksheets[1];

            sampleData = GetSampleData();
            sampleData.Address = path;
            sampleData.FileName = package.File.Name;

        }

        private SampleData sampleData;

        public SampleData SampleData
        {
            get { return sampleData; }
            private set { sampleData = value; }
        }
        

        private SampleData GetSampleData()
        {
            SampleData data = new SampleData();
            int sampleRowCount = 11;
            var start = WorkSheet.Dimension.Start;
            var end = WorkSheet.Dimension.End;
            dataRowCount = end.Row;

            for (int column = start.Column; column < end.Column; column++)
			{
                if (!string.IsNullOrEmpty(WorkSheet.Cells[1, column].Text))
                {
                    Column c = new Column() { Header = WorkSheet.Cells[1, column].Text, Position = column.ToString() };

                    for (int row = start.Row+1 ; row <= sampleRowCount; row++)
                    {
                        c.Values.Add(WorkSheet.Cells[row, column].Text);
                    }
                    data.Columns.Add(c); 
                }
			}
            return data;
        }

       
        public List<ImportData> GetImportDataWithMatch(ImportData match)
        {
            List<ImportData> cars = WorkSheet
                     .Extract<ImportData>()
                            // Here we can chain multiple definition for the columns
                        .WithProperty(p => p.Car, (match.Car == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.Car))))
                        .WithProperty(p => p.Codekala, (match.Codekala == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.Codekala))))
                        .WithProperty(p => p.DriverName, (match.DriverName == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.DriverName))))
                        .WithProperty(p => p.FaniCode, (match.FaniCode == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.FaniCode))))
                        .WithProperty(p => p.Maghsad, (match.Maghsad == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.Maghsad))))
                        .WithProperty(p => p.Pelak, (match.Pelak == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.Pelak))))
                        .WithProperty(p => p.Phone, (match.Phone == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.Phone))))
                        .WithProperty(p => p.SanadNumber, (match.SanadNumber == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.SanadNumber))))
                        .WithProperty(p => p.Tarikh, (match.Tarikh == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.Tarikh))))
                        .WithProperty(p => p.Tedad, (match.Tedad == null ? "ABC" : ExcelColumnIndexToName(int.Parse(match.Tedad))))
                        .GetData(2, dataRowCount) // To obtain the data we indicate the start and end of the rows.
                            // In this case, the rows 4, 5 and 6 will be extracted.
                     .ToList();

            cars.RemoveAll(p => string.IsNullOrEmpty(p.Codekala) & string.IsNullOrEmpty(p.Tedad));
            

            return cars;
        }
        private string ExcelColumnIndexToName(int Index)
        {
            Index -= 1;
            string range = string.Empty;
            if (Index < 0) return range;
            int a = 26;
            int x = (int)Math.Floor(Math.Log((Index) * (a - 1) / a + 1, a));
            Index -= (int)(Math.Pow(a, x) - 1) * a / (a - 1);
            for (int i = x + 1; Index + i > 0; i--)
            {
                range = ((char)(65 + Index % a)).ToString() + range;
                Index /= a;
            }
            return range;
        }
    }
}

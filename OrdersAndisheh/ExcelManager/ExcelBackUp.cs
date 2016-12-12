using BL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersAndisheh.ExcelManager
{
    public class ExcelBackUp
    {
        ISefareshService service;
        ExcelPackage package;
        FileInfo excelTemplate = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"Report\ErsalTemplate.xlsx");

        public ExcelBackUp(ISefareshService s)
        {
            this.service = s;
            package = new ExcelPackage();
        }

        public void ExportLastSavedSefaresh(string Tarikh)
        {
            var sefaresh = service.LoadSefaresh(Tarikh);

            

            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    string path = dlg.SelectedPath;
                    string file = PersianDateTime.Parse(sefaresh.Tarikh).DayOfYear + ".xlsx";
                    string excelFileName = Path.Combine(path, file);
                    FileInfo excelForSave = new FileInfo(excelFileName);

                    using (var package = new ExcelPackage(excelForSave, excelTemplate))
                    {
                        ExcelWorksheet ws = package.Workbook.Worksheets[1];
                        var t = sefaresh.Items;
                        for (int i = 0; i < t.Count; i++)
                        {
                            ws.Cells[i + 2, 1].Value = t[i].CodeKala;
                            ws.Cells[i + 2, 2].Value = t[i].Tedad.ToString();
                            ws.Cells[i + 2, 3].Value = t[i].Kala;
                            ws.Cells[i + 2, 4].Value = t[i].PalletCount.ToString();
                            ws.Cells[i + 2, 5].Value = t[i].Karton.ToString();
                            ws.Cells[i + 2, 7].Value = t[i].Vazn.ToString();
                            ws.Cells[i + 2, 8].Value = t[i].BazresName;
                            ws.Cells[i + 2, 9].Value = t[i].Maghsad;
                            ws.Cells[i + 2, 10].Value = t[i].Ranande;
                            ws.Cells[i + 2, 11].Value = t[i].TahvilFrosh.ToString();
                        }
                        package.Save();
                    }
                }
            }

            
        }

    }
}

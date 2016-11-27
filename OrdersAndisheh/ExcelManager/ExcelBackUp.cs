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
                        for (int i = 1; i < t.Count; i++)
                        {
                            ws.Cells[i + 1, 1].Value = t[i].CodeKala;
                            ws.Cells[i + 1, 2].Value = t[i].Tedad.ToString();
                            ws.Cells[i + 1, 3].Value = t[i].Kala;
                            ws.Cells[i + 1, 4].Value = t[i].Pallet.ToString();
                            ws.Cells[i + 1, 5].Value = t[i].Karton.ToString();
                            ws.Cells[i + 1, 7].Value = t[i].Vazn.ToString();
                            ws.Cells[i + 1, 8].Value = t[i].BazresName;
                            ws.Cells[i + 1, 9].Value = t[i].Maghsad;
                            ws.Cells[i + 1, 10].Value = t[i].Ranande;
                            ws.Cells[i + 1, 11].Value = t[i].TahvilFrosh.ToString();
                        }
                        package.Save();
                    }
                }
            }

            
        }

    }
}

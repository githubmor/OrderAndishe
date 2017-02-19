using BL;
using OfficeOpenXml;
using OrdersAndisheh.Properties;
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

        public string ExportLastSavedSefaresh(string Tarikh)
        {
            string re = "";
            var sefaresh = service.LoadSefaresh(Tarikh);

            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                //Properties.Settings.Default.Upgrade();
                dlg.SelectedPath = Properties.Settings.Default.LastExcelBackUpPath;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.LastExcelBackUpPath = dlg.SelectedPath;
                    Properties.Settings.Default.Save();
                    re = Properties.Settings.Default.LastExcelBackUpPath;
                    string path = Path.Combine(dlg.SelectedPath , PersianDateTime.Parse(sefaresh.Tarikh).Year.ToString());
                    string path2 = Path.Combine(path, PersianDateTime.Parse(sefaresh.Tarikh).MonthName.ToString());
                    Directory.CreateDirectory(path2);
                    string file = PersianDateTime.Parse(sefaresh.Tarikh).Day + ".xlsx";
                    string excelFileName = Path.Combine(path2, file);
                    
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
            return re;

            
        }

    }
}

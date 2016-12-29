﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BL
{
    using OrdersAndisheh.BL;
    using Stimulsoft.Report;
    using Stimulsoft.Report.Export;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

	public class FileManagar
	{

        private List<ReportRow> ReportRows;
        //private List<DriverReportRow> DriverReportRows;
        private string Tarikh;
        private List<CheckList> CheckList;
        //private ObservableCollection<ItemSefaresh> items;


        public FileManagar(List<ReportRow> reportRows, string Tarikh)
        {
            this.ReportRows = reportRows;
            this.Tarikh = Tarikh;
        }

        public FileManagar(List<CheckList> reportRows, string Tarikh)
        {
            this.CheckList = reportRows;
            this.Tarikh = Tarikh;
        }
        //public FileManagar(ObservableCollection<ItemSefaresh> observableCollection)
        //{
        //    // TODO: Complete member initialization
        //    this.items = observableCollection;
        //}


        public void CreatDriverFile(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    path = dlg.SelectedPath;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "Report";
                    }
                    
                    StiReport mainreport = new StiReport();
                    mainreport.RegBusinessObject("Items", ReportRows);
                    mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\DriverListReport.mrt");
                    mainreport.Render();
                    mainreport.Show();
                    mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + fileName + ".pdf");
                }
            }
        }
        
		public void CreatFile(string fileName)
		{
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //MessageBox.Show("You selected: " + dlg.SelectedPath);
                    path = dlg.SelectedPath;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "Report";
                    }
                    var h = new Header();
                    h.Tarikh = Tarikh;
                    h.WeekDay = PersianDateTime.Parse(Tarikh).DayName;



                    StiReport mainreport = new StiReport();
                    mainreport.RegBusinessObject("Sefaresh", h);
                    mainreport.RegBusinessObject("Items", ReportRows);
                    mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\Report2.mrt");
                    mainreport.Render();
                    mainreport.Show();
                    mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + fileName + ".pdf");
                }
            }
            
		}

        public virtual void CreatDocFile(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //MessageBox.Show("You selected: " + dlg.SelectedPath);
                    path = dlg.SelectedPath;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "Report";
                    }
                    var h = new Header();
                    h.Tarikh = Tarikh;
                    h.WeekDay = PersianDateTime.Parse(Tarikh).DayName;



                    StiReport mainreport = new StiReport();
                    mainreport.RegBusinessObject("Sefaresh", h);
                    mainreport.RegBusinessObject("Items", ReportRows);
                    mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\Report.mrt");
                    mainreport.Render();
                    mainreport.Show();
                    mainreport.ExportDocument(StiExportFormat.RtfWinWord, path + "\\" + fileName + ".doc");
                }
            }
            
        }

        public void CreatCheckListFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //var settings = new StiPdfExportSettings() { ImageQuality = 1.0f, ImageResolution = 300, EmbeddedFonts = true, UseUnicode = false, StandardPdfFonts = true };
                    path = dlg.SelectedPath;
                    foreach (var item in CheckList)
                    {
                        StiReport mainreport = new StiReport();
                        mainreport.RegBusinessObject("Item", item);
                        mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\CheckListReport.mrt");
                        mainreport.Render();
                        //mainreport.Show();
                        mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + item.CodeKala + ".pdf");
                    }
                }
            }

        }

        public void CreatDriverErsalFile(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    path = dlg.SelectedPath;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        fileName = "Report";
                    }

                    StiReport mainreport = new StiReport();
                    mainreport.RegBusinessObject("Items", ReportRows);
                    mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\DriverErsalListReport.mrt");
                    mainreport.Render();
                    mainreport.Show();
                    mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + fileName + ".pdf");
                }
            }
        }
    }
    public class Header
    {
        public string Tarikh { get; set; }
        public string WeekDay { get; set; }
    }
}


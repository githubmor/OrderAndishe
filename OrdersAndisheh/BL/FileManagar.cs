namespace BL
{
    using OfficeOpenXml;
    using OrdersAndisheh.BL;
    using OrdersAndisheh.DBL;
    using Stimulsoft.Report;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

	public class FileManagar
	{

        private List<ReportRow> ReportRows;
        //private List<DriverReportRow> DriverReportRows;
        private string Tarikh;
        private List<CheckList> CheckList;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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

        public FileManagar()
        {

        }

        public void CreatePlaskoreport(List<ItemSefaresh> items,string path)
        {
            FileInfo f = new FileInfo(path);
            using (ExcelPackage package = new ExcelPackage(f))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("test");

                worksheet.Cells["A1"].LoadFromCollection(items, true, OfficeOpenXml.Table.TableStyles.Medium1);

                package.Save();
            }
        }
        //public FileManagar(ObservableCollection<ItemSefaresh> observableCollection)
        //{
        //    // TODO: Complete member initialization
        //    this.items = observableCollection;
        //}


        public void CreatDriverFile(List<DriverWork> rt , string fileName)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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

                    CreateReportFile(fileName, null, "DriverListReport", StiExportFormat.Pdf,false);
                    //StiReport mainreport = new StiReport();
                    //mainreport.RegBusinessObject("Items", ReportRows);
                    //mainreport.RegBusinessObject("Works", rt);
                    //mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\DriverListReport.mrt");
                    //mainreport.Render();
                    //mainreport.Show();
                    //mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + fileName + ".pdf");
                }
            }
        }
        
		public void CreatFile(string fileName)
		{
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
                    h.WeekDay = getWeekDay();



                    CreateReportFile(fileName, h,"Report2",StiExportFormat.Pdf,false);
                }
            }
            
		}

        private void CreateReportFile(string fileName, Header h,string ReportFileName,StiExportFormat fileFormat,bool showreportAfterCreate)
        {
            string extension = "";
            switch (fileFormat)
            {
                case StiExportFormat.Excel:
                    extension = "xls";
                    break;
                case StiExportFormat.Excel2007:
                    extension = "xlsx";
                    break;
                case StiExportFormat.Pdf:
                    extension = "pdf";
                    break;
                case StiExportFormat.Word2007:
                    extension = "docx";
                    break;
                default:
                    extension = "pdf";
                    break;
            }
            StiReport mainreport = new StiReport();
            mainreport.RegBusinessObject("Sefaresh", h);
            mainreport.RegBusinessObject("Items", ReportRows);
            mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\" + ReportFileName + ".mrt");
            mainreport.Render(false);
            if (showreportAfterCreate)
            {
                mainreport.Show();
            }
            mainreport.ExportDocument(fileFormat, path + "\\" + fileName + "." + extension);
        }
        public void CreatFileMali(string fileName)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
                    h.WeekDay = getWeekDay();


                    CreateReportFile(fileName, h, "MaliReport", StiExportFormat.Pdf, false);
                    //StiReport mainreport = new StiReport();
                    //mainreport.RegBusinessObject("Sefaresh", h);
                    //mainreport.RegBusinessObject("Items", ReportRows);
                    //mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\MaliReport.mrt");
                    //mainreport.Render();
                    //mainreport.Show();
                    //mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + fileName + ".pdf");
                }
            }

        }

        public virtual void CreatDocFile(string fileName)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
                    h.WeekDay = getWeekDay();


                    CreateReportFile(fileName, h, "Report2", StiExportFormat.Word2007, false);
                    //StiReport mainreport = new StiReport();
                    //mainreport.RegBusinessObject("Sefaresh", h);
                    //mainreport.RegBusinessObject("Items", ReportRows);
                    //mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\Report2.mrt");
                    //mainreport.Render();
                    //mainreport.Show();
                    //mainreport.ExportDocument(StiExportFormat.RtfWinWord, path + "\\" + fileName + ".doc");
                }
            }
            
        }

        private string getWeekDay()
        {
            return PersianDateTime.Parse(Tarikh).DayName +" - ساعت " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        }

        public void CreatCheckListFile()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //var settings = new StiPdfExportSettings() { ImageQuality = 1.0f, ImageResolution = 300, EmbeddedFonts = true, UseUnicode = false, StandardPdfFonts = true };
                    path = dlg.SelectedPath;
                    for (int i = 0; i < CheckList.Count; i++)
                    {
                        StiReport mainreport = new StiReport();
                        mainreport.RegBusinessObject("Item", CheckList[i]);
                        mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\CheckListReport.mrt");
                        mainreport.Render();
                        //mainreport.Show();
                        mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + i + ".pdf");
                    }
                    
                }
            }

        }

        public void CreatDriverErsalFile(string fileName)
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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

                    CreateReportFile(fileName, null, "DriverErsalListReport", StiExportFormat.Pdf, false);
                    //StiReport mainreport = new StiReport();
                    //mainreport.RegBusinessObject("Items", ReportRows);
                    //mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\DriverErsalListReport.mrt");
                    //mainreport.Render();
                    //mainreport.Show();
                    //mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\" + fileName + ".pdf");
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


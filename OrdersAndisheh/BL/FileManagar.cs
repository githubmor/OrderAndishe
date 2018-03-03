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
        private string Tarikh;
        private List<CheckList> CheckList;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


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

        public void CreatDriverFile(List<DriverWork> rt , string fileName = "DriverReport",bool IsLocationSelection = true)
        {
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }
            CreateReportFile(fileName, null, "DriverListReport", StiExportFormat.Pdf, false);
        }

        private void GetSavingLocation()
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    path = dlg.SelectedPath;
                }

            }
        }

        public void CreatFile(string fileName = "Report", bool IsLocationSelection = true, bool IsShowReportAfterCreat = false)
		{
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }
            var h = new Header();
            h.Tarikh = Tarikh;
            h.WeekDay = getWeekDay();

            CreateReportFile(fileName, h, "Report2", StiExportFormat.Pdf, IsShowReportAfterCreat);
            
		}
        public void CreatPalletTabloFile(string fileName = "PalletTablo", bool IsLocationSelection = true)
        {
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }

            CreateReportFile(fileName, null, "PalletTabloReport", StiExportFormat.Pdf, false);

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
        public void CreatFileMali(string fileName = "MaliReport", bool IsLocationSelection = true)
        {
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }
            var h = new Header();
            h.Tarikh = Tarikh;
            h.WeekDay = getWeekDay();

            //CreateReportFile(fileName, h, "MaliReport", StiExportFormat.Excel2007, false);
            CreateReportFile(fileName, h, "MaliReport2", StiExportFormat.Excel2007, false);
        }

        public virtual void CreatDocFile(string fileName = "DocReport", bool IsLocationSelection = true)
        {
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }
            var h = new Header();
            h.Tarikh = Tarikh;
            h.WeekDay = getWeekDay();


            CreateReportFile(fileName, h, "Report2", StiExportFormat.Word2007, false);
            
        }

        private string getWeekDay()
        {
            return PersianDateTime.Parse(Tarikh).DayName +" - ساعت " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
        }

        public void CreatCheckListFile()
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    path = dlg.SelectedPath;
                    for (int i = 0; i < CheckList.Count; i++)
                    {
                        StiReport mainreport = new StiReport();
                        mainreport.RegBusinessObject("Item", CheckList[i]);
                        mainreport.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Report\\CheckListReport.mrt");
                        mainreport.Render(false);
                        //mainreport.Show();
                        mainreport.ExportDocument(StiExportFormat.Pdf, path + "\\"
                            + CheckList[i].NameKala +" - "+ CheckList[i].RanandeName + ".pdf");
                    }
                    
                }
            }
            //GetSavingLocation();
            //for (int i = 0; i < CheckList.Count; i++)
            //{
            //    CreateReportFile(i.ToString(), null, "CheckListReport", StiExportFormat.Pdf, false);
            //}

        }

        public void CreatDriverErsalFile(string fileName = "DriverErsalReport", bool IsLocationSelection = true)
        {
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }
            CreateReportFile(fileName, null, "DriverErsalListReport", StiExportFormat.Pdf, false);
        }

        public void CreatCheckFile(string fileName = "CheckReport", bool IsLocationSelection = true)
        {
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }
            var h = new Header();
            h.Tarikh = Tarikh;
            h.WeekDay = getWeekDay();

            CreateReportFile(fileName, h, "CheckReport", StiExportFormat.Pdf, false);
        }

        public void CreatKartablFile(string fileName = "KartablReport", bool IsLocationSelection = true)
        {
            if (IsLocationSelection)
            {
                GetSavingLocation();
            }
            var h = new Header();
            h.Tarikh = Tarikh;
            h.WeekDay = getWeekDay();

            CreateReportFile(fileName, h, "KartablReport", StiExportFormat.Pdf, false);
        }
    }
    public class Header
    {
        public string Tarikh { get; set; }
        public string WeekDay { get; set; }
    }
}


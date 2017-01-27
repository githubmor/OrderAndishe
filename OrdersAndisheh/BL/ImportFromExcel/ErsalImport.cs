using BL;
using OfficeOpenXml;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OrdersAndisheh.BL.ImportFromExcel
{
    public class ErsalImport : ExcelImporter
    {
        SefareshService service;
        ExcelPackage package;
        public ErsalImport(SefareshService service)
        {
            this.service = service;
            
        }

        
        public object GetData(string filePath)
        {
            string month = "05";
            FileInfo f = new FileInfo(filePath);
            package = new ExcelPackage(f);
            List<Order> items = new List<Order>();
            //string month = package.File.Name.;
            foreach (var WorkSheet in package.Workbook.Worksheets)
            {
                int day;
                //bool res = int.TryParse(WorkSheet.Name, out num1);
                if (int.TryParse(WorkSheet.Name, out day))
                {
                    Order order = new Order();

                    order.Tarikh = "1395/" + month + (day<=9?"/0":"/") + day;
                    //}
                    //ExcelWorksheet WorkSheet = package.Workbook.Worksheets[1];
                    var start = WorkSheet.Dimension.Start;
                    var end = WorkSheet.Dimension.End;

                    int CodeKalaCol = 5;
                    int MaghsadCol = 2;
                    //int TahvilFroshNumCol = 3;
                    int TedadCol = 2;
                    int RanandeCol = 12;
                    

                    for (int i = 1; i <= end.Column; i++)
                    {
                        if (WorkSheet.Cells[1, i].Text == "کد محصول")
                        {
                            CodeKalaCol = i;
                        }
                        if (WorkSheet.Cells[1, i].Text == "مقصد")
                        {
                            MaghsadCol = i;
                        }
                        //if (WorkSheet.Cells[1, i].Text == "تعداد قطعه ارسالی")
                        //{
                        //    TedadCol = i;
                        //}
                        //if (WorkSheet.Cells[1, i].Text == "تحویل فروش")
                        //{
                        //    TahvilFroshNumCol = i;
                        //}
                        if (WorkSheet.Cells[1, i].Text == "راننده")
                        {
                            RanandeCol = i;
                        }
                    }



                    for (int row = start.Row + 1; row <= end.Row; row++)
                    {
                        Product po = service.GetProduct(WorkSheet.Cells[row, CodeKalaCol].Text);
                        Customer co = service.GetCustomer(WorkSheet.Cells[row, MaghsadCol].Text);
                        Driver dot = service.GetDriver(WorkSheet.Cells[row, RanandeCol].Text);
                        short Tedad = short.Parse(WorkSheet.Cells[row, TedadCol].Text != "" ? WorkSheet.Cells[row, TedadCol].Text : "0");

                        if (po == null)
                        {
                            throw new ApplicationException("کد کالای " +
                                WorkSheet.Cells[row, CodeKalaCol].Text + " در برگه" + WorkSheet.Name + " در دیتابیس پیدا نشد");
                        }
                        //if (co == null)
                        //{
                        //    MessageBox.Show("مشتری با نام  " +
                        //        WorkSheet.Cells[row, MaghsadCol].Text + " در برگه" + WorkSheet.Name + " در دیتابیس پیدا نشد");
                        //}
                        //if (co == null)
                        //{
                        //    MessageBox.Show("راننده با نام  " +
                        //        WorkSheet.Cells[row, RanandeCol].Text + " در برگه" + WorkSheet.Name + " در دیتابیس پیدا نشد");
                        //}
                        OrderDetail s = new OrderDetail();
                        s.Product = service.GetProduct(WorkSheet.Cells[row, CodeKalaCol].Text);
                        s.Customer = service.GetCustomer(WorkSheet.Cells[row, MaghsadCol].Text);
                        //s.TahvilForosh = short.Parse(WorkSheet.Cells[row, TahvilFroshNumCol].Text);
                        s.Tedad = short.Parse(WorkSheet.Cells[row, TedadCol].Text != "" ? WorkSheet.Cells[row, TedadCol].Text : "0");
                        s.Driver = service.GetDriver(WorkSheet.Cells[row, RanandeCol].Text);
                        order.OrderDetails.Add(s);
                    }

                    items.Add(order); 
                }
            }

            return items;
        }

        
    }
}

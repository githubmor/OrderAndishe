using OrdersAndisheh.DBL;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.ExcelManager
{
    public class ExcelService 
    {

        ExcelPackage package;
        DbService service;
        public ExcelService(string path,DbService dbservice)
        {
            this.path = path;
            FileInfo f = new FileInfo(path);
            package = new ExcelPackage(f);
            service = dbservice;
        }

        private string path = "";

        public string FilePath
        {
            get { return path; }
            set { path = value; }
        }

        public bool HasNewCustomer { get; set; }

        public bool HasNewBazres { get; set; }

        public bool HasNewPallet { get; set; }

        public bool HasNewDriver { get; set; }
        public bool HasNewProduct { get; set; }
        public bool HasNewDays { get; set; }

        private List<Bazres> newBazres = new List<Bazres>();
        private List<Pallet> newPallet = new List<Pallet>();
        private List<Driver> newDriver = new List<Driver>();
        private List<Customer> newCustomer = new List<Customer>();
        private List<Product> newProduct = new List<Product>();
        private List<Order> newSefaresh = new List<Order>();
        

        public string CheckingData()
        {
            try
            {
                string cg = this.CheckProduct();
                string cc = this.CheckCustomer();
                string cd = this.CheckDriver();
                string cp = this.CheckPallet();
                string cb = this.CheckBazres();
                string cdy = this.CheckDays();
                if (!string.IsNullOrEmpty(cg))
                {
                    HasNewProduct = true;
                }
                if (!string.IsNullOrEmpty(cc))
                {
                    HasNewCustomer = true;
                }
                if (!string.IsNullOrEmpty(cd))
                {
                    HasNewDriver = true;
                }
                if (!string.IsNullOrEmpty(cb))
                {
                    HasNewBazres = true;
                }
                if (!string.IsNullOrEmpty(cp))
                {
                    HasNewPallet = true;
                }
                if (!string.IsNullOrEmpty(cdy))
                {
                    HasNewDays = true;
                }

                return cg + "\n" + cc + "\n" + cd + "\n" + cb + "\n" + cp + "\n" + cdy;
            }
            catch (Exception)
            {
                
                throw ;
            }
        }

        private string CheckBazres()
        {
            string re = "";
            int index = 0;
            List<Bazres> bazres = new List<Bazres>();

            foreach (var item in package.Workbook.Worksheets)
            {
                if (item.Name == "محصولات")
                {
                    index = item.Index;
                }
            }
            if (index > 0)
            {
                ExcelWorksheet WorkSheet = package.Workbook.Worksheets[index];
                var start = WorkSheet.Dimension.Start;
                var end = WorkSheet.Dimension.End;

                int bazresColindex = 0; 

                for (int i = start.Column; i < end.Column; i++)
                {
                    if (WorkSheet.Cells[1,i].Text=="بازرس")
                    {
                        bazresColindex = i;
                    }
                }

                if (bazresColindex>0)
                {
                    for (int row = start.Row + 1; row <= end.Row; row++)
                    {
                        if (WorkSheet.Cells[row, bazresColindex].Text!="")
                            bazres.Add(new Bazres() { Name = WorkSheet.Cells[row, bazresColindex].Text});
                    }
                    bazres = bazres.GroupBy(a => a.Name).Select(y => y.First()).ToList();

                    List<Bazres> DbBazres = service.LoadAllBazres();

                    foreach (var b in bazres)
                    {
                        if (!DbBazres.Exists(pl => pl.Name == b.Name))
                        {
                            re += b.Name + "\n";
                            newBazres.Add(b);
                        }
                    }
                }
                else
                {
                    throw new ApplicationException("ستون بازرس در برگه پیدا نشد !");
                }

            }
            else
            {
                throw new ApplicationException("برگه اطلاعات محصولات - بازرس پیدا نشد !");
            }

            return re;

        }

        private string CheckPallet()
        {
            string re = "";
            int index = 0;
            List<Pallet> pallets = new List<Pallet>();

            foreach (var item in package.Workbook.Worksheets)
            {
                if (item.Name == "محصولات")
                {
                    index = item.Index;
                }
            }
            if (index > 0)
            {
                ExcelWorksheet WorkSheet = package.Workbook.Worksheets[index];
                var start = WorkSheet.Dimension.Start;
                var end = WorkSheet.Dimension.End;

                int Colindex = 0;

                for (int i = start.Column; i < end.Column; i++)
                {
                    if (WorkSheet.Cells[1, i].Text == "نوع پالت")
                    {
                        Colindex = i;
                    }
                }

                if (Colindex > 0)
                {
                    for (int row = start.Row + 1; row <= end.Row; row++)
                    {
                        if (WorkSheet.Cells[row, Colindex].Text != "" && WorkSheet.Cells[row, Colindex+1].Text!="")
                            pallets.Add(new Pallet() { Name = WorkSheet.Cells[row, Colindex].Text, Vazn = byte.Parse(WorkSheet.Cells[row, Colindex+1].Text)});
                    }

                    pallets = pallets.GroupBy(a => a.Name).Select(y => y.First()).ToList();

                    List<Pallet> DbData = service.LoadAllPallet();

                   
                    foreach (var b in pallets)
                    {
                        if (!DbData.Exists(pl=>pl.Name==b.Name))
                        {
                            re += b.Name + "\n";
                            newPallet.Add(b);
                        }
                    }
                }
                else
                {
                    throw new ApplicationException("ستون پالت در برگه پیدا نشد !");
                }

            }
            else
            {
                throw new ApplicationException("برگه اطلاعات محصولات - پالت پیدا نشد !");
            }

            return re;
        }

        private string CheckDriver()
        {
            string re = "";
            int index = 0;
            List<Driver> drivers = new List<Driver>();

            foreach (var item in package.Workbook.Worksheets)
            {
                if (item.Name == "راننده")
                {
                    index = item.Index;
                }
            }
            if (index > 0)
            {
                ExcelWorksheet WorkSheet = package.Workbook.Worksheets[index];
                var start = WorkSheet.Dimension.Start;
                var end = WorkSheet.Dimension.End;

                    for (int row = start.Row + 1; row <= end.Row; row++)
                    {
                        if (WorkSheet.Cells[row, 1].Text != "" )
                            drivers.Add(new Driver() { 
                                Name = WorkSheet.Cells[row, 1].Text,
                                Car = WorkSheet.Cells[row, 5].Text ,
                                Pelak = WorkSheet.Cells[row, 4].Text,
                                Tel1 = WorkSheet.Cells[row, 2].Text,
                                Tel2 = WorkSheet.Cells[row, 3].Text,
                            });
                    }


                    List<Driver> DbData = service.LoadAllDrivers();

                    foreach (var b in drivers)
                    {
                        if (!DbData.Exists(pl => pl.Name == b.Name))
                        {
                            re += b.Name + "\n";
                            newDriver.Add(b);
                        }
                    }
            }
            else
            {
                throw new ApplicationException("برگه راننده پیدا نشد !");
            }

            return re;
        }

        private string CheckCustomer()
        {
            string re = "";
            int index = 0;
            List<Customer> customer = new List<Customer>();

            foreach (var item in package.Workbook.Worksheets)
            {
                if (item.Name == "مقاصد")
                {
                    index = item.Index;
                }
            }
            if (index > 0)
            {
                ExcelWorksheet WorkSheet = package.Workbook.Worksheets[index];
                var start = WorkSheet.Dimension.Start;
                var end = WorkSheet.Dimension.End;

                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    if (WorkSheet.Cells[row, 1].Text != "")
                        customer.Add(new Customer()
                        {
                            Name = WorkSheet.Cells[row, 1].Text
                        });
                }


                List<Customer> DbData = service.LoadAllCustomers();

                foreach (var b in customer)
                {
                    if (!DbData.Exists(pl => pl.Name == b.Name))
                    {
                        re += b.Name + "\n";
                        newCustomer.Add(b);
                    }
                }
            }
            else
            {
                throw new ApplicationException("برگه مقاصد پیدا نشد !");
            }

            return re;
        }

        private string CheckProduct()
        {
            string re = "";
            int index = 0;
            List<Product> product = new List<Product>();

            foreach (var item in package.Workbook.Worksheets)
            {
                if (item.Name == "محصولات")
                {
                    index = item.Index;
                }
            }
            if (index > 0)
            {
                ExcelWorksheet WorkSheet = package.Workbook.Worksheets[index];
                var start = WorkSheet.Dimension.Start;
                var end = WorkSheet.Dimension.End;

                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    if (WorkSheet.Cells[row, 1].Text != "" & WorkSheet.Cells[row, 12].Text != "" & WorkSheet.Cells[row, 9].Text != "")
                    {
                        Product pp = new Product();
                        pp.Name = WorkSheet.Cells[row, 2].Text;
                        pp.Code = WorkSheet.Cells[row, 1].Text;
                        pp.CodeJense = WorkSheet.Cells[row, 8].Text;
                        pp.FaniCode = (WorkSheet.Cells[row, 7].
                            Text.Length > 15 ? WorkSheet.Cells[row, 7].
                            Text.Substring(0, 15) : WorkSheet.Cells[row, 7].Text);
                        pp.IsImenKala = (int.Parse(WorkSheet.Cells[row, 1].Text) > 15010000);
                        pp.Bazres_Id = GetBazres(WorkSheet.Cells[row, 12].Text);
                        pp.PalletId = GetPallet(WorkSheet.Cells[row, 9].Text);
                        pp.TedadDarPallet = (WorkSheet.Cells[row, 3].Text == "" ? 0 : int.Parse(WorkSheet.Cells[row, 3].Text));
                        pp.TedadDarSabad = (WorkSheet.Cells[row, 4].Text == "" ? 0 : int.Parse(WorkSheet.Cells[row, 4].Text));
                        pp.Weight = (WorkSheet.Cells[row, 6].Text == "" ? 0 : double.Parse(WorkSheet.Cells[row, 6].Text));
                        product.Add(pp);
                    }
                }


                List<Product> DbData = service.LoadAllProduct();

                foreach (var b in product)
                {
                    if (!DbData.Exists(pl => pl.Code == b.Code))
                    {
                        re += b.Name + "\n";
                        newProduct.Add(b);
                    }
                }
            }
            else
            {
                throw new ApplicationException("برگه محصولات پیدا نشد !");
            }

            return re;
        }

        private int GetPallet(string palletName)
        {

            int p;
            p = service.GetPalletByName(palletName);
            //if (p==null)
            //{
            //    p = new Pallet() { Name = palletName, Vazn = 0 };
            //}
            return p;
        }

        private int GetBazres(string BazresName)
        {
            int p;
            p = service.GetBazretByName(BazresName);
            //if (p == null)
            //{
            //    p = new Bazre() { Name = BazresName };
            //}
            return p;
        }
        private int GetCustomer(string CustomerName)
        {
            int p;
            p = service.GetCustomerByName(CustomerName);
            //if (p == null)
            //{
            //    p = new Bazre() { Name = BazresName };
            //}
            return p;
        }
        private int GetDriver(string DriverName)
        {
            int p;
            p = service.GetDriverByName(DriverName);
            //if (p == null)
            //{
            //    p = new Bazre() { Name = BazresName };
            //}
            return p;
        }

        private string CheckDays()
        {
            
            string re = "";
            //int[] index = new int[] { };
            //List<Order> orders = new List<Order>();

            //foreach (var item in package.Workbook.Worksheets)
            //{
            //    int day = int.Parse(item.Name);
            //    if (day > 0 & day < 31)
            //    {
            //        index[0] = item.Index;
            //    }
            //}
            //foreach (var item in index)
            //{
            //    //if (item > 0)
            //    //{
            //        ExcelWorksheet WorkSheet = package.Workbook.Worksheets[item];
            //        var start = WorkSheet.Dimension.Start;
            //        var end = WorkSheet.Dimension.End;

            //        List<OrderDetail> od = new List<OrderDetail>();
            //        for (int row = start.Row + 1; row <= end.Row; row++)
            //        {
            //            if (WorkSheet.Cells[row, 9].Text=="")
            //            {
            //                throw new ApplicationException("در برگه " + item + " نام مقصد در ردیف" + row + " وجود ندارد");
            //            }
            //            if (WorkSheet.Cells[row, 1].Text == "")
            //            {
            //                throw new ApplicationException("در برگه " + item + "  کد کالا در ردیف" + row + " وجود ندارد");
            //            }
            //            if (WorkSheet.Cells[row, 2].Text == "")
            //            {
            //                throw new ApplicationException("در برگه " + item + "  تعداد ارسالی در ردیف" + row + " وجود ندارد");
            //            }
            //            if (WorkSheet.Cells[row, 10].Text == "")
            //            {
            //                throw new ApplicationException("در برگه " + item + " نام راننده در ردیف" + row + " وجود ندارد");
            //            }
            //            if (WorkSheet.Cells[row, 11].Text == "")
            //            {
            //                throw new ApplicationException("در برگه " + item + " شماره تحویل در ردیف" + row + " وجود ندارد");
            //            }
            //            od.Add(new OrderDetail()
            //            {
            //                Customer_Id = this.GetCustomer(WorkSheet.Cells[row, 9].Text),
            //                Driver_Id = this.GetDriver(WorkSheet.Cells[row, 10].Text),
            //                HasOracle = (WorkSheet.Cells[row, 12].Text!=""?true:false),
            //                ProductId = int.Parse(WorkSheet.Cells[row, 1].Text),
            //                TahvilForosh = int.Parse(WorkSheet.Cells[row, 11].Text),
            //                Tedad = int.Parse(WorkSheet.Cells[row, 2].Text)
            //            });
            //        }

            //        orders.Add(new Order()
            //                {
            //                    Tarikh="1395/01/"+(item>9?item.ToString():"0"+item),//TODO  محاسبه ماه و دو رقمی روز
            //                    Accepted = true,
            //                    OrderDetails = od
            //                });



            //        List<Order> DbData = service.LoadAllSefareshs();

            //        foreach (var b in orders)
            //        {
            //            if (!DbData.Exists(pl => pl.Tarikh == b.Tarikh))
            //            {
            //                re += b.Tarikh + "\n";
            //                newSefaresh.Add(b);
            //            }
            //        }
            //    //}
            //    //else
            //    //{
            //    //    throw new ApplicationException("برگه راننده پیدا نشد !");
            //    //}
            //}

            return re;
        }

        public void AddNewProductToDataBase()
        {
            service.AddNewProductToDataBase(newProduct);
        }
        public void AddNewCustomerToDataBase()
        {
            service.AddNewCustomerToDataBase(newCustomer);
        }
        public void AddNewDriverToDataBase()
        {
            service.AddNewDriverToDataBase(newDriver);
        }
        public void AddNewPalletToDataBase()
        {
            service.AddNewPalletToDataBase(newPallet);
        }
        public void AddNewBazresToDataBase()
        {
            service.AddNewBazresToDataBase(newBazres);
        }

        public void AddDaysToDataBase()
        {
            service.AddNewDaysToDataBase(newSefaresh);
        }
    }
}

﻿namespace BL
{
	using System;
    using System.Data.Entity;
	using System.Collections.Generic;
	using System.Linq;
    using OrdersAndisheh.DBL;
    using OrdersAndisheh.BL;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using OrdersAndisheh.ViewModel;
    using OrdersAndisheh.BL.Importing;
    using System.Threading.Tasks;


    /// <summary>
    /// This is Just For Service Sefaresh To Database
    /// </summary>
	public class SefareshService : ISefareshService 
	{
        MyContextCF db;
        public SefareshService()
        {
            db = new MyContextCF();
            
        }
		public void SaveSefaresh(Sefaresh sefaresh)
		{
            try
            {
                if (sefaresh.Order != null)
                {
                    db.Orders.Add(sefaresh.Order);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
		}

        public string GetDrivePriority(string tarikh)
        {
            String re = "";

            PersianDateTime emroz = PersianDateTime.Parse(tarikh);
            String inMah = (emroz.Month > 10 ? emroz.Month.ToString() : "0" + emroz.Month.ToString());

            PersianDateTime yekmahghabl = emroz.AddDays(-30);
            int year = yekmahghabl.Year;
            String mahGhabl = (yekmahghabl.Month>10?yekmahghabl.Month.ToString():"0"+yekmahghabl.Month.ToString());
            int roz = yekmahghabl.DaysInMonth;

            //var ssr = db.OrderDetails.OrderBy(p=>p.Id).Where(
            //            p => p.Order.Tarikh.StartsWith(year.ToString())
            //            & (p.Order.Tarikh.Substring(5, 2) == mahGhabl | p.Order.Tarikh.Substring(5, 2) == inMah))
            //            .GroupBy(p => p.Driver).Select(g => new
            //            {
            //                dname = g.Key.Name,
            //                dcar = g.Key.Car,
            //                tahala = g.Key.OrderDetails.GroupBy(p => p.Order).Count(),
            //                tahalad = g.Key.OrderDetails.GroupBy(p => p.Order).Select(p=>p.Key.Tarikh)
            //            }).ToList();

            var ss = db.OrderDetails.OrderBy(p=>p.Id).GroupBy(p => p.Driver)
                .Select(g => new
                {
                    dname = g.Key.Name,
                    dcar = g.Key.Car,
                    tahala = g.Key.OrderDetails.Where(
                        p => p.Order.Tarikh.StartsWith(year.ToString())
                        & (p.Order.Tarikh.Substring(5, 2) == mahGhabl | p.Order.Tarikh.Substring(5, 2) == inMah))
                        .GroupBy(p => p.Order).Count(),
                        tarikhha = g.Key.OrderDetails.GroupBy(p => p.Order).OrderBy(p=>p.Key.Tarikh).Select(p=>p.Key.Tarikh)
                }).ToList();

            var nisanha = ss.Where(p => p.tahala > 0 & p.dcar == "نیسان").OrderByDescending(p => p.tarikhha.Last()).Take(6);
            var khavarha = ss.Where(p => p.tahala > 0 & p.dcar == "خاور").OrderByDescending(p => p.tarikhha.Last()).Take(6);

            re = "نیسان :";
            foreach (var item in nisanha)
            {
               re += item.dname + " = " + (item.tahala) + " / ";
            }

            re += ".... خاور :";
            foreach (var item in khavarha)
            {
                re += item.dname + " = " + (item.tahala) + " / ";
            }

            return re;
        }
        

        public void UpdateSefaresh(Sefaresh sefaresh)
		{
            
            List<OrderDetail> existingItems = db.OrderDetails.
            Where(p => p.OrderId == sefaresh.SefareshId).ToList();


            List<OrderDetail> newItems = new List<OrderDetail>();
            foreach (var item in sefaresh.Items)
            {
                newItems.Add(item.OrderDetail);
            }


            List<OrderDetail> deletedItems = existingItems.Except(newItems).ToList();

            foreach (var item in deletedItems)
            {
                db.OrderDetails.Remove(item);
            }
            

            db.SaveChanges();
            
		}

        public virtual void AcceptSefaresh(string tarikhsefaresh)
		{
            var o = db.Orders.Where(u => u.Tarikh == tarikhsefaresh).FirstOrDefault();
            o.Accepted = true;
            db.SaveChanges();
		}

        public virtual void UnAcceptSefaresh(Sefaresh sefaresh)
		{
            sefaresh.Accepted = false;
            db.SaveChanges();
		}

       

        public Sefaresh LoadSefaresh(string tarikh)
        {
            if (string.IsNullOrEmpty(tarikh))
            {
                throw new ApplicationException("تاریخ به صورت صحیح وارد نشده");
            }
            //using (MyContextCF db = new MyContextCF())
            //{
                
                Order t = db.Orders.Where(p => p.Tarikh == tarikh)
                            .FirstOrDefault();
                List<OrderDetail> to = db.OrderDetails.Where(p => p.OrderId == t.Id)
                    .Include("Customer")
                    .Include("Driver")
                    .Include("Driver.TempDriver")
                    .Include("Product")
                    .Include("Product.Pallet")
                    .Include("Product.Bazre")
                    .Include("Product.Amount")
                    .Include("MOracle")
                    .OrderBy(o => o.Driver_Id)
                    .ThenBy(o => o.Customer_Id)
                    .ThenBy(i => i.ItemType)
                    .ThenBy(s=>s.Product.Code)
                    .ToList();

                //t.OrderDetails = to;

                if (t==null)
                {
                    throw new ApplicationException("اطلاعات سفارش وجود ندارد");
                }
                return new Sefaresh(t, t.OrderDetails.ToList());

            //}
        }

        public List<Driver> LoadDrivers()
        {
            //using (MyContextCF db = new MyContextCF())
            //{
            return db.Drivers.Include("TempDriver").OrderBy(j => j.Name).ToList();
            //}
        }

        public List<Product> LoadGoods()
        {
            var pp = db.Products.Include("Pallet").Include("Bazre").ToList();
                return pp;
        }

        public List<Customer> LoadDestinations()
        {
            return db.Customers.OrderBy(o => o.Name).ToList();
        }

        public List<string> LoadAllNOAcceptedSefareshTarikh()
        {
            return db.Orders.Where(p=>!p.Accepted).Select(p=>p.Tarikh).ToList();
        }

        public List<Customer> LoadOracleCustomer()
        {
            var b = db.Customers.Include("OracleProducts").ToList();
            return b.Where(p => p.Relations!=null).ToList();
        }

        public void SaveOracleRelation(List<Customer> oCustomers)
        {
            var b = db.Customers.Include("OracleProducts").ToList();
            List<Customer> existingItems = b.Where(p => p.Relations!=null).ToList();

            List<Customer> newItems = new List<Customer>();
            foreach (var item in oCustomers)
            {
                newItems.Add(item);
            }

            List<Customer> deletedItems = existingItems.Except(newItems).ToList();

            foreach (var item in deletedItems)
            {
                db.Customers.Remove(item);
            }

            db.SaveChanges();
        }

        public List<ItemSefaresh> LoadNoDriverSefareshItems(string sefareshTarikh)
        {
            List<ItemSefaresh> mn = new List<ItemSefaresh>();
            var m = db.OrderDetails
                .Where(p => p.Order.Tarikh == sefareshTarikh)
                .Include("Customer")
                .Include("Product")
                .Include("Driver")
                .Include("Driver.TempDriver")
                .Include("Product.Pallet")
                .ToList();
            foreach (var item in m)
            {
                if (item.Driver==null || item.Driver.TempDriver!=null)
                {
                    mn.Add(new ItemSefaresh(item));
                }
            }

            return mn;
        }

        public void AddDriver(Driver p)
        {
            db.Drivers.Add(p);
            //db.SaveChanges();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void DelNoUsedTempDrivers(List<Driver> TempDriverForDelete)
        {
            foreach (var item in TempDriverForDelete)
            {
                db.Drivers.Remove(item);
            }
            db.SaveChanges();
        }

        public void DeleteSefaresh(string SelectedTarikh)
        {
            var or = db.Orders.Where(p => p.Tarikh == SelectedTarikh).FirstOrDefault();
            if (or!=null)
            {
                db.Orders.Remove(or);
                db.SaveChanges();
            }
        }

        public List<CheckSefaresh> LoadCheckSefareshs(int year)
        {
            db.Database.Log = Console.Write;
            List<CheckSefaresh> sd = new List<CheckSefaresh>();
            var u = from p in db.Orders
                    where !p.Accepted
                    where p.Tarikh.StartsWith(year.ToString())
                    orderby p.Tarikh descending
                    select new { 
                        tarikh = p.Tarikh, 
                        hasAnyZeroTedad = p.OrderDetails.Any(t => t.Tedad == 0),
                        hasAnyEmptyMaghsad = p.OrderDetails.Any(t => t.Customer == null),
                        hasAnyEmptyRanande = p.OrderDetails.Any(t => t.Driver == null),
                        hasAnyTempRanande = p.OrderDetails.Any(t => t.Driver.TempDriver!=null),
                        hasAnyZeroTahvilFrosh = p.OrderDetails.Any(t => t.TahvilForosh == 0 | 
                            t.TahvilForosh==9999),
                        hasAnyEmptyOracle = p.OrderDetails.Where(y=>y.Customer!=null)
                        .Where(e => e.Product.OracleRelations.Any(f => f.Customer == e.Customer))
                            .Any(r => r.MOracle == null)
                    };
            var sad = u.ToList();

            foreach (var item in sad)
            {
                sd.Add(new CheckSefaresh(
                    item.tarikh,
                    item.hasAnyZeroTedad,
                    item.hasAnyEmptyMaghsad,
                    item.hasAnyEmptyRanande,
                    item.hasAnyTempRanande,
                    item.hasAnyZeroTahvilFrosh,
                    item.hasAnyEmptyOracle
                    ));
            }
            return sd;

        }

        public void SaveDriverWorks(DriverWork SelectedDriverWork)
        {
            db.DriverWork.Add(SelectedDriverWork);
            db.SaveChanges();
        }

        public List<Driver> LoadDriversForThisSefaresh(Order order)
        {
            List<Driver> dr = new List<Driver>();
            var t = db.Orders.Where(p => p.Id == order.Id).Include("OrderDetails.Driver").FirstOrDefault();

            foreach (var item in t.OrderDetails)
            {
                dr.Add(item.Driver);
            }

            return dr.Distinct().ToList();
        }

        public Order LoadThisSefareshWithAllDriverWork(string sefareshtarikh)
        {
            return db.Orders
                .Where(p => p.Tarikh == sefareshtarikh)
                .Include("DriverWorks")
                .Include("DriverWorks.Driver")
                .FirstOrDefault();
        }

        public List<DriverWork> LoadDriverWorksForThisSefaresh(int orderId)
        {
            return db.DriverWork.Where(p => p.OrderId == orderId).Include("Driver").ToList();
        }

        public List<ErsalItem> LoadAllSefaresh(int year)
        {
            List<ErsalItem> ret = new List<ErsalItem>();
            var p = db.Orders.Where(r=>r.Tarikh.StartsWith(year.ToString()))
                .Include("OrderDetails.Customer")
                .Include("OrderDetails.Driver")
                .Include("OrderDetails.Product")
                .Include("OrderDetails.Product.Bazre")
                .Include("OrderDetails.Product.Pallet")
                .Include("OrderDetails")
                .ToList();
            foreach (var item in p)
            {
                foreach (var rr in item.OrderDetails)
                {
                    ret.Add(new ErsalItem(rr, item.Tarikh));
                }
                //ret.Add(new ErsalItem(item, item.OrderDetails.ToList()));
            }
            return ret;
        }

        public void AddNewAmount(List<AmountData> Amounts)
        {
            List<Amount> addamount = new List<Amount>();
            //var pr = db.Amount.ToList();
            //db.Amount.RemoveRange(pr);
            //db.SaveChanges();

            foreach (var item in Amounts)
            {
                var kala = db.Products.Include("Amount").Where(p => p.Code == item.CodeKala).FirstOrDefault();
                if (kala!=null)
                {
                    if (kala.Amount!=null)
                    {
                        kala.Amount.LastAmount = item.LastAmount;
                    }
                    else
                    {
                        addamount.Add(new Amount() { Product = kala, LastAmount = item.LastAmount }); 
                    }
                    
                }
            }
            db.Amount.AddRange(addamount);
            db.SaveChanges();
            
        }

        public string GetAnbarNumber(Product product, Customer customer)
        {
            return db.CustomerProductRelations
                .Where(p => p.CustomerId == customer.Id)
                .Where(p => p.ProductId == product.Id)
                .FirstOrDefault().Anbar;
        }

        public string MontagReciving(int sefareshId)
        {
            string re = "";
            var items = db.OrderDetails
                .Where(p => p.OrderId == sefareshId)
                .Include("Product")
                .Include("Product.Amount")
                .ToList();

            var goodsInItems = items.Select(p => p.Product).Distinct().ToList();
            foreach (var good in goodsInItems)
            {
                if (good.Amount!=null)
                {
                    var y = good.Amount.LastAmount -
                                items.Where(p => p.ProductId == good.Id).Select(p => p.Tedad).Sum(x => x);
                    if (y<0)
                    {
                        re += good.Name + "-" + y + "\n";
                    }

                }
            }
            return re;
        }

        public Product GetProduct(string code)
        {
            var pr = db.Products.Where(p => p.Code == code).FirstOrDefault();
            if (pr == null)
            {
                throw new NullReferenceException("کالایی با کد " + code + " پیدا نشد");
            }
            else
            {
                return pr;
            }
        }

        public Customer GetCustomer(string name)
        {
            var c = db.Customers.Where(p => p.Name == name).FirstOrDefault();
            if (c == null)
            {
                throw new NullReferenceException("مشتری با نام " + name + " پیدا نشد");
            }
            else
            {
                return c;
            }
        }

        public Driver GetDriver(string name)
        {
            var d =  db.Drivers.Where(p => p.Name == name).FirstOrDefault();
            if (d == null)
            {
                throw new NullReferenceException("راننده با نام " + name + " پیدا نشد");
            }
            else
            {
                return d;
            }
        }

        public void SaveOrders(List<Order> yu)
        {
            db.Orders.AddRange(yu);
            db.SaveChanges();
        }

        public List<ItemSefaresh> LoadAllPelaskosefaresh()
        {
            List<ItemSefaresh> rets = new List<ItemSefaresh>();

            var allPelaskitems = db.OrderDetails
                .Where(p => p.ProductId == 79)
                .Where(p => p.Customer_Id == 9)
                .Include("Customer")
                .Include("Driver")
                .Include("Product")
                .Include("Product.Pallet")
                .Include("Product.Bazre")
                .Include("Order")
                .ToList();

            foreach (var we in allPelaskitems)
            {
                var t = db.OrderDetails
                    .Include("Customer")
                    .Include("Driver")
                    .Include("Product")
                    .Include("Product.Pallet")
                    .Include("Product.Bazre")
                    .Include("Order")
                    .Where(p => p.Driver_Id == we.Driver_Id)
                    .Where(p => p.Order.Tarikh == we.Order.Tarikh)
                    .ToList();
                foreach (var tr in t)
                {
                    rets.Add(new ItemSefaresh(tr));
                }
            }

            return rets;
        }

        public bool ChechHasSefaresh(string today)
        {
            return db.Orders.Any(p => p.Tarikh == today);
        }


        public bool HasOracle(Product product, Customer customer)
        {
            if (product==null | customer == null)
            {
                throw new ApplicationException("اطلاعات کافی فرستاده نشد");
            }
            return db.OracleRelation.Where(p => p.ProductId == product.Id).Where(o => o.CustomerId == customer.Id).Any();
        }


        public void SaveOracle(MOracle m)
        {
            db.MOracles.Add(m);
        }

        public void DatabaseChecking()
        {
            var t = db.Drivers.Include("OrderDetails").ToList();
            for (int i = 0; i < t.Count; i++)
			{
			    if (t[i].OrderDetails.Count==0)
                {
                    db.Drivers.Remove(t[i]);
                }
			}
            db.SaveChanges();
            //foreach (var item in t)
            //{
            //    if (item.OrderDetails==null)
            //    {
            //        item.
            //    }
            //}
            //var ds = db.OrderDetails.Include("Driver").Include("TempDriver").Select(p => p.Driver.TempDriver != null);
        }


        public void SaveOracle(List<ItemSefaresh> Oracles)
        {
            if (Oracles.Any(p=>p.OrderDetail==null))
            {
                throw new ApplicationException("اطلاعات سفارش وجود ندارد");
            }
            foreach (var item in Oracles.Where(p=>!string.IsNullOrEmpty(p.MNumber)))
            {
                db.MOracles.Add(new MOracle() { MNumber = item.MNumber, OrderDetail = item.OrderDetail });
                db.SaveChanges();
            }
        }

        public List<PalletItem> LoadAllPalletReport(int year)
        {
            var ret = new List<PalletItem>();
            
            var d = db.OrderDetails.Where(p=>p.Order.Tarikh.StartsWith(year.ToString())).GroupBy(p => new { p.Order.Tarikh,p.Product.PalletId,p.Product.IsImenKala })
                .Select(
                    g => new
                    {
                        Tarikh = g.Key.Tarikh,
                        Tedad = g.Sum(s => s.TedadPallet),
                        palletId = g.FirstOrDefault().Product.Pallet.Id,
                        palletName = g.FirstOrDefault().Product.Pallet.Name,
                        isImen = g.Key.IsImenKala
                    }).ToList();

            var erf = d.GroupBy(p =>p.Tarikh)
                .Select(j => new
                {
                    Tarikh = j.Key,
                    er = j.ToList()
                }).ToList();

            foreach (var item in erf)
            {
                bool isSaipaA = item.er.Where(p => !p.isImen).Any(p => p.palletName == "GP8");
                bool isSaipaI = item.er.Where(p => p.isImen).Any(p => p.palletName == "GP8");
                bool isSapcoA = item.er.Where(p => !p.isImen).Any(p => p.palletName == "RE8");
                bool isSapcoI = item.er.Where(p => p.isImen).Any(p => p.palletName == "RE8");
                ret.Add(new PalletItem() { 
                    Tarikh = item.Tarikh,
                    saipaA = (isSaipaA ? item.er.Where(o => !o.isImen).Where(p => p.palletName == "GP8").FirstOrDefault().Tedad : 0),
                    saipaI = (isSaipaI ? item.er.Where(o => o.isImen).Where(p => p.palletName == "GP8").FirstOrDefault().Tedad : 0),
                    sapcoA = (isSapcoA ? item.er.Where(o => !o.isImen).Where(p => p.palletName == "RE8").FirstOrDefault().Tedad : 0),
                    sapcoI = (isSapcoI ? item.er.Where(o => o.isImen).Where(p => p.palletName == "RE8").FirstOrDefault().Tedad : 0),
                });
            }

            return ret;
        }

        public List<string> GetYears()
        {
            var y = db.Orders.GroupBy(p => p.Tarikh.Substring(0, 4)).Select(p => p.Key).ToList();

            return y;
        }

        //public void change()
        //{
        //    var os = db.Orders.ToList();

        //    foreach (var item in os)
        //    {
        //        var t = item.Tarikh;
        //        var day = t.Substring(8, 2);
        //        var year = t.Substring(0, 4);
        //        var mah = t.Substring(5, 2);

        //        item.Day = Byte.Parse(day);
        //        item.Mah = Byte.Parse(mah);
        //        item.Year = int.Parse(year);
        //    }

        //    db.SaveChanges();
        //}
    }
}


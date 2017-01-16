﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BL
{
	using System;
    using System.Data.Entity;
	using System.Collections.Generic;
	using System.Linq;
    using OrdersAndisheh.DBL;
    using OrdersAndisheh.BL;
    using System.Collections.ObjectModel;


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
            if (sefaresh.Items == null)
            {
                throw new ApplicationException("");
            }
            sefaresh.Accepted = false;
            this.UpdateSefaresh(sefaresh);
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
            //using (MyContextCF db = new MyContextCF())
            //{
            var pp = db.Products.Include("Pallet").Include("Bazre").ToList();
                return pp;
            //}
        }

        public List<Customer> LoadDestinations()
        {
            //using (MyContextCF db = new MyContextCF())
            //{
            return db.Customers.OrderBy(o => o.Name).ToList();
            //}
        }

        public List<string> LoadAllNOAcceptedSefareshTarikh()
        {
            //using (MyContextCF db = new MyContextCF())
            //{
                return db.Orders.Where(p=>!p.Accepted).Select(p=>p.Tarikh).ToList();
            //}
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

            //List<OrderDetail> addedItems = newItems.Except(existingItems).ToList();

            List<Customer> deletedItems = existingItems.Except(newItems).ToList();

            //List<OrderDetail> modifiedItems = newItems.Except(addedItems).ToList();

            //    //db.Orders.Remove(db.Orders.Where(p => p.Id == sefaresh.SefareshId).FirstOrDefault());
            //    //db.SaveChanges();
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

        public List<CheckSefaresh> LoadCheckSefareshs()
        {
            List<CheckSefaresh> sd = new List<CheckSefaresh>();
            var p = db.Orders
                .Include("OrderDetails.Customer")
                .Include("OrderDetails.Driver")
                .Include("OrderDetails.Driver.TempDriver")
                .Include("OrderDetails.Product")
                .OrderByDescending(o => o.Id)
                //.Where(i=>!i.Accepted) فعلا تا فعال کردن قابلیت برگرداندن تثبیت شده ها 
                .ToList();
            foreach (var item in p)
            {
                sd.Add(new CheckSefaresh(new Sefaresh(item, item.OrderDetails.ToList())));
            }
            return sd;

        }

        //public List<DriverWork> LoadAllDriverWorkForThisSefaresh(Order order)
        //{
        //    return db.DriverWork.Where(p => p.Order.Id == order.Id)
        //        .Include("Order")
        //        .Include("Driver")
        //        .ToList();
        //}

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

        public List<ErsalItem> LoadAllSefaresh()
        {
            List<ErsalItem> ret = new List<ErsalItem>();
            var p = db.Orders
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

        public void AddNewAmount(ObservableCollection<AmountDto> Amounts)
        {
            List<Amount> addamount = new List<Amount>();
            var pr = db.Amount.ToList();
            db.Amount.RemoveRange(pr);
            db.SaveChanges();

            foreach (var item in Amounts)
            {
                var kala = db.Products.Where(p => p.Code == item.CodeKala).FirstOrDefault();
                if (kala!=null)
                {
                    addamount.Add(new Amount() { Product = kala, LastAmount = item.LastAmount }); 
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

            var goodsInItems = items.Select(p => p.Product).ToList();
            foreach (var good in goodsInItems)
            {
                if (good.Amount!=null)
                {
                    good.Weight =
                                good.Amount.LastAmount -
                                items.Where(p => p.ProductId == good.Id).Select(p => p.Tedad).Sum(x => x); 
                }
            }
            foreach (var s in goodsInItems)
            {
                if (s.Weight<0)
                {
                    re += s.Name + "-" + s.Weight + "\n";
                }
            }
            return re;
        }
    }
}


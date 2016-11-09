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
            if (sefaresh.Order != null)
            {
                //using (MyContextCF db = new MyContextCF())
                //{
                    //AttachToDataBase(sefaresh.Order.OrderDetails.ToList(), db);
                    db.Orders.Add(sefaresh.Order);
                    db.SaveChanges();
                //}
            }
		}

        //private void AttachToDataBase(List<OrderDetail> orderDetails, MyContextCF db)
        //{
        //    foreach (var item in orderDetails)
        //    {
        //        if (item.Customer != null)
        //        {
        //            db.Customers.Attach(item.Customer);
        //        }
        //        if (item.Driver != null)
        //        {
        //            db.Drivers.Attach(item.Driver);
        //        }
        //        db.Pallets.Attach(item.Product.Pallet);
        //        db.Bazress.Attach(item.Product.Bazre);
        //        db.Products.Attach(item.Product);
        //    }
        //}

        public void UpdateSefaresh(Sefaresh sefaresh)
		{
            //using (MyContextCF db = new MyContextCF())
            //{
                //الان آپدیت که می کنیم همه چی قاطی میشه
            List<OrderDetail> existingItems = db.OrderDetails.
            Where(p => p.OrderId == sefaresh.SefareshId).ToList();


            List<OrderDetail> newItems = new List<OrderDetail>();
            foreach (var item in sefaresh.Items)
            {
                newItems.Add(item.OrderDetail);
            }

            //List<OrderDetail> addedItems = newItems.Except(existingItems).ToList();

            List<OrderDetail> deletedItems = existingItems.Except(newItems).ToList();

            //List<OrderDetail> modifiedItems = newItems.Except(addedItems).ToList();

            //    //db.Orders.Remove(db.Orders.Where(p => p.Id == sefaresh.SefareshId).FirstOrDefault());
            //    //db.SaveChanges();
            foreach (var item in deletedItems)
            {
                db.OrderDetails.Remove(item);
            }
            //foreach (var item in addedItems)
            //{
            //    db.OrderDetails.Add(item);
            //}
            

            db.SaveChanges();
            //}
            
		}

        public virtual void AcceptSefaresh(Sefaresh sefaresh)
		{
            if (sefaresh.Items==null)
            {
                throw new ApplicationException("آیتم ها سفارش نهای میباشد");
            }
            foreach (var item in sefaresh.Items)
            {
                if (item.Maghsad==""|item.Ranande==""|item.Tedad>0)
                {
                    throw new ApplicationException("اطلاعات مناسب وارد نشده");
                }
            }
            sefaresh.Accepted = true;
            this.UpdateSefaresh(sefaresh);
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
                    .Include("Product")
                    .Include("Product.Pallet")
                    .Include("Product.Bazre")
                    .OrderBy(o => o.Customer_Id)
                    .OrderBy(o => o.Driver_Id)
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
                return db.Drivers.ToList();
            //}
        }

        public List<Product> LoadGoods()
        {
            //using (MyContextCF db = new MyContextCF())
            //{
                var pp = db.Products.Include("Pallet").ToList();
                return pp;
            //}
        }

        public List<Customer> LoadDestinations()
        {
            //using (MyContextCF db = new MyContextCF())
            //{
                return db.Customers.ToList();
            //}
        }

        public List<string> LoadAllSefareshTarikh()
        {
            //using (MyContextCF db = new MyContextCF())
            //{
                return db.Orders.Select(p=>p.Tarikh).ToList();
            //}
        }

        public List<Customer> LoadOracleCustomer()
        {
            //using (MyContextCF db = new MyContextCF())
            //{
            var b = db.Customers.Include("OracleProducts").ToList();
            return b.Where(p => p.OracleProducts.Count>0).ToList();
            //}
        }

        public void SaveOracleRelation(List<Customer> oCustomers)
        {
            var b = db.Customers.Include("OracleProducts").ToList();
            List<Customer> existingItems = b.Where(p => p.OracleProducts.Count > 0).ToList();


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
        //public void Dispose()
        //{
        //    db.Database.Connection.Close();
        //}
    }
}


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
	using System.Text;
    using OrdersAndisheh.DBL;
    using OrdersAndisheh.BL;
    using System.Collections.ObjectModel;


    /// <summary>
    /// This is Just For Service Sefaresh To Database
    /// </summary>
	public class SefareshService : ISefareshService 
	{
        //MyContextCF db;
        //public SefareshService(MyContextCF context)
        //{
        //    db = context;
        //}
		public void SaveSefaresh(Sefaresh sefaresh)
		{
            if (sefaresh.Order != null)
            {
                using (MyContextCF db = new MyContextCF())
                {
                    foreach (var item in sefaresh.Order.OrderDetails)
                    {
                        if (item.Customer != null)
                        {
                            db.Customers.Attach(item.Customer);
                        }
                        if (item.Driver != null)
                        {
                            db.Drivers.Attach(item.Driver);
                        }
                        db.Pallets.Attach(item.Product.Pallet);
                        db.Bazress.Attach(item.Product.Bazre);
                        db.Products.Attach(item.Product);
                        //db.OrderDetails.Add(item);
                    }
                    db.Orders.Add(sefaresh.Order);
                    db.SaveChanges();
                }
            }
		}

        public void UpdateSefaresh(Sefaresh sefaresh)
		{
            using (MyContextCF db = new MyContextCF())
            {
                db.Orders.Remove(db.Orders.Where(p => p.Id == sefaresh.SefareshId).FirstOrDefault());
                db.Orders.Add(sefaresh.Order);
                db.SaveChanges();
            }
            
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
            using (MyContextCF db = new MyContextCF())
            {
                
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

                t.OrderDetails = to;

                if (t==null)
                {
                    throw new ApplicationException("اطلاعات سفارش وجود ندارد");
                }
                return new Sefaresh(t, t.OrderDetails.ToList());

            }
        }

        public List<Driver> LoadDrivers()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Drivers.ToList();
            }
        }

        public List<Product> LoadGoods()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Products.Include("Pallet").ToList();
            }
        }

        public List<Customer> LoadDestinations()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Customers.ToList();
            }
        }
    }
}


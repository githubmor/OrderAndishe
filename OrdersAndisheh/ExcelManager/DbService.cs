using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.ExcelManager
{
    public class DbService
    {

        public List<Bazres> LoadAllBazres()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Bazress.ToList();
            }
        }

        public List<Pallet> LoadAllPallet()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Pallets.ToList();
            }
        }

        public List<Driver> LoadAllDrivers()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Drivers.ToList();
            }
        }

        public List<Customer> LoadAllCustomers()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Customers.ToList();
            }
        }

        public List<Product> LoadAllProduct()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Products.ToList();
            }
        }



        public void AddNewProductToDataBase(List<Product> newProduct)
        {
            using (MyContextCF db = new MyContextCF())
            {
                foreach (var item in newProduct)
                {
                    //if (item.Bazre.Id>0)
                    //{
                    //    db.Entry(item.Bazre).State = EntityState.Unchanged;
                    //}
                    //if (item.Pallet.Id>0)
                    //{
                    //    db.Entry(item.Pallet).State = EntityState.Unchanged;
                    //}
                    db.Products.Add(item);
                    //db.Entry(item).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
        }

        public void AddNewCustomerToDataBase(List<Customer> newCustomer)
        {
            using (MyContextCF db = new MyContextCF())
            {
                foreach (var item in newCustomer)
                {
                    db.Entry(item).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
        }

        public void AddNewDriverToDataBase(List<Driver> newDriver)
        {
            using (MyContextCF db = new MyContextCF())
            {
                foreach (var item in newDriver)
                {
                    db.Entry(item).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
        }

        public void AddNewPalletToDataBase(List<Pallet> newPallet)
        {
            using (MyContextCF db = new MyContextCF())
            {
                foreach (var item in newPallet)
                {
                    db.Entry(item).State = EntityState.Added;
                    db.SaveChanges(); 
                }
            }
        }

        public void AddNewBazresToDataBase(List<Bazres> newBazres)
        {
            using (MyContextCF db = new MyContextCF())
            {
                foreach (var item in newBazres)
                {
                    db.Entry(item).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
        }

        public int GetPalletByName(string palletName)
        {
            using (MyContextCF db = new MyContextCF())
            {
                Pallet b = db.Pallets.Where(p => p.Name == palletName).FirstOrDefault();
                if (b != null)
                {
                    return b.Id;
                }
                else
                {
                    return 0;
                }
                //return db.Pallets.Where(p => p.Name == palletName).FirstOrDefault().Id;
            }
        }

        public int GetBazretByName(string BazresName)
        {
            using (MyContextCF db = new MyContextCF())
            {
                Bazres b = db.Bazress.Where(p => p.Name == BazresName).FirstOrDefault();
                if (b!=null)
                {
                    return b.Id;
                }
                else
                {
                    return 0;
                }
                //return db.Bazres.Where(p => p.Name == BazresName).FirstOrDefault().Id;
            }
        }

        public void AddNewDaysToDataBase(List<Order> newSefaresh)
        {
            using (MyContextCF db = new MyContextCF())
            {
                foreach (var item in newSefaresh)
                {
                    db.Entry(item).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
        }

        public int GetCustomerByName(string CustomerName)
        {
            using (MyContextCF db = new MyContextCF())
            {
                Customer b = db.Customers.Where(p => p.Name == CustomerName).FirstOrDefault();
                if (b != null)
                {
                    return b.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int GetDriverByName(string DriverName)
        {
            using (MyContextCF db = new MyContextCF())
            {
                Driver b = db.Drivers.Where(p => p.Name == DriverName).FirstOrDefault();
                if (b != null)
                {
                    return b.Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        internal List<Order> LoadAllSefareshs()
        {
            using (MyContextCF db = new MyContextCF())
            {
                return db.Orders.ToList();
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using System.Linq;
using System.Collections.Generic;
using OrdersAndisheh.DBL;

namespace UnitTest
{
    [TestClass]
    public class SefareshTest
    {
       
        


        //چک شود سفارش جدید میسازیم و ایتم میدهیم آیتم های خواندنی درست هست یا نه
        [TestMethod]
        public void CreateNewSefaresh()
        {
            Product p = new Product() {Code="656",Name="hghg" };
            //باید یادمان باشد ایتم سفارش اینجا چک نمیشود - نحوه ذخیره سازی هم اینجا چک نمی شود
            // اینجا فقط ساخت یک سفارش چک میشود
            List<ItemSefaresh> f = new List<ItemSefaresh>()
            {
                new ItemSefaresh(p){ItemKind=ItemType.Fori.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.AgharAmadeShod.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.Govahi.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.Usual.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.AgharAmadeShod.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.Usual.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.Usual.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.AgharAmadeShod.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.Govahi.ToString()},
                new ItemSefaresh(p){ItemKind=ItemType.Govahi.ToString()},
            };
            Sefaresh s = new Sefaresh();
            s.Tarikh = "1395/01/02";

            Assert.IsNotNull(s.Items);
            Assert.IsNotNull(s.Order);
            Assert.IsNotNull(s.AgarShodItems);
            Assert.IsNotNull(s.ForiItems);
            Assert.IsNotNull(s.GovahiItems);
            Assert.IsNotNull(s.UsualItems);

            Assert.IsFalse(s.Order.Accepted);
            Assert.IsNotNull(0, s.Order.Tarikh);

            Assert.AreEqual(0, s.Items.Count);
            Assert.AreEqual(0, s.AgarShodItems.Count);
            Assert.AreEqual(0, s.ForiItems.Count);
            Assert.AreEqual(0, s.GovahiItems.Count);
            Assert.AreEqual(0, s.UsualItems.Count);

            s.Items = f;

            Assert.AreEqual(f.Count, s.Items.Count);
            //Assert.AreEqual(f.Count(po => po.ItemKind == ItemType.AgharAmadeShod.ToString()), s.AgarShodItems.Count);
            //Assert.AreEqual(f.Count(po => po.ItemKind == ItemType.Fori.ToString()), s.ForiItems.Count);
            //Assert.AreEqual(f.Count(po => po.ItemKind == ItemType.Usual.ToString()), s.UsualItems.Count);
            //Assert.AreEqual(f.Count(po => po.ItemKind == ItemType.Govahi.ToString()), s.GovahiItems.Count);
        }


        //چک شود اگر تاریخ فرمت درست نیاید ایرور میدهد
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void IfTarikhFormatIsNotValid()
        {
            Sefaresh ds = new Sefaresh();
            ds.Tarikh = "SAdad";
        }

        // چک شود سفارش از روی دیتابیس ساخته شد درست ساخته میشود یا نه - آیتم ها هم باید از دیتابیس باشد
        [TestMethod]
        public void CreateSefareshFromDataBase()
        {
            Order o = new Order() { Accepted = false, Id = 5, Tarikh = "1359/02/03" };

            List<OrderDetail> od = new List<OrderDetail>()
            {
                new OrderDetail(){Customer_Id=4,ItemType=ItemType.Usual.ToString(),Driver_Id=2,HasOracle=false,Id=6,Product=new Product(){Name="d",Code="d"},Tedad=85},
                new OrderDetail(){Customer_Id=8,ItemType=ItemType.Govahi.ToString(),Driver_Id=3,HasOracle=true,Id=2,Product=new Product(){Name="d",Code="d"},Tedad=6},
                new OrderDetail(){Customer_Id=7,ItemType=ItemType.Usual.ToString(),Driver_Id=2,HasOracle=true,Id=1,Product=new Product(){Name="d",Code="d"},Tedad=5},
                new OrderDetail(){Customer_Id=2,ItemType=ItemType.Fori.ToString(),Driver_Id=5,HasOracle=false,Id=3,Product=new Product(){Name="d",Code="d"},Tedad=78},
                new OrderDetail(){Customer_Id=1,ItemType=ItemType.Usual.ToString(),Driver_Id=2,HasOracle=true,Id=4,Product=new Product(){Name="d",Code="d"},Tedad=98},
            };

            Sefaresh sef = new Sefaresh(o,od);

            Assert.AreEqual(o.Tarikh, sef.Tarikh);
            Assert.AreEqual(o.Accepted, sef.Accepted);
            Assert.AreEqual(o.Description, sef.Description);

            foreach (var item in this.Zip(sef.Items, od, (x, y) => new {sefareshOd = x, Od = y}))
            {
                Assert.AreEqual(item.Od, item.sefareshOd.OrderDetail);
            }

            //Assert.AreEqual(3, sef.ForiItems.Count);
            //Assert.AreEqual(1, sef.GovahiItems.Count);
            //Assert.AreEqual(1, sef.AgarShodItems.Count);
        }

        [ExpectedException(typeof(ApplicationException))]
        //اگر سفارش نال باشد
        [TestMethod]
        public void IfOrderISNull()
        {

            Sefaresh sef = new Sefaresh(null,null);
          }
        //اگر آیتم سفارش نال باشد
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void IfOrderDetailISNull()
        {
            Order o = new Order() { Accepted = false, Id = 5, Tarikh = "1359/02/03" };
            Sefaresh sef = new Sefaresh(o, null);
        }


        public IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(List<TFirst> first, List<TSecond> second, Func<TFirst, TSecond, TResult> selector)
        {
            if (first.Count != second.Count)
                throw new Exception();

            for (var i = 0; i < first.Count; i++)
            {
                yield return selector.Invoke(first[i], second[i]);
            }
        }
       
    }

}

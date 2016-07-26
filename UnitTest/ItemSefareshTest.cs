using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using OrdersAndisheh.DBL;

namespace UnitTest
{
    [TestClass]
    public class ItemSefareshTest
    {
        [TestMethod]
        public void CreatNewItem_AssignIt()
        {
            Customer c = new Customer() { Id = 1, Name = "asd" };
            Driver d = new Driver() { Car = "dd", Id = 1, Nam = "sd", Pelak = "SDsds", Tol = 6, Ton = 4 };
            Product p = new Product() { Bazres_Id = 5, Code = "655", Id = 5, Nam = "sd", PalletId = 45, TedadDarPallet = 3, TedadDarSabad = 5 };

            ItemSefaresh i = new ItemSefaresh(p);

            Assert.IsNull(i.OrderDetail.Customer);
            Assert.IsNull(i.OrderDetail.Driver);
            Assert.IsNotNull(i.OrderDetail.Product);
            Assert.IsNotNull(i.OrderDetail);
            Assert.AreEqual("", i.Maghsad);
            Assert.AreEqual("", i.Ranande);
            Assert.AreEqual(p.Code, i.CodeKala);
            Assert.AreEqual(p.Nam, i.Kala);
            Assert.IsTrue(i.HasOracle);
            Assert.AreEqual(ItemType.Usual, i.ItemKind);
            Assert.AreEqual(0,i.TahvilFrosh);
            Assert.AreEqual(0,i.Tedad);

            
            i.Customer = c;
            i.Driver = d;
            i.HasOracle = false;
            i.ItemKind = ItemType.Govahi;
            i.TahvilFrosh = 654;
            i.Tedad = 6500;

            Assert.AreEqual(c.Name, i.Maghsad);
            Assert.AreEqual(d.Nam, i.Ranande);
            Assert.AreEqual(p.Code, i.CodeKala);
            Assert.AreEqual(p.Nam, i.Kala);
            Assert.IsNotNull(i.OrderDetail);
            Assert.AreEqual(c, i.OrderDetail.Customer);
            Assert.AreEqual(d, i.OrderDetail.Driver);
            Assert.AreEqual(p, i.OrderDetail.Product);
            Assert.IsFalse(i.OrderDetail.HasOracle);
            Assert.AreEqual((int) ItemType.Govahi, i.OrderDetail.ItemType);
            Assert.AreEqual(654, i.OrderDetail.TahvilForosh);
            Assert.AreEqual(6500, i.OrderDetail.Tedad);
            Assert.IsFalse(i.HasOracle);
            Assert.AreEqual(ItemType.Govahi, i.ItemKind);
            Assert.AreEqual(654,i.TahvilFrosh);
            Assert.AreEqual(6500,i.Tedad);
            
        }

        //چک شود از روی دیتابیس ساخته شد همه چی درسته
        [TestMethod]
        public void CreateFromDatabase()
        {

            Customer c = new Customer() { Id = 1, Name = "asd" };
            Driver d = new Driver() { Car = "dd", Id = 1, Nam = "sd", Pelak = "SDsds", Tol = 6, Ton = 4 };
            Product p = new Product() { Bazres_Id = 5, Code = "655", Id = 5, Nam = "sd", PalletId = 45, TedadDarPallet = 3, TedadDarSabad = 5 };


            OrderDetail o = new OrderDetail();
            o.Customer = c;
            o.Driver = d;
            o.Product = p;
            o.HasOracle = true;
            o.ItemType = (int) ItemType.Usual;
            o.TahvilForosh = 56;
            o.Tedad = 48522;

            ItemSefaresh i = new ItemSefaresh(o);

            Assert.IsNotNull(i.OrderDetail.Customer);
            Assert.IsNotNull(i.OrderDetail.Driver);
            Assert.IsNotNull(i.OrderDetail.Product);
            Assert.IsNotNull(i.OrderDetail);
            Assert.AreEqual(c.Name, i.Maghsad);
            Assert.AreEqual(d.Nam, i.Ranande);
            Assert.AreEqual(p.Code, i.CodeKala);
            Assert.AreEqual(p.Nam, i.Kala);
            Assert.IsTrue(i.HasOracle);
            Assert.AreEqual(ItemType.Usual, i.ItemKind);
            Assert.AreEqual(o.TahvilForosh,i.TahvilFrosh);
            Assert.AreEqual(o.Tedad,i.Tedad);

        }

        //چک شود اگر کالا نال بود ایرور دهد
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void IfProductIsNull()
        {
            Product p = null;
            ItemSefaresh i = new ItemSefaresh(p);
        }

        //اگر کالای خالی فرستاده شود
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void IfProductIsEmpty()
        {
            Product p = new Product();
            ItemSefaresh i = new ItemSefaresh(p);
        }

        //اگر آیتم از دیتابیس خالی آمد
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void IfItemIsEmpty()
        {
            OrderDetail o = new OrderDetail();
            ItemSefaresh i = new ItemSefaresh(o);

        }
    }
}

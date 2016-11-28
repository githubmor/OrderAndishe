using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersAndisheh.ViewModel;
using OrdersAndisheh.DBL;
using System.Collections.ObjectModel;
using BL;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var p = new ObservableCollection<ItemSefaresh>();
            for (int i = 0; i < 10; i++)
            {
                p.Add(new ItemSefaresh(
                    new Product() { Name = "g" + i ,
                                    Pallet = new Pallet() { Vazn = (i % 2 == 0 ? (byte)20 : (byte)200) }
                    })
                {
                    Tedad = (short)(10 * i),
                      Customer = new Customer() { Name = (i % 2 == 0 ? "c" : "b") }
                    });
                vazn += p[i].Vazn;
            }


            DriverContainerViewModel v = new DriverContainerViewModel(null,p,1);

            Assert.AreEqual(10, v.Mahmole.Count);
            Assert.AreEqual(5, v.ChobiPalletCount);
            Assert.AreEqual(5, v.FeleziPalletCount);
            Assert.AreEqual("8", v.JaigahCount);
            Assert.AreEqual(vazn, v.VaznKol);

            v.Mahmole.Add(new ItemSefaresh(
                    new Product()
                    {
                        Name = "g",
                        Pallet = new Pallet() { Vazn = (byte) 200 }
                    })
            {
                Tedad = 10,
                Customer = new Customer() { Name = "c"  }
            });

            Assert.AreEqual(11, v.Mahmole.Count);
            Assert.AreEqual(5, v.ChobiPalletCount);
            Assert.AreEqual(6, v.FeleziPalletCount);
            Assert.AreEqual("8", v.JaigahCount);
            Assert.AreEqual(vazn+2000, v.VaznKol);
        }

        public int vazn { get; set; }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersAndisheh.ViewModel;
using Moq;
using BL;
using System.Collections.Generic;
using OrdersAndisheh.DBL;

namespace UnitTest
{
    [TestClass]
    public class MainViewModelTest
    {
        MainViewModel vm;
        List<Driver> drs;
        List<Product> pds;
        List<Customer> cts;

        [TestInitialize]
        public void IntializeTest()
        {
            drs = new List<Driver>(){
                new Driver(){Id=1,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=2,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=3,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=4,Name="ASD",Pelak="as",Car="Asd"}
            };

            pds = new List<Product>(){
                new Product(){Id=1,Name="ASD",Code="AS"},
                new Product(){Id=1,Name="ASD",Code="AS"},
                new Product(){Id=1,Name="ASD",Code="AS"},
                new Product(){Id=1,Name="ASD",Code="AS"}
            };

            cts = new List<Customer>(){
                new Customer(){Id=1,Name="ASD"},
                new Customer(){Id=1,Name="ASD"},
                new Customer(){Id=1,Name="ASD"},
                new Customer(){Id=1,Name="ASD"}
            };

            Mock<ISefareshService> ss = new Mock<ISefareshService>();

            ss.Setup(p => p.LoadDrivers()).Returns(drs);
            ss.Setup(p => p.LoadGoods()).Returns(pds);
            ss.Setup(p => p.LoadDestinations()).Returns(cts);


            vm = new MainViewModel(ss.Object);
        }


        [TestMethod]
        public void CreateNewInstanceIsOk()
        {
            Assert.AreEqual(4,vm.Destinations.Count);
            Assert.AreEqual(4, vm.Drivers.Count);
            Assert.AreEqual(4, vm.Goods.Count);

            Assert.IsNotNull(vm.sefaresh);//چون اینجا الان داریم یه سفارش میسازیم دیگه - نال نیست ولی تهی هست
            Assert.IsNotNull(vm.TypeList);
            Assert.IsNotNull(vm.Items);

            Assert.IsNull(vm.SelectedDestenation);
            Assert.IsNull(vm.SelecteddItem);
            Assert.IsNull(vm.SelectedDriver);
            Assert.IsNull(vm.SelectedProduct);
            Assert.IsNull(vm.Tarikh);

            Assert.AreEqual(0,vm.Tedad);

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsFalse(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));

        }

        //انتخاب یک کالا چه بر سر فرم میاورد
        [TestMethod]
        public void IfSelectProductIsOk()
        {
            vm.SelectedProduct = pds[1];

            Assert.IsNotNull(vm.SelecteddItem);//یک آیتم برای ذخیره شدن ساخته شد دیگه
            Assert.AreEqual(pds[1], vm.SelecteddItem.Product);

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsFalse(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));
        }

        //انتخاب تعداد چه بر سر فرم میاورد
        [ExpectedException(typeof(ApplicationException))]
        [TestMethod]
        public void IfNOTSelectProductButTedadSetThrowException()
        {
            vm.Tedad = 10;

        }


        //انتخاب یک مقصد چه بر سر فرم میاورد
        [TestMethod]
        public void IfSelectDestenation()
        {
            vm.SelectedDestenation = cts[2];
kjhkjhj

        }

        //انتخاب یک راننده چه بر سر فرم میاورد

        //بعد از افزودن آیتم چه میشود

        // بعد از افزودن راننده مقصد چه میشود

        //بعد از ذخیره سازی سفارش چه میشود

        // یک آیتم انتخاب شد چه میشود

        //اگر یک آیتم را انتخاب کردیم و همه بالایی ها را انجام دهیم چه اتفاقی می افتد

        //یک آیتم تیک خورد همه بالا انجام شد چه میشود  

        //چند آیتم تیک خورد همه بالا چه میشود

    }
}

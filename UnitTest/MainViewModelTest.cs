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
        [TestMethod]
        public void CreateNewInstanceIsOk()
        {
            List<Driver> drs = new List<Driver>(){
                new Driver(){Id=1,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=2,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=3,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=4,Name="ASD",Pelak="as",Car="Asd"}
            };

            List<Product> pds = new List<Product>(){
                new Product(){Id=1,Name="ASD",Code="AS"},
                new Product(){Id=1,Name="ASD",Code="AS"},
                new Product(){Id=1,Name="ASD",Code="AS"},
                new Product(){Id=1,Name="ASD",Code="AS"}
            };

            List<Customer> cts = new List<Customer>(){
                new Customer(){Id=1,Name="ASD"},
                new Customer(){Id=1,Name="ASD"},
                new Customer(){Id=1,Name="ASD"},
                new Customer(){Id=1,Name="ASD"}
            };
            Mock<ISefareshService> ss = new Mock<ISefareshService>();

            ss.Setup(p => p.LoadDrivers()).Returns(drs);
            ss.Setup(p => p.LoadGoods()).Returns(pds);
            ss.Setup(p => p.LoadDestinations()).Returns(cts);


            MainViewModel vm = new MainViewModel(ss.Object);

            Assert.IsNotNull(vm.Destinations);
            Assert.IsNotNull(vm.Drivers);
            Assert.IsNotNull(vm.Goods);
            Assert.IsNull(vm.Items);
        }
    }
}

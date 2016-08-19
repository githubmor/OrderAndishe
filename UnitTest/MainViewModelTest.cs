using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersAndisheh.ViewModel;
using Moq;
using BL;
using System.Collections.Generic;
using OrdersAndisheh.DBL;
using System.Collections.ObjectModel;

namespace UnitTest
{
    [TestClass]
    public class MainViewModelTest
    {
        MainViewModel vm;
        List<Driver> drs;
        List<Product> pds;
        List<Customer> cts;

        List<Order> InDBOrder;
        List<OrderDetail> InDBOrderDetails;

        [TestInitialize]
        public void IntializeTest()
        {
            InDBOrder = new List<Order>();
            InDBOrderDetails = new List<OrderDetail>();

            drs = new List<Driver>(){
                new Driver(){Id=1,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=2,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=3,Name="ASD",Pelak="as",Car="Asd"},
                new Driver(){Id=4,Name="ASD",Pelak="as",Car="Asd"}
            };

            pds = new List<Product>(){
                new Product(){Id=1,Name="ASD",Code="AS"},
                new Product(){Id=2,Name="ASD",Code="AS"},
                new Product(){Id=3,Name="ASD",Code="AS"},
                new Product(){Id=4,Name="ASD",Code="AS"}
            };

            cts = new List<Customer>(){
                new Customer(){Id=1,Name="ASD"},
                new Customer(){Id=2,Name="ASD"},
                new Customer(){Id=3,Name="ASD"},
                new Customer(){Id=4,Name="ASD"}
            };

            Mock<ISefareshService> ss = new Mock<ISefareshService>();

            ss.Setup(p => p.LoadDrivers()).Returns(drs);
            ss.Setup(p => p.LoadGoods()).Returns(pds);
            ss.Setup(p => p.LoadDestinations()).Returns(cts);
            ss.Setup(p => p.SaveSefaresh(It.IsAny<Sefaresh>())).Callback((Sefaresh pp) =>
            {
                if (pp.Order==null)
                {
                    throw new ApplicationException("Order Is Null !");
                }
                if (pp.Order.OrderDetails == null)
                {
                    throw new ApplicationException("OrderDetails Is Null !");
                }
                InDBOrder.Add(pp.Order);
                InDBOrderDetails.AddRange(pp.Order.OrderDetails);
            });


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

        //اول انتخاب کالا بعد بقیه اطلاعات 
        [TestMethod]
        public void IfFirstSelectProductAndThenSelectOtherIsOk()
        {
            vm.SelectedProduct = pds[1];

            Assert.IsNotNull(vm.SelecteddItem);//یک آیتم برای ذخیره شدن ساخته شد دیگه
            Assert.AreEqual(pds[1], vm.SelecteddItem.Product);

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsFalse(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));

            vm.Tedad = 10;


            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsTrue(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));

            vm.SelectedDestenation = cts[2];

            Assert.AreEqual(cts[2], vm.SelecteddItem.Customer);

            vm.SelectedDriver = drs[1];

            Assert.AreEqual(drs[1], vm.SelecteddItem.Driver);

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsTrue(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));

            

        }


        //اول اطلاعات اضافی بعد کالا
        [TestMethod]
        public void IfFirstSelectDestenationAndDriverThenSelectProduct()
        {
            vm.SelectedDestenation = cts[2];
            vm.SelectedDriver = drs[2];

            Assert.IsNull(vm.SelecteddItem);

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsFalse(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));

            vm.SelectedProduct = pds[3];

            //بعد از اینکه کالا انتخاب شد باید بشه 
            Assert.AreEqual(cts[2], vm.SelecteddItem.Customer);
            Assert.AreEqual(drs[2], vm.SelecteddItem.Driver);

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsFalse(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));

            vm.Tedad = 15000;

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsTrue(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));
        }


        //بعد از افزودن آیتم چه میشود
        [TestMethod]
        public void IfAddNewItem()
        {
            vm.SelectedDestenation = cts[1];
            vm.SelectedDriver = drs[1];
            vm.SelectedProduct = pds[1];
            vm.Tedad = 100;

            Assert.IsNotNull(vm.SelecteddItem);

            vm.AddNewItem.Execute(null);

            Assert.AreEqual(1, vm.Items.Count);
            Assert.IsNull(vm.SelecteddItem);
            Assert.IsNull(vm.SelectedDestenation);
            Assert.IsNull(vm.SelectedDriver);
            Assert.IsNull(vm.SelectedProduct);
            Assert.AreEqual(0,vm.Tedad);
        }
       
        //بعد از ذخیره سازی سفارش چه میشود
        [TestMethod]
        public void IfSaveSefareshIsOkForm()
        {
            vm.Items = new ObservableCollection<ItemSefaresh>(){
                new ItemSefaresh(pds[0]){},
                new ItemSefaresh(pds[1]),
                new ItemSefaresh(pds[2]),
                new ItemSefaresh(pds[3])
            };

            vm.Tarikh = "1395/05/05";

            Assert.IsTrue(vm.SaveSefaresh.CanExecute(null));

            vm.SaveSefaresh.Execute(null);

            Assert.AreEqual(1, InDBOrder.Count);
            Assert.AreEqual(4, InDBOrderDetails.Count);

            Assert.AreEqual("1395/05/05", InDBOrder[0].Tarikh);
            Assert.AreEqual(pds[0],InDBOrderDetails[0].Product);

        }

        // یک آیتم انتخاب شد چه میشود
        [TestMethod]
        public void IfSelectedItem()
        {
            vm.Items = new ObservableCollection<ItemSefaresh>(){
                new ItemSefaresh(pds[0]){Customer=cts[0],Driver=drs[0],Tedad=50},
                new ItemSefaresh(pds[1]){Customer=cts[1],Driver=drs[1],Tedad=50},
                new ItemSefaresh(pds[2]){Customer=cts[2],Driver=drs[2],Tedad=50},
                new ItemSefaresh(pds[3]){Customer=cts[0],Driver=drs[3],Tedad=50}
            };

            vm.SelecteddItem = vm.Items[1];

            Assert.AreEqual(pds[1], vm.SelectedProduct);
            Assert.AreEqual(drs[1], vm.SelectedDriver);
            Assert.AreEqual(cts[1], vm.SelectedDestenation);
            تعداد هم چک شود

            Assert.IsFalse(vm.ADDDriverDestenation.CanExecute(null));
            Assert.IsTrue(vm.AddNewItem.CanExecute(null));
            Assert.IsFalse(vm.SaveSefaresh.CanExecute(null));

        }

        //اگر یک آیتم را انتخاب کردیم و همه بالایی ها را انجام دهیم چه اتفاقی می افتد

        //یک آیتم تیک خورد همه بالا انجام شد چه میشود  

        //چند آیتم تیک خورد همه بالا چه میشود

    }
}

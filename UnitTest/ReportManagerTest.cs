using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersAndisheh.BL;
using OrdersAndisheh.DBL;
using System.Collections.ObjectModel;
using BL;

namespace UnitTest
{
    [TestClass]
    public class ReportManagerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Product p = new Product() { Code = "656", Name = "hghg" };
            //باید یادمان باشد ایتم سفارش اینجا چک نمیشود - نحوه ذخیره سازی هم اینجا چک نمی شود
            // اینجا فقط ساخت یک سفارش چک میشود
            ObservableCollection<ItemSefaresh> f = new ObservableCollection<ItemSefaresh>()
            {
                new ItemSefaresh(p){ItemKind="فوری"},
                new ItemSefaresh(p){ItemKind="نامشخص"},
                new ItemSefaresh(p){ItemKind="گواهی"},
                new ItemSefaresh(p){ItemKind="عادی"},
                new ItemSefaresh(p){ItemKind="گواهی"},
                new ItemSefaresh(p){ItemKind="عادی"},
                new ItemSefaresh(p){ItemKind="عادی"},
                new ItemSefaresh(p){ItemKind="گواهی"},
                new ItemSefaresh(p){ItemKind="گواهی"},
                new ItemSefaresh(p){ItemKind="گواهی"},
            };
            Sefaresh s = new Sefaresh();
            s.Tarikh = "1395/01/02";
            s.Items = f;


            ReportManager rp = new ReportManager(s);

            rp.CreatAllBazresReportOnDeskTop();

        }
    }
}

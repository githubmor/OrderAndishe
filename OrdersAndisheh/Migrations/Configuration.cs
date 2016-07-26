namespace OrdersAndisheh.Migrations
{
    using OrdersAndisheh.DBL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyContextCF>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            
        }

        protected override void Seed(OrdersAndisheh.DBL.MyContextCF context)
        {
            //  This method will be called after migrating to the latest version.

            //context.Orders.AddOrUpdate(
            //    new Order() { Id = 1, Tarikh = "1395/04/12", Description = "seed_test", Accepted = false }
            //    );
            //context.Customers.AddOrUpdate(
            //    new Customer() { Id = 1, Name = "سایپا" },
            //    new Customer() { Id = 2, Name = "ایرانخودرو" },
            //    new Customer() { Id = 3, Name = "پارس خودرو" },
            //    new Customer() { Id = 4, Name = "اپکو" }
            //    );

            //context.Drivers.AddOrUpdate(
            //    new Driver() { Id = 1, Nam = "پورشریف", Pelak = "16-27ع845", Ton = 4 },
            //    new Driver() { Id = 2, Nam = "اسدی", Pelak = "19-54ف789", Ton = 7 }
            //    );

            //context.Bazress.AddOrUpdate(
            //    new Bazres() { Id = 1, Name = "فهامه" },
            //    new Bazres() { Id = 2, Name = "گرما فلز" }
            //    );
            //context.Pallets.AddOrUpdate(
            //   new Pallet() { Id = 1, Name = "GP8", Vazn = 200 },
            //   new Pallet() { Id = 2, Name = "پالت کفی", Vazn = 20 }
            //   );

            //context.Products.AddOrUpdate(
            //    new Product() { Id = 1, Bazres_Id = 5, Code = "15001025", IsImenKala = false, Nam = "کمربند 4 درب", PalletId = 5 },
            //    new Product() { Id = 2, Bazres_Id = 5, Code = "15001026", IsImenKala = false, Nam = "قفل سمند مشکی", PalletId = 6 },
            //    new Product() { Id = 3, Bazres_Id = 6, Code = "15001027", IsImenKala = false, Nam = "کمربند عقب تیبا", PalletId = 5 },
            //    new Product() { Id = 4, Bazres_Id = 6, Code = "15010001", IsImenKala = true, Nam = "ایربگ راننده", PalletId = 6 },
            //    new Product() { Id = 5, Bazres_Id = 6, Code = "15011001", IsImenKala = true, Nam = "غربیلک 206", PalletId = 6 }
            //    );

            //context.OrderDetails.AddOrUpdate(
            //    new OrderDetail() { OrderId = 1, CustomerId = 9, DriverId = 5, HasOracle = true, Id = 1, ItemType = 1, ProductId = 5, TahvilForosh = 540, Tedad = 500 },
            //    new OrderDetail() { OrderId = 1, CustomerId = 11, DriverId = 5, HasOracle = true, Id = 2, ItemType = 1, ProductId = 8, TahvilForosh = 541, Tedad = 20 },
            //    new OrderDetail() { OrderId = 1, CustomerId = 12, DriverId = 6, HasOracle = true, Id = 3, ItemType = 2, ProductId = 7, TahvilForosh = 555, Tedad = 2000 },
            //    new OrderDetail() { OrderId = 1, CustomerId = 12, DriverId = 6, HasOracle = true, Id = 4, ItemType = 0, ProductId = 6, TahvilForosh = 584, Tedad = 115 },
            //    new OrderDetail() { OrderId = 1, CustomerId = 10, DriverId = 5, HasOracle = true, Id = 5, ItemType = 1, ProductId = 9, TahvilForosh = 568, Tedad = 216 }
            //    );

            ////  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}

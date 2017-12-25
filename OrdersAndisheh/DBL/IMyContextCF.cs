using System;
using System.Data.Entity;
namespace OrdersAndisheh.DBL
{
    public interface IMyContextCF 
    {
        System.Data.Entity.DbSet<Amount> Amount { get; set; }
        System.Data.Entity.DbSet<Baste> Bastes { get; set; }
        System.Data.Entity.DbSet<Bazres> Bazress { get; set; }
        System.Data.Entity.DbSet<CustomerProductRelation> CustomerProductRelations { get; set; }
        System.Data.Entity.DbSet<Customer> Customers { get; set; }
        System.Data.Entity.DbSet<Driver> Drivers { get; set; }
        System.Data.Entity.DbSet<DriverWork> DriverWork { get; set; }
        System.Data.Entity.DbSet<MOracle> MOracles { get; set; }
        System.Data.Entity.DbSet<OracleRelation> OracleRelation { get; set; }
        System.Data.Entity.DbSet<OrderDetail> OrderDetails { get; set; }
        System.Data.Entity.DbSet<Order> Orders { get; set; }
        System.Data.Entity.DbSet<Pallet> Pallets { get; set; }
        System.Data.Entity.DbSet<Product> Products { get; set; }
        System.Data.Entity.DbSet<TempDriver> TempDriver { get; set; }

        int SaveChanges();
    }
}

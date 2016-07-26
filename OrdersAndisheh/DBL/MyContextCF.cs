namespace OrdersAndisheh.DBL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using OrdersAndisheh.Migrations;

    public class MyContextCF : DbContext
    {
        public MyContextCF()
            : base("name=CodeFirstConection")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyContextCF>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<MyContextCF>());
        }

        public virtual DbSet<Bazres> Bazress { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Pallet> Pallets { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bazres>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Bazre)
                .HasForeignKey(e => e.Bazres_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .HasOptional(p => p.Customer)
                .WithMany()
                .HasForeignKey(o => o.Customer_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderDetail>()
                .HasOptional(p => p.Driver)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                 .HasMany(p => p.OrderDetails)
                 .WithOptional(p => p.Customer)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                 .HasMany(p => p.OrderDetails)
                 .WithOptional(p => p.Driver)
                 .HasForeignKey(i => i.Customer_Id)
                 .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Order>()
            //    .Property(e => e.Tarikh)
            //    .IsFixedLength();

            modelBuilder.Entity<Order>()
                .Property(e => e.Description)
                .IsUnicode(true);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.Description)
                .IsUnicode(true);

            modelBuilder.Entity<Pallet>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Pallet)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Product>()
            //    .Property(e => e.Code)
            //    .IsFixedLength();

            //modelBuilder.Entity<Product>()
            //    .Property(e => e.FaniCode)
            //    .IsFixedLength();

            //modelBuilder.Entity<Product>()
            //    .Property(e => e.CodeJense)
            //    .IsFixedLength();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}

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
            : base("name=CodeFirstConectionCompact")
        {
            base.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
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
        public virtual DbSet<MOracle> MOracles { get; set; }
        public virtual DbSet<CustomerProductRelation> CustomerProductRelations { get; set; }
        public virtual DbSet<TempDriver> TempDriver { get; set; }

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
                .HasForeignKey(o => o.Driver_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                 .HasMany(p => p.OrderDetails)
                 .WithOptional(p => p.Customer)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                 .HasMany(p => p.OrderDetails)
                 .WithOptional(p => p.Driver)
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

            modelBuilder.Entity<OrderDetail>()
                .HasOptional(p => p.MOracle)
                .WithRequired(p => p.OrderDetail)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Driver>()
                .HasOptional(e => e.TempDriver)
                .WithRequired(e => e.Driver)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CustomerProductRelation>()
                .HasRequired(p => p.Customer)
                .WithMany(o => o.Relations)
                .HasForeignKey(p=>p.CustomerId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<CustomerProductRelation>()
                .HasRequired(p => p.Product)
                .WithMany(o => o.Relations)
                .HasForeignKey(p => p.ProductId)
                .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Product>()
            //    .HasMany(o=>o.OracleCustomers)
            //    .WithMany(p=>p.OracleProducts)
            //    .Map(
            //        m =>
            //        {
            //            m.MapLeftKey("ProductId");
            //            m.MapRightKey("CustomerId");
            //            m.ToTable("OracleRelations");
            //        });
        }
    }
}

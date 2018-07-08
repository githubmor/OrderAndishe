using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using OrdersAndisheh.Common.Utils;
using OrdersAndisheh.Common.DataModel;
using OrdersAndisheh.Common.DataModel.DesignTime;
using OrdersAndisheh.Common.DataModel.EntityFramework;
using OrdersAndisheh.DBL;

namespace OrdersAndisheh.DevUI.MyContextCFDataModel
{
    /// <summary>
    /// A MyContextCFUnitOfWork instance that represents the run-time implementation of the IMyContextCFUnitOfWork interface.
    /// </summary>
    public class MyContextCFUnitOfWork : DbUnitOfWork<MyContextCF>, IMyContextCFUnitOfWork
    {

        public MyContextCFUnitOfWork(Func<MyContextCF> contextFactory)
            : base(contextFactory)
        {
        }

        IRepository<AmarTolidKhodro, int> IMyContextCFUnitOfWork.AmarTolidKhodros
        {
            get { return GetRepository(x => x.Set<AmarTolidKhodro>(), x => x.Id); }
        }

        IRepository<Khodro, int> IMyContextCFUnitOfWork.Khodros
        {
            get { return GetRepository(x => x.Set<Khodro>(), x => x.Id); }
        }

        IRepository<KhodroProductRelation, int> IMyContextCFUnitOfWork.KhodroProductRelation
        {
            get { return GetRepository(x => x.Set<KhodroProductRelation>(), x => x.Id); }
        }

        IRepository<Product, int> IMyContextCFUnitOfWork.Products
        {
            get { return GetRepository(x => x.Set<Product>(), x => x.Id); }
        }

        IRepository<Amount, int> IMyContextCFUnitOfWork.Amount
        {
            get { return GetRepository(x => x.Set<Amount>(), x => x.ProductId); }
        }

        IRepository<Baste, int> IMyContextCFUnitOfWork.Bastes
        {
            get { return GetRepository(x => x.Set<Baste>(), x => x.Id); }
        }

        IRepository<Bazres, int> IMyContextCFUnitOfWork.Bazress
        {
            get { return GetRepository(x => x.Set<Bazres>(), x => x.Id); }
        }

        IRepository<OracleRelation, int> IMyContextCFUnitOfWork.OracleRelation
        {
            get { return GetRepository(x => x.Set<OracleRelation>(), x => x.Id); }
        }

        IRepository<Customer, int> IMyContextCFUnitOfWork.Customers
        {
            get { return GetRepository(x => x.Set<Customer>(), x => x.Id); }
        }

        IRepository<OrderDetail, int> IMyContextCFUnitOfWork.OrderDetails
        {
            get { return GetRepository(x => x.Set<OrderDetail>(), x => x.Id); }
        }

        IRepository<Driver, int> IMyContextCFUnitOfWork.Drivers
        {
            get { return GetRepository(x => x.Set<Driver>(), x => x.Id); }
        }

        IRepository<DriverWork, int> IMyContextCFUnitOfWork.DriverWork
        {
            get { return GetRepository(x => x.Set<DriverWork>(), x => x.DriverWorkId); }
        }

        IRepository<Order, int> IMyContextCFUnitOfWork.Orders
        {
            get { return GetRepository(x => x.Set<Order>(), x => x.Id); }
        }

        IRepository<TempDriver, int> IMyContextCFUnitOfWork.TempDriver
        {
            get { return GetRepository(x => x.Set<TempDriver>(), x => x.Id); }
        }

        IRepository<MOracle, int> IMyContextCFUnitOfWork.MOracles
        {
            get { return GetRepository(x => x.Set<MOracle>(), x => x.Id); }
        }

        IRepository<CustomerProductRelation, int> IMyContextCFUnitOfWork.CustomerProductRelations
        {
            get { return GetRepository(x => x.Set<CustomerProductRelation>(), x => x.Id); }
        }

        IRepository<Pallet, int> IMyContextCFUnitOfWork.Pallets
        {
            get { return GetRepository(x => x.Set<Pallet>(), x => x.Id); }
        }
    }
}

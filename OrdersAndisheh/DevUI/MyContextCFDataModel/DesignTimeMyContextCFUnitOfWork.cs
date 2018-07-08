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
    /// A MyContextCFDesignTimeUnitOfWork instance that represents the design-time implementation of the IMyContextCFUnitOfWork interface.
    /// </summary>
    public class MyContextCFDesignTimeUnitOfWork : DesignTimeUnitOfWork, IMyContextCFUnitOfWork
    {

        /// <summary>
        /// Initializes a new instance of the MyContextCFDesignTimeUnitOfWork class.
        /// </summary>
        public MyContextCFDesignTimeUnitOfWork()
        {
        }

        IRepository<AmarTolidKhodro, int> IMyContextCFUnitOfWork.AmarTolidKhodros
        {
            get { return GetRepository((AmarTolidKhodro x) => x.Id); }
        }

        IRepository<Khodro, int> IMyContextCFUnitOfWork.Khodros
        {
            get { return GetRepository((Khodro x) => x.Id); }
        }

        IRepository<KhodroProductRelation, int> IMyContextCFUnitOfWork.KhodroProductRelation
        {
            get { return GetRepository((KhodroProductRelation x) => x.Id); }
        }

        IRepository<Product, int> IMyContextCFUnitOfWork.Products
        {
            get { return GetRepository((Product x) => x.Id); }
        }

        IRepository<Amount, int> IMyContextCFUnitOfWork.Amount
        {
            get { return GetRepository((Amount x) => x.ProductId); }
        }

        IRepository<Baste, int> IMyContextCFUnitOfWork.Bastes
        {
            get { return GetRepository((Baste x) => x.Id); }
        }

        IRepository<Bazres, int> IMyContextCFUnitOfWork.Bazress
        {
            get { return GetRepository((Bazres x) => x.Id); }
        }

        IRepository<OracleRelation, int> IMyContextCFUnitOfWork.OracleRelation
        {
            get { return GetRepository((OracleRelation x) => x.Id); }
        }

        IRepository<Customer, int> IMyContextCFUnitOfWork.Customers
        {
            get { return GetRepository((Customer x) => x.Id); }
        }

        IRepository<OrderDetail, int> IMyContextCFUnitOfWork.OrderDetails
        {
            get { return GetRepository((OrderDetail x) => x.Id); }
        }

        IRepository<Driver, int> IMyContextCFUnitOfWork.Drivers
        {
            get { return GetRepository((Driver x) => x.Id); }
        }

        IRepository<DriverWork, int> IMyContextCFUnitOfWork.DriverWork
        {
            get { return GetRepository((DriverWork x) => x.DriverWorkId); }
        }

        IRepository<Order, int> IMyContextCFUnitOfWork.Orders
        {
            get { return GetRepository((Order x) => x.Id); }
        }

        IRepository<TempDriver, int> IMyContextCFUnitOfWork.TempDriver
        {
            get { return GetRepository((TempDriver x) => x.Id); }
        }

        IRepository<MOracle, int> IMyContextCFUnitOfWork.MOracles
        {
            get { return GetRepository((MOracle x) => x.Id); }
        }

        IRepository<CustomerProductRelation, int> IMyContextCFUnitOfWork.CustomerProductRelations
        {
            get { return GetRepository((CustomerProductRelation x) => x.Id); }
        }

        IRepository<Pallet, int> IMyContextCFUnitOfWork.Pallets
        {
            get { return GetRepository((Pallet x) => x.Id); }
        }
    }
}

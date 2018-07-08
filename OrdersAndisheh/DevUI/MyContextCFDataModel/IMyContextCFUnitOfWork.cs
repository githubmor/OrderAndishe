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
    /// IMyContextCFUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface IMyContextCFUnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// The AmarTolidKhodro entities repository.
        /// </summary>
        IRepository<AmarTolidKhodro, int> AmarTolidKhodros { get; }

        /// <summary>
        /// The Khodro entities repository.
        /// </summary>
        IRepository<Khodro, int> Khodros { get; }

        /// <summary>
        /// The KhodroProductRelation entities repository.
        /// </summary>
        IRepository<KhodroProductRelation, int> KhodroProductRelation { get; }

        /// <summary>
        /// The Product entities repository.
        /// </summary>
        IRepository<Product, int> Products { get; }

        /// <summary>
        /// The Amount entities repository.
        /// </summary>
        IRepository<Amount, int> Amount { get; }

        /// <summary>
        /// The Baste entities repository.
        /// </summary>
        IRepository<Baste, int> Bastes { get; }

        /// <summary>
        /// The Bazres entities repository.
        /// </summary>
        IRepository<Bazres, int> Bazress { get; }

        /// <summary>
        /// The OracleRelation entities repository.
        /// </summary>
        IRepository<OracleRelation, int> OracleRelation { get; }

        /// <summary>
        /// The Customer entities repository.
        /// </summary>
        IRepository<Customer, int> Customers { get; }

        /// <summary>
        /// The OrderDetail entities repository.
        /// </summary>
        IRepository<OrderDetail, int> OrderDetails { get; }

        /// <summary>
        /// The Driver entities repository.
        /// </summary>
        IRepository<Driver, int> Drivers { get; }

        /// <summary>
        /// The DriverWork entities repository.
        /// </summary>
        IRepository<DriverWork, int> DriverWork { get; }

        /// <summary>
        /// The Order entities repository.
        /// </summary>
        IRepository<Order, int> Orders { get; }

        /// <summary>
        /// The TempDriver entities repository.
        /// </summary>
        IRepository<TempDriver, int> TempDriver { get; }

        /// <summary>
        /// The MOracle entities repository.
        /// </summary>
        IRepository<MOracle, int> MOracles { get; }

        /// <summary>
        /// The CustomerProductRelation entities repository.
        /// </summary>
        IRepository<CustomerProductRelation, int> CustomerProductRelations { get; }

        /// <summary>
        /// The Pallet entities repository.
        /// </summary>
        IRepository<Pallet, int> Pallets { get; }
    }
}

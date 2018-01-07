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
        /// The Driver entities repository.
        /// </summary>
        IRepository<Driver, int> Drivers { get; }

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

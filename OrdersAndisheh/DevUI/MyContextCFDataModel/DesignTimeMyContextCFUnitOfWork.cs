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

        IRepository<Bazres, int> IMyContextCFUnitOfWork.Bazress
        {
            get { return GetRepository((Bazres x) => x.Id); }
        }

        IRepository<Product, int> IMyContextCFUnitOfWork.Products
        {
            get { return GetRepository((Product x) => x.Id); }
        }

        IRepository<Customer, int> IMyContextCFUnitOfWork.Customers
        {
            get { return GetRepository((Customer x) => x.Id); }
        }

        IRepository<CustomerProductRelation, int> IMyContextCFUnitOfWork.CustomerProductRelations
        {
            get { return GetRepository((CustomerProductRelation x) => x.Id); }
        }

        IRepository<Driver, int> IMyContextCFUnitOfWork.Drivers
        {
            get { return GetRepository((Driver x) => x.Id); }
        }

        IRepository<Pallet, int> IMyContextCFUnitOfWork.Pallets
        {
            get { return GetRepository((Pallet x) => x.Id); }
        }
    }
}

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

        IRepository<Bazres, int> IMyContextCFUnitOfWork.Bazress
        {
            get { return GetRepository(x => x.Set<Bazres>(), x => x.Id); }
        }

        IRepository<Product, int> IMyContextCFUnitOfWork.Products
        {
            get { return GetRepository(x => x.Set<Product>(), x => x.Id); }
        }

        IRepository<Customer, int> IMyContextCFUnitOfWork.Customers
        {
            get { return GetRepository(x => x.Set<Customer>(), x => x.Id); }
        }

        IReadOnlyRepository<CustomerProductRelation> IMyContextCFUnitOfWork.CustomerProductRelations
        {
            get { return GetReadOnlyRepository(x => x.Set<CustomerProductRelation>()); }
        }

        IRepository<Driver, int> IMyContextCFUnitOfWork.Drivers
        {
            get { return GetRepository(x => x.Set<Driver>(), x => x.Id); }
        }

        IRepository<Pallet, int> IMyContextCFUnitOfWork.Pallets
        {
            get { return GetRepository(x => x.Set<Pallet>(), x => x.Id); }
        }
    }
}

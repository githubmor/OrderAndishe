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

        IRepository<Driver, int> IMyContextCFUnitOfWork.Drivers
        {
            get { return GetRepository(x => x.Set<Driver>(), x => x.Id); }
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

﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using OrdersAndisheh.Common.Utils;
using OrdersAndisheh.DevUI.MyContextCFDataModel;
using OrdersAndisheh.Common.DataModel;
using OrdersAndisheh.DBL;
using OrdersAndisheh.Common.ViewModel;

namespace OrdersAndisheh.DevUI.ViewModels
{
    /// <summary>
    /// Represents the single Customer object view model.
    /// </summary>
    public partial class CustomerViewModel : SingleObjectViewModel<Customer, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of CustomerViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CustomerViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new CustomerViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CustomerViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CustomerViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CustomerViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Customers, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the CustomerOrderDetails detail collection.
        /// </summary>
        public CollectionViewModel<OrderDetail, int, IMyContextCFUnitOfWork> CustomerOrderDetailsDetails
        {
            get { return GetDetailsCollectionViewModel((CustomerViewModel x) => x.CustomerOrderDetailsDetails, x => x.OrderDetails, x => x.Customer_Id, (x, key) => x.Customer_Id = key); }
        }

        /// <summary>
        /// The view model for the CustomerRelations detail collection.
        /// </summary>
        public CollectionViewModel<CustomerProductRelation, int, IMyContextCFUnitOfWork> CustomerRelationsDetails
        {
            get { return GetDetailsCollectionViewModel((CustomerViewModel x) => x.CustomerRelationsDetails, x => x.CustomerProductRelations, x => x.CustomerId, (x, key) => x.CustomerId = key); }
        }

        /// <summary>
        /// The view model for the CustomerOracleRelations detail collection.
        /// </summary>
        public CollectionViewModel<OracleRelation, int, IMyContextCFUnitOfWork> CustomerOracleRelationsDetails
        {
            get { return GetDetailsCollectionViewModel((CustomerViewModel x) => x.CustomerOracleRelationsDetails, x => x.OracleRelation, x => x.CustomerId, (x, key) => x.CustomerId = key); }
        }
    }
}

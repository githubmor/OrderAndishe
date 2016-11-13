using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using OrdersAndisheh.Common.Utils;
using OrdersAndisheh.DevUI.MyContextCFDataModel1;
using OrdersAndisheh.Common.DataModel;
using OrdersAndisheh.DBL;
using OrdersAndisheh.Common.ViewModel;

namespace OrdersAndisheh.DevUI.ViewModels
{
    /// <summary>
    /// Represents the single CustomerProductRelation object view model.
    /// </summary>
    public partial class CustomerProductRelationViewModel : SingleObjectViewModel<CustomerProductRelation, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of CustomerProductRelationViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static CustomerProductRelationViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new CustomerProductRelationViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the CustomerProductRelationViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the CustomerProductRelationViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected CustomerProductRelationViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.CustomerProductRelations, x => x.Id)
        {
        }

        /// <summary>
        /// The view model for the CustomerProductRelationCustomers detail collection.
        /// </summary>
        public CollectionViewModel<Customer, int, IMyContextCFUnitOfWork> CustomerProductRelationCustomersDetails
        {
            get { return GetDetailsCollectionViewModel((CustomerProductRelationViewModel x) => x.CustomerProductRelationCustomersDetails, x => x.Customers, x => x.RelationId, (x, key) => x.RelationId = key); }
        }

        /// <summary>
        /// The view model for the CustomerProductRelationProducts detail collection.
        /// </summary>
        public CollectionViewModel<Product, int, IMyContextCFUnitOfWork> CustomerProductRelationProductsDetails
        {
            get { return GetDetailsCollectionViewModel((CustomerProductRelationViewModel x) => x.CustomerProductRelationProductsDetails, x => x.Products, x => x.RelationId, (x, key) => x.RelationId = key); }
        }
    }
}

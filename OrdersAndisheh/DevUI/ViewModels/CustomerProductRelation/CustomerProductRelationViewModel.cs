using System;
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
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.CustomerProductRelations, x => x.CustomerId)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Products for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Product> LookUpProducts
        {
            get { return GetLookUpEntitiesViewModel((CustomerProductRelationViewModel x) => x.LookUpProducts, x => x.Products); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Customers for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Customer> LookUpCustomers
        {
            get { return GetLookUpEntitiesViewModel((CustomerProductRelationViewModel x) => x.LookUpCustomers, x => x.Customers); }
        }
    }
}

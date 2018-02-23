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
    /// Represents the single OrderDetail object view model.
    /// </summary>
    public partial class OrderDetailViewModel : SingleObjectViewModel<OrderDetail, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of OrderDetailViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static OrderDetailViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new OrderDetailViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the OrderDetailViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the OrderDetailViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected OrderDetailViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.OrderDetails, x => x.Description)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Products for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Product> LookUpProducts
        {
            get { return GetLookUpEntitiesViewModel((OrderDetailViewModel x) => x.LookUpProducts, x => x.Products); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Customers for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Customer> LookUpCustomers
        {
            get { return GetLookUpEntitiesViewModel((OrderDetailViewModel x) => x.LookUpCustomers, x => x.Customers); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Drivers for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Driver> LookUpDrivers
        {
            get { return GetLookUpEntitiesViewModel((OrderDetailViewModel x) => x.LookUpDrivers, x => x.Drivers); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Orders for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Order> LookUpOrders
        {
            get { return GetLookUpEntitiesViewModel((OrderDetailViewModel x) => x.LookUpOrders, x => x.Orders); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of MOracles for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<MOracle> LookUpMOracles
        {
            get { return GetLookUpEntitiesViewModel((OrderDetailViewModel x) => x.LookUpMOracles, x => x.MOracles); }
        }
    }
}

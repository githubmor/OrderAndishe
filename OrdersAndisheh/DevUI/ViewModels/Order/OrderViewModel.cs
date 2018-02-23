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
    /// Represents the single Order object view model.
    /// </summary>
    public partial class OrderViewModel : SingleObjectViewModel<Order, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of OrderViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static OrderViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new OrderViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the OrderViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the OrderViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected OrderViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Orders, x => x.Description)
        {
        }

        /// <summary>
        /// The view model for the OrderOrderDetails detail collection.
        /// </summary>
        public CollectionViewModel<OrderDetail, int, IMyContextCFUnitOfWork> OrderOrderDetailsDetails
        {
            get { return GetDetailsCollectionViewModel((OrderViewModel x) => x.OrderOrderDetailsDetails, x => x.OrderDetails, x => x.OrderId, (x, key) => x.OrderId = key); }
        }

        /// <summary>
        /// The view model for the OrderDriverWorks detail collection.
        /// </summary>
        public CollectionViewModel<DriverWork, int, IMyContextCFUnitOfWork> OrderDriverWorksDetails
        {
            get { return GetDetailsCollectionViewModel((OrderViewModel x) => x.OrderDriverWorksDetails, x => x.DriverWork, x => x.OrderId, (x, key) => x.OrderId = key); }
        }
    }
}

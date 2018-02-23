using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using OrdersAndisheh.Common.Utils;
using OrdersAndisheh.DevUI.MyContextCFDataModel;
using OrdersAndisheh.Common.DataModel;
using OrdersAndisheh.DBL;
using OrdersAndisheh.Common.ViewModel;

namespace OrdersAndisheh.DevUI.ViewModels
{
    /// <summary>
    /// Represents the OrderDetails collection view model.
    /// </summary>
    public partial class OrderDetailCollectionViewModel : CollectionViewModel<OrderDetail, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of OrderDetailCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static OrderDetailCollectionViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new OrderDetailCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the OrderDetailCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the OrderDetailCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected OrderDetailCollectionViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.OrderDetails)
        {
        }
    }
}
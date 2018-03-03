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
    /// Represents the single Driver object view model.
    /// </summary>
    public partial class DriverViewModel : SingleObjectViewModel<Driver, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of DriverViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static DriverViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new DriverViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the DriverViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the DriverViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected DriverViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Drivers, x => x.Name)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of TempDriver for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<TempDriver> LookUpTempDriver
        {
            get { return GetLookUpEntitiesViewModel((DriverViewModel x) => x.LookUpTempDriver, x => x.TempDriver); }
        }

        /// <summary>
        /// The view model for the DriverOrderDetails detail collection.
        /// </summary>
        public CollectionViewModel<OrderDetail, int, IMyContextCFUnitOfWork> DriverOrderDetailsDetails
        {
            get { return GetDetailsCollectionViewModel((DriverViewModel x) => x.DriverOrderDetailsDetails, x => x.OrderDetails, x => x.Driver_Id, (x, key) => x.Driver_Id = key); }
        }

        /// <summary>
        /// The view model for the DriverDriverWorks detail collection.
        /// </summary>
        public CollectionViewModel<DriverWork, int, IMyContextCFUnitOfWork> DriverDriverWorksDetails
        {
            get { return GetDetailsCollectionViewModel((DriverViewModel x) => x.DriverDriverWorksDetails, x => x.DriverWork, x => x.DriverId, (x, key) => x.DriverId = key); }
        }
    }
}

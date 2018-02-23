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
    /// Represents the single DriverWork object view model.
    /// </summary>
    public partial class DriverWorkViewModel : SingleObjectViewModel<DriverWork, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of DriverWorkViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static DriverWorkViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new DriverWorkViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the DriverWorkViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the DriverWorkViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected DriverWorkViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.DriverWork, x => x.Works)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Drivers for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Driver> LookUpDrivers
        {
            get { return GetLookUpEntitiesViewModel((DriverWorkViewModel x) => x.LookUpDrivers, x => x.Drivers); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Orders for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Order> LookUpOrders
        {
            get { return GetLookUpEntitiesViewModel((DriverWorkViewModel x) => x.LookUpOrders, x => x.Orders); }
        }
    }
}

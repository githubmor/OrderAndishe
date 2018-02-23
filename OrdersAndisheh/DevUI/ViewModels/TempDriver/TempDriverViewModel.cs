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
    /// Represents the single TempDriver object view model.
    /// </summary>
    public partial class TempDriverViewModel : SingleObjectViewModel<TempDriver, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of TempDriverViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static TempDriverViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new TempDriverViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the TempDriverViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the TempDriverViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected TempDriverViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.TempDriver, x => x.Name)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Drivers for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Driver> LookUpDrivers
        {
            get { return GetLookUpEntitiesViewModel((TempDriverViewModel x) => x.LookUpDrivers, x => x.Drivers); }
        }
    }
}

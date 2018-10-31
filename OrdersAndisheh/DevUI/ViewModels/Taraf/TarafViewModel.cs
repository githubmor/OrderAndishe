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
    /// Represents the single Taraf object view model.
    /// </summary>
    public partial class TarafViewModel : SingleObjectViewModel<Taraf, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of TarafViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static TarafViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new TarafViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the TarafViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the TarafViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected TarafViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Taraf, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the TarafKhodros detail collection.
        /// </summary>
        public CollectionViewModel<Khodro, int, IMyContextCFUnitOfWork> TarafKhodrosDetails
        {
            get { return GetDetailsCollectionViewModel((TarafViewModel x) => x.TarafKhodrosDetails, x => x.Khodros, x => x.TarafId, (x, key) => x.TarafId = key); }
        }
    }
}

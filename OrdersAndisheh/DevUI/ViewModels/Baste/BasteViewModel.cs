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
    /// Represents the single Baste object view model.
    /// </summary>
    public partial class BasteViewModel : SingleObjectViewModel<Baste, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of BasteViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BasteViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BasteViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BasteViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BasteViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected BasteViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Bastes, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the BasteProducts detail collection.
        /// </summary>
        public CollectionViewModel<Product, int, IMyContextCFUnitOfWork> BasteProductsDetails
        {
            get { return GetDetailsCollectionViewModel((BasteViewModel x) => x.BasteProductsDetails, x => x.Products, x => x.BasteId, (x, key) => x.BasteId = key); }
        }
    }
}

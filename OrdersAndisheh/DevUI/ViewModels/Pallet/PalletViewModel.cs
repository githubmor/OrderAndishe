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
    /// Represents the single Pallet object view model.
    /// </summary>
    public partial class PalletViewModel : SingleObjectViewModel<Pallet, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of PalletViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static PalletViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new PalletViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the PalletViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the PalletViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected PalletViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Pallets, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the PalletProducts detail collection.
        /// </summary>
        public CollectionViewModel<Product, int, IMyContextCFUnitOfWork> PalletProductsDetails
        {
            get { return GetDetailsCollectionViewModel((PalletViewModel x) => x.PalletProductsDetails, x => x.Products, x => x.PalletId, (x, key) => x.PalletId = key); }
        }
    }
}

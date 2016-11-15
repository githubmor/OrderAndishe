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
    /// Represents the single Bazres object view model.
    /// </summary>
    public partial class BazresViewModel : SingleObjectViewModel<Bazres, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of BazresViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static BazresViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new BazresViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the BazresViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the BazresViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected BazresViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Bazress, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the BazresProducts detail collection.
        /// </summary>
        public CollectionViewModel<Product, int, IMyContextCFUnitOfWork> BazresProductsDetails
        {
            get { return GetDetailsCollectionViewModel((BazresViewModel x) => x.BazresProductsDetails, x => x.Products, x => x.Bazres_Id, (x, key) => x.Bazres_Id = key); }
        }
    }
}

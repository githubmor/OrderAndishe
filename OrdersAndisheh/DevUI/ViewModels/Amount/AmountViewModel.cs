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
    /// Represents the single Amount object view model.
    /// </summary>
    public partial class AmountViewModel : SingleObjectViewModel<Amount, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of AmountViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static AmountViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new AmountViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the AmountViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the AmountViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected AmountViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Amount, x => x.ProductId)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Products for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Product> LookUpProducts
        {
            get { return GetLookUpEntitiesViewModel((AmountViewModel x) => x.LookUpProducts, x => x.Products); }
        }
    }
}

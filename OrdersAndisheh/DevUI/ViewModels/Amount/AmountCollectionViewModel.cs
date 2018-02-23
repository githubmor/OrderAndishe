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
    /// Represents the Amount collection view model.
    /// </summary>
    public partial class AmountCollectionViewModel : CollectionViewModel<Amount, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of AmountCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static AmountCollectionViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new AmountCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the AmountCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the AmountCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected AmountCollectionViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Amount)
        {
        }
    }
}
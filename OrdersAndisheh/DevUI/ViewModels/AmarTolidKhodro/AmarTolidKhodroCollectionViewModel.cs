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
    /// Represents the AmarTolidKhodros collection view model.
    /// </summary>
    public partial class AmarTolidKhodroCollectionViewModel : ReadOnlyCollectionViewModel<AmarTolidKhodro, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of AmarTolidKhodroCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static AmarTolidKhodroCollectionViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new AmarTolidKhodroCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the AmarTolidKhodroCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the AmarTolidKhodroCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected AmarTolidKhodroCollectionViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.AmarTolidKhodros)
        {
        }
    }
}
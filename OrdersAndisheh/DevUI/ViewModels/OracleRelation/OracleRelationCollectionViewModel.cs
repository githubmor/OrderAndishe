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
    /// Represents the OracleRelation collection view model.
    /// </summary>
    public partial class OracleRelationCollectionViewModel : CollectionViewModel<OracleRelation, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of OracleRelationCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static OracleRelationCollectionViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new OracleRelationCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the OracleRelationCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the OracleRelationCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected OracleRelationCollectionViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.OracleRelation)
        {
        }
    }
}
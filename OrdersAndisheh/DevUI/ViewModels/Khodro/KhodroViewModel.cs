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
    /// Represents the single Khodro object view model.
    /// </summary>
    public partial class KhodroViewModel : SingleObjectViewModel<Khodro, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of KhodroViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static KhodroViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new KhodroViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the KhodroViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the KhodroViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected KhodroViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Khodros, x => x.Name)
        {
        }

        /// <summary>
        /// The view model for the KhodroProductsRelation detail collection.
        /// </summary>
        public CollectionViewModel<KhodroProductRelation, int, IMyContextCFUnitOfWork> KhodroProductsRelationDetails
        {
            get { return GetDetailsCollectionViewModel((KhodroViewModel x) => x.KhodroProductsRelationDetails, x => x.KhodroProductRelation, x => x.KhodroId, (x, key) => x.KhodroId = key); }
        }
    }
}

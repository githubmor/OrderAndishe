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
    /// Represents the single KhodroProductRelation object view model.
    /// </summary>
    public partial class KhodroProductRelationViewModel : SingleObjectViewModel<KhodroProductRelation, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of KhodroProductRelationViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static KhodroProductRelationViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new KhodroProductRelationViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the KhodroProductRelationViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the KhodroProductRelationViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected KhodroProductRelationViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.KhodroProductRelation, x => x.Id)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Khodros for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Khodro> LookUpKhodros
        {
            get { return GetLookUpEntitiesViewModel((KhodroProductRelationViewModel x) => x.LookUpKhodros, x => x.Khodros); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Products for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Product> LookUpProducts
        {
            get { return GetLookUpEntitiesViewModel((KhodroProductRelationViewModel x) => x.LookUpProducts, x => x.Products); }
        }
    }
}

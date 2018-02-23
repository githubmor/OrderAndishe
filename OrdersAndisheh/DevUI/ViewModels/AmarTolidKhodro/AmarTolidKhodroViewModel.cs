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
    /// Represents the single AmarTolidKhodro object view model.
    /// </summary>
    public partial class AmarTolidKhodroViewModel : SingleObjectViewModel<AmarTolidKhodro, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of AmarTolidKhodroViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static AmarTolidKhodroViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new AmarTolidKhodroViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the AmarTolidKhodroViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the AmarTolidKhodroViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected AmarTolidKhodroViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.AmarTolidKhodros, x => x.Tarikh)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Khodros for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Khodro> LookUpKhodros
        {
            get { return GetLookUpEntitiesViewModel((AmarTolidKhodroViewModel x) => x.LookUpKhodros, x => x.Khodros); }
        }
    }
}

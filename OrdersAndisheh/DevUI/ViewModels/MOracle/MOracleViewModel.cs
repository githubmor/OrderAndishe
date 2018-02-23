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
    /// Represents the single MOracle object view model.
    /// </summary>
    public partial class MOracleViewModel : SingleObjectViewModel<MOracle, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of MOracleViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static MOracleViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new MOracleViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the MOracleViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the MOracleViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected MOracleViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.MOracles, x => x.Descrioption)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of OrderDetails for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<OrderDetail> LookUpOrderDetails
        {
            get { return GetLookUpEntitiesViewModel((MOracleViewModel x) => x.LookUpOrderDetails, x => x.OrderDetails); }
        }
    }
}

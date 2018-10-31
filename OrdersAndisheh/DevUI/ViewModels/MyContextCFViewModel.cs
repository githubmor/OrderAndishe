using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using OrdersAndisheh.Common.DataModel;
using OrdersAndisheh.Common.ViewModel;
using OrdersAndisheh.DevUI.MyContextCFDataModel;
using OrdersAndisheh.DBL;

namespace OrdersAndisheh.DevUI.ViewModels
{
    /// <summary>
    /// Represents the root POCO view model for the MyContextCF data model.
    /// </summary>
    public partial class MyContextCFViewModel : DocumentsViewModel<MyContextCFModuleDescription, IMyContextCFUnitOfWork>
    {

        const string TablesGroup = "Tables";

        const string ViewsGroup = "Views";

        /// <summary>
        /// Creates a new instance of MyContextCFViewModel as a POCO view model.
        /// </summary>
        public static MyContextCFViewModel Create()
        {
            return ViewModelSource.Create(() => new MyContextCFViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the MyContextCFViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the MyContextCFViewModel type without the POCO proxy factory.
        /// </summary>
        protected MyContextCFViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory())
        {
        }

        protected override MyContextCFModuleDescription[] CreateModules()
        {
            return new MyContextCFModuleDescription[] {
                new MyContextCFModuleDescription("Amar Tolid Khodros", "AmarTolidKhodroCollectionView", TablesGroup),
                new MyContextCFModuleDescription("Khodros", "KhodroCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Khodros)),
                new MyContextCFModuleDescription("Khodro Product Relation", "KhodroProductRelationCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.KhodroProductRelation)),
                new MyContextCFModuleDescription("Products", "ProductCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Products)),
                new MyContextCFModuleDescription("Amount", "AmountCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Amount)),
                new MyContextCFModuleDescription("Bastes", "BasteCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Bastes)),
                new MyContextCFModuleDescription("Bazress", "BazresCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Bazress)),
                new MyContextCFModuleDescription("Oracle Relation", "OracleRelationCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.OracleRelation)),
                new MyContextCFModuleDescription("Customers", "CustomerCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Customers)),
                new MyContextCFModuleDescription("Order Details", "OrderDetailCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.OrderDetails)),
                new MyContextCFModuleDescription("Drivers", "DriverCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Drivers)),
                new MyContextCFModuleDescription("Driver Work", "DriverWorkCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.DriverWork)),
                new MyContextCFModuleDescription("Orders", "OrderCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Orders)),
                new MyContextCFModuleDescription("Temp Driver", "TempDriverCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.TempDriver)),
                new MyContextCFModuleDescription("MOracles", "MOracleCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.MOracles)),
                new MyContextCFModuleDescription("Customer Product Relations", "CustomerProductRelationCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.CustomerProductRelations)),
                new MyContextCFModuleDescription("Pallets", "PalletCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Pallets)),
                new MyContextCFModuleDescription("Taraf", "TarafCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Taraf)),
			};
        }



    }

    public partial class MyContextCFModuleDescription : ModuleDescription<MyContextCFModuleDescription>
    {
        public MyContextCFModuleDescription(string title, string documentType, string group, Func<MyContextCFModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory)
        {
        }
    }
}
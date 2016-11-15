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

        const string TablesGroup = "داده ها";

        const string ViewsGroup = "Views";
        INavigationService NavigationService { get { return this.GetService<INavigationService>(); } }

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
                new MyContextCFModuleDescription("بازرسان", "BazresCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Bazress)),
                new MyContextCFModuleDescription("کالا ها", "ProductCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Products)),
                new MyContextCFModuleDescription("مقاصد", "CustomerCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Customers)),
                new MyContextCFModuleDescription("ارتباط مشتری کالا", "CustomerProductRelationCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.CustomerProductRelations)),
                new MyContextCFModuleDescription("رانندگان", "DriverCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Drivers)),
                new MyContextCFModuleDescription("پالت ها", "PalletCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Pallets)),
			};
        }



        protected override void OnActiveModuleChanged(MyContextCFModuleDescription oldModule)
        {
            if (ActiveModule != null && NavigationService != null)
            {
                NavigationService.ClearNavigationHistory();
            }
            base.OnActiveModuleChanged(oldModule);
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
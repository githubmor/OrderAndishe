﻿using System;
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
    /// Represents the single Product object view model.
    /// </summary>
    public partial class ProductViewModel : SingleObjectViewModel<Product, int, IMyContextCFUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of ProductViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static ProductViewModel Create(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new ProductViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the ProductViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the ProductViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected ProductViewModel(IUnitOfWorkFactory<IMyContextCFUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Products, x => x.Name)
        {
        }

        /// <summary>
        /// The view model that contains a look-up collection of Amount for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Amount> LookUpAmount
        {
            get { return GetLookUpEntitiesViewModel((ProductViewModel x) => x.LookUpAmount, x => x.Amount); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Bastes for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Baste> LookUpBastes
        {
            get { return GetLookUpEntitiesViewModel((ProductViewModel x) => x.LookUpBastes, x => x.Bastes); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Bazress for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Bazres> LookUpBazress
        {
            get { return GetLookUpEntitiesViewModel((ProductViewModel x) => x.LookUpBazress, x => x.Bazress); }
        }

        /// <summary>
        /// The view model that contains a look-up collection of Pallets for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Pallet> LookUpPallets
        {
            get { return GetLookUpEntitiesViewModel((ProductViewModel x) => x.LookUpPallets, x => x.Pallets); }
        }

        /// <summary>
        /// The view model for the ProductKhodrosRelation detail collection.
        /// </summary>
        public CollectionViewModel<KhodroProductRelation, int, IMyContextCFUnitOfWork> ProductKhodrosRelationDetails
        {
            get { return GetDetailsCollectionViewModel((ProductViewModel x) => x.ProductKhodrosRelationDetails, x => x.KhodroProductRelation, x => x.ProductId, (x, key) => x.ProductId = key); }
        }

        /// <summary>
        /// The view model for the ProductOrderDetails detail collection.
        /// </summary>
        public CollectionViewModel<OrderDetail, int, IMyContextCFUnitOfWork> ProductOrderDetailsDetails
        {
            get { return GetDetailsCollectionViewModel((ProductViewModel x) => x.ProductOrderDetailsDetails, x => x.OrderDetails, x => x.ProductId, (x, key) => x.ProductId = key); }
        }

        /// <summary>
        /// The view model for the ProductRelations detail collection.
        /// </summary>
        public CollectionViewModel<CustomerProductRelation, int, IMyContextCFUnitOfWork> ProductRelationsDetails
        {
            get { return GetDetailsCollectionViewModel((ProductViewModel x) => x.ProductRelationsDetails, x => x.CustomerProductRelations, x => x.ProductId, (x, key) => x.ProductId = key); }
        }

        /// <summary>
        /// The view model for the ProductOracleRelations detail collection.
        /// </summary>
        public CollectionViewModel<OracleRelation, int, IMyContextCFUnitOfWork> ProductOracleRelationsDetails
        {
            get { return GetDetailsCollectionViewModel((ProductViewModel x) => x.ProductOracleRelationsDetails, x => x.OracleRelation, x => x.ProductId, (x, key) => x.ProductId = key); }
        }
    }
}

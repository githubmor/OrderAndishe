﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BL
{
    using OrdersAndisheh.DBL;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;

	public class Sefaresh
	{
        public Sefaresh()
        {
            Items = new ObservableCollection<ItemSefaresh>();
            Order = new Order();
            AgarShodItems = new List<ItemSefaresh>();
            UsualItems = new List<ItemSefaresh>();
            ForiItems = new List<ItemSefaresh>();
            GovahiItems = new List<ItemSefaresh>();
            Items.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var item in e.NewItems)
                    {
                        ItemSefaresh d = (ItemSefaresh)item;
                        Order.OrderDetails.Add(d.OrderDetail);
                    }
                    
                }
            };
        }

        public Sefaresh(Order order, List<OrderDetail> orderDetails):this()
        {
            if (orderDetails == null)
            {
                throw new ApplicationException("اطلاعات آیتم سفارش وجود ندارد");
            }
            this.Order = order;
            foreach (OrderDetail item in orderDetails)
            {
                Items.Add(new ItemSefaresh(item));
            }
        }

        
		public string Tarikh
		{
            get { return Order.Tarikh ;}
            set
            {
                DateTime j;
                if (!DateTime.TryParse(value,out j))
                {
                    throw new ApplicationException("فرمت تاریخ وارد شده صحیح نمی باشد !");
                };
                Order.Tarikh = value;
            }
		}

		public string Description
		{
			get { return Order.Description; }
            set { Order.Description = value; }
        }

        public int SefareshId
        {
            get { return Order.Id; }
        }
		
        public Order Order
        {
            get;
            private set;
        }


        private ObservableCollection<ItemSefaresh> items;
        

	    public ObservableCollection<ItemSefaresh> Items
	    {
		    get { return items;}
		    set { 
                items = value;
                foreach (var s in value)
                {
                    s.OrderDetail.OrderId = Order.Id;
                    Order.OrderDetails.Add(s.OrderDetail);
                }
                //foreach (var item in items)
                //{
                //    switch (item.ItemKind)
                //    {
                //        case ItemType.Fori:
                //            ForiItems.Add(item);
                //            break;
                //        case ItemType.Govahi:
                //            GovahiItems.Add(item);
                //            break;
                //        case ItemType.AgharAmadeShod:
                //            AgarShodItems.Add(item);
                //            break;
                //        case ItemType.Usual:
                //            UsualItems.Add(item);
                //            break;
                //        default:
                //            break;
                //    }
                //}
            }
	    }

        

        

		public virtual List<ItemSefaresh> AgarShodItems
		{
			get;
            private set;
		}

        public virtual List<ItemSefaresh> ForiItems 
		{
			get;
            private set;
		}

        public virtual List<ItemSefaresh> GovahiItems 
		{
			get;
            private set;
		}

        public virtual List<ItemSefaresh> UsualItems 
		{
			get;
            private set;
		}




        public bool Accepted 
        {
            get { return Order.Accepted; }
            set { Order.Accepted = value; } 
        }
    }
}


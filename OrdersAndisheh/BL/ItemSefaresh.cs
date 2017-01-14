﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace BL
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using OrdersAndisheh.DBL;
    using System.ComponentModel;

    public class ItemSefaresh : INotifyPropertyChanged
	{
        public ItemSefaresh(Product p)
        {
            if (p==null)
            {
                throw new ApplicationException("کالا وجود ندارد !"); 
            }
            if (p.Code==null&p.Name==null)
            {
                throw new ApplicationException("نام کالا یا کد کالا مشخص نمی باشد !"); 
            }
            OrderDetail = new OrderDetail();
            Product = p;
            PalletCount = PalletCalCulate();
            ItemKind = (byte) ItemType.عادی;

        }

        public ItemSefaresh(OrderDetail od):this(od.Product)
        {
            if (od==null)
            {
                throw new ApplicationException("آیتم سفارش از دیتابیس وجود ندارد !");
            }
            
            this.OrderDetail = od;
        }

        

        public string Ranande
        {
            get { return (Driver==null?"":Driver.Name); }
        }

        public string Maghsad
        {
            get { return (Customer==null?"":Customer.Name); }
        }

        public string Kala
        {
            get { return Product.Name; }
        }

        public string BazresName
        {
            get 
            {
                if (Product.Bazre==null)
                {
                    throw new ApplicationException("باید بازرس های کالا ها از دیتابیس لود شود");
                }
                return Product.Bazre.Name; 
            }
        }

        public string CodeKala
        {
            get { return Product.Code; }
        }

       
		public OrderDetail OrderDetail
		{
			get;
			set;
		}

		public Product Product
		{
            get { return OrderDetail.Product; }
            set 
            { 
                OrderDetail.Product = value;
                PalletCount = PalletCalCulate();
                NotifyPropertyChanged("Kala");
                NotifyPropertyChanged("CodeKala");
            }
		}

		public Driver Driver
		{
            get { return OrderDetail.Driver; }
            set { OrderDetail.Driver = value;
            NotifyPropertyChanged("Ranande");
            }
		}

		public Customer Customer
		{
            get { return OrderDetail.Customer; }
            set 
            { 
                OrderDetail.Customer = value;
                NotifyPropertyChanged("Maghsad");
            }
		}
        private bool isSelacted;
        public bool IsSelected //{ get; set; }
        {
            get { return isSelacted; }
            set 
            { 
                isSelacted = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

		public short Tedad
		{
            get { return OrderDetail.Tedad; }
            set 
            { 

                OrderDetail.Tedad = value;
                PalletCount = PalletCalCulate();
                NotifyPropertyChanged("Tedad");
                NotifyPropertyChanged("Vazn");
                NotifyPropertyChanged("Darkhast");
            }
		}
        public string Des
        {
            get { return OrderDetail.Description; }
            set
            {
                OrderDetail.Description = value;
                NotifyPropertyChanged("Des");
            }
        }

		public short TahvilFrosh
		{
            get { return OrderDetail.TahvilForosh; }
            set { OrderDetail.TahvilForosh = value; }
		}

        public int AsnNumber
        {
            get { return OrderDetail.AsnNumber; }
            set { OrderDetail.AsnNumber = value; }
        }

        
		public byte ItemKind
		{
            get { return OrderDetail.ItemType; }
            set { OrderDetail.ItemType = value; }
		}


        public string Karton //{ get; set; }
        {
            get { return KartonCalCulate(); }
        }

        private string KartonCalCulate()
        {

            if (Product.TedadDarSabad>0 && Tedad % Product.TedadDarSabad > 0)
            {
                return ((Tedad / Product.TedadDarSabad) > 0 ?
                    (Tedad / Product.TedadDarSabad).ToString()
                    : "") +"[" + Tedad % Product.TedadDarSabad + "]";
            }
            else if (Product.TedadDarPallet > 0 && Tedad % Product.TedadDarPallet == 0)
            {
                return "";
            }
            else if (Product.TedadDarSabad > 0)
            {
                return (Tedad / Product.TedadDarSabad).ToString();
            }
            else
            {
                return "";
            }
            
        }

        
        public int PalletCount //{ get; set; }
        {
            get { return OrderDetail.TedadPallet; }
            set 
            { 
                OrderDetail.TedadPallet = value;
                NotifyPropertyChanged("PalletCount");
                NotifyPropertyChanged("Vazn");
            }
        }

        private int PalletCalCulate()
        {
            if (Tedad > Product.TedadDarPallet)
            {
                if (Product.TedadDarPallet > 0)
                {
                    if (Tedad % Product.TedadDarPallet == 0)
                    {
                        return (Tedad /(int) Product.TedadDarPallet);
                    }
                    else
                    {
                        return ((Tedad / (int)Product.TedadDarPallet) + 1);
                    }
                }
                else
                {
                    return 1;
                }
                
            }
            else
            {
                return 1;
            }
        }
        public int Vazn //{ get; set; }
        {
            get { return VaznCalCulate(); }
        }

        private int VaznCalCulate()
        {
            if (Product.Weight!=null && Product.Weight>0)
            {
                if (Product.TedadDarPallet > 0)
                {
                    double OneProductWeight;
                    int p = (int)Product.Weight - (int)Product.Pallet.Vazn;
                    OneProductWeight = (double)(p / (double)Product.TedadDarPallet);

                    return (int)(Tedad * OneProductWeight) + (int)(PalletCount * Product.Pallet.Vazn);

                }
                else
                {
                    return 0;
                }
                
                

                //if (Product.TedadDarPallet > 0 && (Tedad % Product.TedadDarPallet) == 0 & Product.TedadDarPallet != null)
                //{
                //    return (int)((Tedad / Product.TedadDarPallet) * Product.Weight);
                //}
                //else
                //{
                    

                //    return (int)(Tedad * OneProductWeight) + PalletCount * (Product.Pallet.Vazn != null ? (int)Product.Pallet.Vazn : 0);
                //}
            }
            else
            {
                return 0;
            }
            //if (PalletCount > 0 )
            //{
            //    return (int)(PalletCount * (Product.Weight != null ? (int)Product.Weight : 0));
            //}
            //else//بدون پالت
            //{
            //    if (Product.TedadDarPallet > 0 & Product.TedadDarPallet != null)
            //    {
            //        double OneProductWeight;
            //        if (Product.Weight != null)
            //        {
            //            int p = (int)Product.Weight - (int)Product.Pallet.Vazn;

            //            OneProductWeight = (double)(p / (double)Product.TedadDarPallet);
            //        }
            //        else
            //        {
            //            OneProductWeight = 0;
            //        }

            //        return (int)(Tedad * OneProductWeight); //+ PalletCount * (Product.Pallet.Vazn != null ? (int)Product.Pallet.Vazn : 0);
            //    }
            //    else
            //    {
            //        return (Product.Weight!=null?(int)Product.Weight:0);
            //    }

            //}

            //if (Product.TedadDarPallet > 0 && (Tedad % Product.TedadDarPallet) == 0 & Product.TedadDarPallet != null)
            //{
            //    return (int)((Tedad / Product.TedadDarPallet) * Product.Weight);
            //}
            //else
            //{
            //    double OneProductWeight;
            //    if (Product.Weight != null)
            //    {
            //        int p = (int)Product.Weight - (int)Product.Pallet.Vazn;

            //        OneProductWeight = (double)(p / (double)Product.TedadDarPallet);
            //    }
            //    else
            //    {
            //        OneProductWeight = 0;
            //    }

            //    return (int)(Tedad * OneProductWeight) + PalletCount * (Product.Pallet.Vazn != null ? (int)Product.Pallet.Vazn : 0);
            //}
        }
        public bool IsImenKala 
        {
            get { return Product.IsImenKala; }
            set { Product.IsImenKala = value; } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}


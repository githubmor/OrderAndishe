namespace BL
{
    using OrdersAndisheh.DBL;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;

    public class Sefaresh : INotifyPropertyChanged
    {
        public Sefaresh()
        {
            Items = new ObservableCollection<ItemSefaresh>();
            Order = new Order();

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
                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var item in e.OldItems)
                    {
                        ItemSefaresh d = (ItemSefaresh)item;
                        Order.OrderDetails.Remove(d.OrderDetail);
                    }
                }
            };
        }

        public Sefaresh(Order order, List<OrderDetail> orderDetails)
            : this()
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
            get { return Order.Tarikh; }
            set
            {
                //PersianDateTime j;
                //if (!PersianDateTime.Parse(value, out j))
                //{
                //    throw new ApplicationException("فرمت تاریخ وارد شده صحیح نمی باشد !");
                //};
                Order.Tarikh = value;
                NotifyPropertyChanged("Tarikh");
            }
        }

        public string Description
        {
            get { return Order.Description; }
            set
            {
                Order.Description = value;
                NotifyPropertyChanged("Description");
            }
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
            get { return items; }
            set
            {
                items = value;
                foreach (var s in value)
                {
                    s.OrderDetail.OrderId = Order.Id;
                    Order.OrderDetails.Add(s.OrderDetail);
                }
                NotifyPropertyChanged("Items");
               
            }
        }

        
        public bool Accepted
        {
            get { return Order.Accepted; }
            set { Order.Accepted = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        internal string GetPalletsSummery()
        {
            string re = "";
            
            var t = Items.Select(p => p.Product.Pallet.Name).Distinct();

            foreach (var item in t)
            {
                int sum = 0;

                foreach (var d in Items.Where(p => p.Product.Pallet.Name == item).Select(o => o.PalletCount))
                {
                    sum += d;
                }
                re += item + " = " + sum + "   -   ";
            }
            return re;
        }

        internal string GetDriversSummery()
        {
            var drss = Items.Select(o => o.Ranande).Distinct();
            string ft = "";
            foreach (var item in drss)
            {
                ft += item + " : ";
                int sum = 0;
                var yu = Items.Where(ik => ik.Ranande == item).ToList();
                foreach (var wew in yu)
                {
                    sum += wew.Vazn;
                }
                ft += sum + " - ";
            }
            return ft;
        }

        internal string GetMaghsadSummery()
        {
            var drss = Items.Select(o => o.Maghsad).Distinct();
            string ft = "";
            foreach (var item in drss)
            {
                ft += item + " : ";
                int sum = 0;
                var yu = Items.Where(ik => ik.Maghsad == item).ToList();
                foreach (var wew in yu)
                {
                    sum += wew.Vazn;
                }
                ft += sum + " - ";
            }
            return ft;
        }

        internal void ChangeDriverCustomer(Customer SelectedDestenation, Driver SelectedDriver)
        {
            string lastMaghsadChanged = "";
            string lastRanandeChanged = "";
            string _newMaghsad = (SelectedDestenation != null ? SelectedDestenation.Name : "تهی");
            string _newRanande = (SelectedDriver != null ? SelectedDriver.Name : "تهی");
            foreach (var item in Items.Where(p => p.IsSelected))
            {
                if (string.IsNullOrEmpty(item.Maghsad))
                {
                    item.Customer = SelectedDestenation;
                }
                else if (item.Maghsad != _newMaghsad)
                {
                    //اگر بازم میخواد همون سوال قبلی رو بپرسه دیگه تکرار نمیشه
                    if (item.Maghsad != lastMaghsadChanged)
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید مقصد را از " +
                            item.Maghsad + " به " + _newMaghsad + " تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            lastMaghsadChanged = item.Maghsad;
                            item.Customer = SelectedDestenation;
                        }
                    }
                    else
                    {
                        lastMaghsadChanged = item.Maghsad;
                        item.Customer = SelectedDestenation;
                    }
                }

                if (string.IsNullOrEmpty(item.Ranande))
                {
                    item.Driver = SelectedDriver;
                }
                else if (item.Ranande != _newRanande)
                {
                    if (item.Ranande != lastRanandeChanged)
                    {
                        DialogResult result = MessageBox.Show("آیا میخواهید راننده را از " +
                            item.Ranande + " به " + _newRanande + " تغییر دهید ؟",
                            "اخطار", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            lastRanandeChanged = item.Ranande;
                            item.Driver = SelectedDriver;
                        }
                    }
                    else
                    {
                        lastRanandeChanged = item.Ranande;
                        item.Driver = SelectedDriver;
                    }
                }
            }
        }
    }
}
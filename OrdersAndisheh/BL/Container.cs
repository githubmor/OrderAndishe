using BL;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class Container : INotifyPropertyChanged
    {
        //اینجا یه کانتینر شامل یک یا یکسری اقلام میشه
        //که تعداد جایگاهش بیشتر از 8 نیست اگر وزنش بیشتر از تک نشه 
        //و یه سری پراپرتی برای نمایش وزن و جایگاه و غیره
        // در ضمن میتونه اینجا بگه راننده در چه حده خاوری و غیره

        public Container()
        {
            items = new List<ItemSefaresh>();
            driver = new Driver();

        }

        public Container(List<ItemSefaresh> items)
            : this()
        {
            this.items = items;
        }
       
        public Container(List<ItemSefaresh> items,Driver driver)
            :this()
        {
            this.items = items;
            this.driver = driver;
        }

        private List<ItemSefaresh> _items;

        public List<ItemSefaresh> items
        {
            get { return _items; }
            set 
            { 
                _items = value;
                NotifyPropertyChanged("items");
            }
        }

        private Driver _driver;

        public Driver driver
        {
            get { return _driver; }
            set 
            { 
                _driver = value;
                NotifyPropertyChanged("driver");
            }
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

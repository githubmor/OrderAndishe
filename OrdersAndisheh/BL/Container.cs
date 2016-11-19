using BL;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class Container
    {
       
        public Container(List<ItemSefaresh> items,Driver driver)
        {
            this.items = items;
            this.driver = driver;
        }

        private List<ItemSefaresh> _items;

        public List<ItemSefaresh> items
        {
            get { return _items; }
            set { _items = value; }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
        

    }
}

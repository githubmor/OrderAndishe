using BL;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class ErsalItem : ItemSefaresh
    {
        public ErsalItem(OrderDetail d ,string tarikh) : base(d)
        {
            this.Tarikh = tarikh;
        }
        public string Tarikh { get; set; }
        //{
        //    get { return this.; }
        //}

    }
}

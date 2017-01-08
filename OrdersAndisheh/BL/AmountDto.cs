using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class AmountDto 
    {
        public Amount amount;
        public AmountDto(Amount a)
        {
            this.amount = a;
        }
        public string KalaName
        {
            get { return this.amount.Product.Name; }
        }
        public int LastAmount
        {
            get { return this.amount.LastAmount; }
        }
        public string CodeKala
        {
            get { return this.amount.Product.Code; }
        }
        
    }
}

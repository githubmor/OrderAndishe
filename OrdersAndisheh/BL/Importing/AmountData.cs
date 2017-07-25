using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public class AmountData : IImportData
    {

        public ImportDataKind FileKind
        {
            get
            {
                return ImportDataKind.GoodAmount;
            }
        }



        public string CodeKala { get; set; }

        public string KalaName { get; set; }

        public double LastAmount { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public class TahvilFroshData : IImportData
    {

        public ImportDataKind FileKind
        {
            get
            {
                return ImportDataKind.tahvilFrosh;
            }
        }

        public string KalaName { get; set; }

        public string CodeKala { get; set; }

        public string TarafeMoghabel { get; set; }

        public short TahvilFroshNum { get; set; }

        public decimal Tedad { get; set; }

        public string TarikhSanad { get; set; }

        public bool IsOk { get; set; }

    }
}

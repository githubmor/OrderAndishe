using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public interface IImportData
    {
        string KalaName { get; set; }
        ImportDataKind FileKind { get; }

        
    }
}

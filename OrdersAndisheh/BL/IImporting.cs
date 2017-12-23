using ImportingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public interface IImporting
    {
        void SaveData(List<ImportData> data);
    }
}

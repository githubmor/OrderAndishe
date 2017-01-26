using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.ImportFromExcel
{
    public interface ExcelImporter
    {
        object GetData(string filePath);
    }
}

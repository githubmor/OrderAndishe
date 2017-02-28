using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public interface IImporter
    {
        public string[,] GetData(string filePath);
    }
}

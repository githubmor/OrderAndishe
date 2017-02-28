using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public abstract class AbstractImporter
    {
        protected IImporter importer;
        public AbstractImporter(IImporter importer)
        {
            this.importer = importer;
        }

        public abstract void GetData(string path);
    }
}

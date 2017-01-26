using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Asn
{
    public class AsnChecker
    {
        private List<IAsn> Asn;
        private Sefaresh sefaresh;
        public AsnChecker(List<IAsn> ASN,Sefaresh sefaresh)
        {
            this.Asn = ASN;
            this.sefaresh = sefaresh;
        }

        public List<string> CheckAsns()
        {

            return null;
        }


    }
}

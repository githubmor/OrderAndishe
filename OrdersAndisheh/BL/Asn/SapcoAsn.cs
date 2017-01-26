using System.Collections.Generic;
using System.Linq;

namespace OrdersAndisheh.BL.Asn
{
    public class SapcoAsn : IAsn
    {
        private AsnHeader header;
        private List<AsnRow> items;
        public SapcoAsn(AsnHeader header,List<AsnRow> items)
        {
            this.header = header;
            this.items = items;
        }

        public string RanandeName
        {
            get { return header.DriverName; }
        }
        public string Tarikh
        {
            get { return header.Tarikh; }
        }
        public string AsnNumber
        {
            get { return header.AsnNumber; }
        }
        public List<string> GoodFaniCodes
        {
            get 
            {
                List<string> t = new List<string>();
                foreach (var item in items)
                {
                    t.Add(item.FaniCode);
                }
                return t;
            }
        }

        public int GetTedad(string fanicode)
        {
            return items.Where(p => p.FaniCode == fanicode).FirstOrDefault().Tedad;
        }
        public int GetAsnAnabrNum(string fanicode)
        {
            return header.AnbarNumber;
        }

        //public void SetIsOk(string fanicode)
        //{
        //    items.Where(p => p.FaniCode == fanicode).FirstOrDefault().IsOk = true;
        //}

        //public List<string> GetNotOkFaniCodes()
        //{
        //    return items.Where(p => !p.IsOk).Select(p => p.FaniCode).ToList();
        //}

        //public bool IsDriverOk(Driver driver)
        //{
        //    return header.DriverName == driver.Name |
        //        header.DriverPelak == driver.Pelak |
        //        header.DriverTel == driver.Tel1;
        //}
    }
}

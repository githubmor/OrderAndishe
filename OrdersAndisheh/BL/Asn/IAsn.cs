using System;
using System.Collections.Generic;
namespace OrdersAndisheh.BL.Asn
{
    public interface IAsn
    {
        string AsnNumber { get; }
        int GetTedad(string fanicode);
        List<string> GoodFaniCodes { get; }
        string RanandeName { get; }
        string Tarikh { get; }
    }
}

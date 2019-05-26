using System;
namespace OrdersAndisheh.BL
{
    interface ICheckList
    {
        string Address { get; }
        //string BasteBandi { get; }
        string CarKind { get; }
        string CodeJens { get; }
        string CodeKala { get; }
        string FaniCode { get; }
        string Govahi { get; }
        string Moshtari { get; }
        string NameKala { get; }
        //string PalletCount { get; }
        string Pelak { get; }
        string RanandeName { get; }
        string SherkatName { get; }
        string TahvilFrosh { get; }
        string Tarikh { get; set; }
        //string Tedad { get; }
        //string TedadDarHarPallet { get; }
    }
}

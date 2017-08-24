using System;
namespace OrdersAndisheh.BL
{
    public interface IReportRow
    {
        //bool IsCustomerChanged { get; set; }
        //bool IsDriverChanged { get; set; }
        //bool IsKalaChanged { get; set; }
        //bool IsTedadChanged { get; set; }
        string Kala { get; set; }
        string Karton { get; set; }
        string Maghsad { get; set; }
        string Pallet { get; set; }
        string Pelak { get; set; }
        int Position { get; set; }
        string Ranande { get; set; }
        string Tedad { get; set; }
        string Vazn { get; set; }
    }
}

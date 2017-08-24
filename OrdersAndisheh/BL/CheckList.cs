using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class CheckList : OrdersAndisheh.BL.ICheckList
    {
        private ItemSefaresh Item;
        public CheckList(ItemSefaresh item,string Tarikh)
        {
            this.Item = item;
            this.Tarikh = Tarikh;
        }

        

        public string NameKala
        {
            get { return Item.Kala; }
        }
        public string CodeKala
        {
            get { return Item.CodeKala; }
        }
        public string FaniCode
        {
            get { return Item.Product.FaniCode; }
        }
        public string Tarikh { get; set; }
        public string SherkatName
        {
            get { return (Item.IsImenKala ? "شرکت ایمن سازان خودرو اندیشه" : "شرکت اندیشه ایمنی خودرو"); }
        }
        public string CodeJens
        {
            get { return Item.Product.CodeJense; }
        }
        public string Govahi
        {
            get { return ""; }
        }
        public string Moshtari
        {
            get { return Item.Maghsad; }
        }
        public string Address
        {
            get { return Item.Maghsad; }
        }
        public string TahvilFrosh
        {
            get { return Item.TahvilFrosh.ToString(); }
        }
        public string Tedad
        {
            get { return Item.Tedad.ToString(); }
        }
        public string TedadDarHarPallet
        {
            get { return Item.Product.TedadDarPallet.ToString(); }
        }
        
        public string PalletCount
        {
            get { return Item.PalletCount.ToString(); }
        }
        public string BasteBandi
        {
            get { return Item.Product.BasteBandi; }
        }
        public string RanandeName
        {
            get { return Item.Ranande; }
        }
        public string CarKind
        {
            get { return Item.Driver.Car; }
        }
        public string Pelak
        {
            get { return Item.Driver.Pelak; }
        }
    }
}

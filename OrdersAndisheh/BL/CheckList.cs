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
        private List<ItemSefaresh> Item;
        public CheckList(List<ItemSefaresh> items,string Tarikh)
        {
            this.Item = items;
            this.Tarikh = Tarikh;
        }

        

        public string NameKala
        {
            get { return joinToString(Item.Select(p => p.Kala).ToList()); }
        }
        public string CodeKala
        {
            get { return joinToString(Item.Select(p => p.CodeKala).ToList()); }
        }
        public string FaniCode
        {
            get { return joinToString(Item.Select(p => p.Product.FaniCode).ToList()); }
        }
        public string Tarikh { get; set; }
        public string SherkatName
        {
            get { return (Item.First().IsImenKala ? "شرکت ایمن سازان خودرو اندیشه" : "شرکت اندیشه ایمنی خودرو"); }
        }
        public string CodeJens
        {
            get { return joinToString(Item.Select(p => p.Product.CodeJense).ToList()); }
        }
        public string Govahi
        {
            get { return ""; }
        }
        public string Moshtari
        {
            get { return joinToString(Item.Select(p => p.Maghsad).Distinct().ToList()); }
        }
        public string Address
        {
            get { return joinToString(Item.Select(p => p.Maghsad).Distinct().ToList()); }
        }
        public string TahvilFrosh
        {
            get { return joinToString(Item.Select(p => p.TahvilFrosh.ToString()).Distinct().ToList()); }
        }

        

        
        //public string Tedad
        //{
        //    get { return jointoString(Item.Select(p => p.Tedad.ToString()).ToArray()); }
        //}
        //public string TedadDarHarPallet
        //{
        //    get { return jointoString(Item.Select(p => p.Product.TedadDarPallet.ToString()).ToArray()); }
        //}
        
        //public string PalletCount
        //{
        //    get { return string.Join(",", Item.Select(p => p.PalletCount.ToString()).ToList()); }
        //}
        //public string BasteBandi
        //{
        //    get { return Item.Product.BasteBandi; }
        //}
        public string RanandeName
        {
            get { return Item.First().Ranande; }
        }
        public string CarKind
        {
            get { return Item.First().Driver.Car; }
        }
        public string Pelak
        {
            get { return Item.First().Driver.Pelak; }
        }

        private string joinToString(List<String> names)
        {
            return string.Join(" , ", names);
        }
    }
}

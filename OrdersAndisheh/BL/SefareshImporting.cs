using BL;
using ImportingLib;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;

namespace OrdersAndisheh.BL
{
    public class SefareshImporting : IImporting
    {
        private SefareshService Service;
        public SefareshImporting(SefareshService service)
        {
            this.Service = service;
        }
        public string Tarikh { get; set; }
        public void SaveData(List<ImportData> data)
        {
            if (string.IsNullOrEmpty(Tarikh))
            {
                throw new NullReferenceException("تاریخ سفارش را مشخص کنید");
            }

            Sefaresh sefaresh = new Sefaresh();
            sefaresh.Tarikh = Tarikh;

            foreach (var detail in data)
            {
                Customer c = Service.GetCustomer(detail.Maghsad);
                
                Driver d = Service.GetDriver(detail.DriverName);
                
                Product p = Service.GetProduct(detail.Codekala);

                short tedad = short.Parse(detail.Tedad);

                //چون شماره تحويل فروش ميتواند اختياري باشد
                short tahvil = (string.IsNullOrEmpty(detail.SanadNumber)?short.Parse("0"):short.Parse(detail.SanadNumber));

                ItemSefaresh it = new ItemSefaresh(p);
                it.Customer = c;
                it.Driver = d;
                it.Tedad = tedad;
                it.TahvilFrosh = tahvil;

                sefaresh.Items.Add(it);
            }

            Service.SaveSefaresh(sefaresh);

            Service.Save();
        }
    }
}

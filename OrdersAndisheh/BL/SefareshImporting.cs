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

                sefaresh.Items.Add(new ItemSefaresh(p){ Customer = c, Driver = d, 
                     Tedad = short.Parse(detail.Tedad),TahvilFrosh = short.Parse(detail.SanadNumber)
                });
            }

            Service.SaveSefaresh(sefaresh);

            Service.Save();
        }
    }
}

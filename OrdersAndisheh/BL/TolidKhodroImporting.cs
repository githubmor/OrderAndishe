using BL;
using ImportingLib;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersAndisheh.BL
{
    public class TolidKhodroImporting : IImporting
    {
        //private SefareshService Service;
        public TolidKhodroImporting()
        {
            //this.Service = service;
        }
        public string SalMah { get; set; }

        public void SaveData(List<ImportData> data)
        {
            if (string.IsNullOrEmpty(SalMah))
            {
                throw new NullReferenceException("سال و ماه را مشخص کنید");
            }

            MyContextCF db = new MyContextCF();

            List<AmarTolidKhodro> amars = new List<AmarTolidKhodro>();

            foreach (var item in data)
            {
                
                Khodro khodro = db.Khodros.SingleOrDefault(p => p.Name == item.Car);
                if (khodro!=null)
                {
                    amars.Add(new AmarTolidKhodro()
                    {
                        KhodroId = khodro.Id,
                        SalMah = SalMah,
                        TedadTolid = int.Parse(item.Tedad)
                    });

                }
                else
                {
                    throw new Exception("خودرويي با نام "+ item.Car + " پيدا نشد");
                }
            }

            db.AmarTolidKhodros.AddRange(amars);
            db.SaveChanges();

            
        }
    }
}

using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class ShakhesService
    {
        MyContextCF db;
        public ShakhesService()
        {
            db = new MyContextCF();
            
        }

        public List<string> LoadYears()
        {
            var y = db.Orders.GroupBy(p => p.Tarikh.Substring(0, 4)).Select(p => p.Key).ToList();

            return y;
        }

        public List<string> LoadMonth(string SelectedSal)
        {
            if (!string.IsNullOrEmpty(SelectedSal))
            {
                var y = db.Orders
                    .Where(p=>p.Tarikh.StartsWith(SelectedSal))
                    .GroupBy(p => p.Tarikh.Substring(5,2)).Select(p => p.Key).ToList();

                return y;
            }
            else
            {
                return new List<string>();
            }
            
        }

        public List<AmartolidKhodro> GetAmarTolid(string SelectedSal, string SelectedMah)
        {

            List<AmartolidKhodro> re = new List<AmartolidKhodro>();


            if (!string.IsNullOrEmpty(SelectedSal) & !string.IsNullOrEmpty(SelectedMah))
            {
                var kho = db.Khodros.Include("Sazandeh").ToList();

                foreach (var item in kho)
                {
                    re.Add(new AmartolidKhodro() { Sazandeh =(item.Sazandeh!=null?item.Sazandeh.Name:"نامشخص"), Khodro = item.Name, IdKhodro = item.Id, Tadad = 0 });
                }

                string rrrr = SelectedSal + SelectedMah;
                var dd = db.AmarTolidKhodros.Where(p => p.SalMah == rrrr).ToList();

                if (dd != null && dd.Count > 0)
                {
                    foreach (var item in dd)
                    {
                        re.Single(p => p.IdKhodro == item.KhodroId).Tadad = item.TedadTolid;
                    }
                }

            }

            return re;
        }


        public AmarErsals GetAmarErsal(string Sal, string Mah)
        {
            AmarErsals listAmarErsalMahsol_result = new AmarErsals();

            if (!string.IsNullOrEmpty(Sal) & !string.IsNullOrEmpty(Mah))
            {
                var product_with_khodro_relation = db.Products
                    .Include("KhodrosRelation").Include("KhodrosRelation.Khodro").Include("KhodrosRelation.Khodro.Sazandeh")
                    .Where(p=>p.KhodrosRelation.Count>0).ToList();

                foreach (var product in product_with_khodro_relation)
                {
                    listAmarErsalMahsol_result.Add(new AmarErsalMahsol() 
                    {
                        Kala = product.Name, 
                        IdKala = product.Id, 
                        Tadad = 0 ,
                        Sahm = 0 ,
                        Sazandeh =(product.KhodrosRelation.First().Khodro.Sazandeh!=null?
                        product.KhodrosRelation.First().Khodro.Sazandeh.Name:"نامشخص"),
                        Sherkat = (product.IsImenKala?"ايمن سازان":"انديشه")
                    });
                }

                string sal_mah = Sal+Mah;
                var orderDetail_in_sal_mah = db.OrderDetails.Include("Product").Include("Product.KhodrosRelation")
                    .Where(p => p.Order.Tarikh.StartsWith(Sal) & p.Order.Tarikh.Substring(5, 2) == Mah & p.Customer.IsTolidLine)
                    .ToList();

                var product_in_sal_mah = orderDetail_in_sal_mah
                    .GroupBy(p => p.Product).Select(b => 
                        new { 
                            id = b.Key.Id,
                            tedadErasal = orderDetail_in_sal_mah.Where(p=>p.ProductId==b.Key.Id).Sum(y=>y.Tedad),
                            khodros = b.Key.KhodrosRelation
                            })
                    .ToList();

                var amarTolid_in_sal_mah = db.AmarTolidKhodros.Where(p => p.SalMah == sal_mah).ToList();


                    if (product_in_sal_mah != null && product_in_sal_mah.Count > 0)
                    {
                        foreach (var product in product_in_sal_mah)
                        {
                            
                            var o = listAmarErsalMahsol_result.SingleOrDefault(p => p.IdKala == product.id);
                            if (o != null)
                            {
                                int khodroSum = 0;
                                int zaribSum = 0;
                                o.Tadad = product.tedadErasal;
                                if (amarTolid_in_sal_mah.Count>0)
                                {
                                    foreach (var tt in product.khodros)
                                    {
                                        khodroSum += amarTolid_in_sal_mah
                                            .SingleOrDefault(p => p.KhodroId == tt.KhodroId).TedadTolid;
                                        zaribSum += tt.Zarib;
                                    } 
                                }
                                int zarib_avg = zaribSum / product.khodros.Count;
                                if (khodroSum>0)
                                {
                                    o.Sahm = ((o.Tadad/zarib_avg)*100 / khodroSum); 
                                }
                            }
                        }
                    } 


            }

            return listAmarErsalMahsol_result;
        }
    }
}

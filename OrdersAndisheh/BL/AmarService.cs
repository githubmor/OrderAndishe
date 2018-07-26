using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class AmarService
    {
        MyContextCF db;
        public AmarService()
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

        public ObservableCollection<AmartolidKhodro> GetAmarTolid(string SelectedSal, string SelectedMah)
        {

            ObservableCollection<AmartolidKhodro> re = new ObservableCollection<AmartolidKhodro>();


            if (!string.IsNullOrEmpty(SelectedSal) & !string.IsNullOrEmpty(SelectedMah))
            {
                var kho = db.Khodros.ToList();

                foreach (var item in kho)
                {
                    re.Add(new AmartolidKhodro() { Khodro = item.Name, IdKhodro = item.Id, Tadad = 0 });
                }

                string rrrr = SelectedSal + SelectedMah;
                var dd = db.AmarTolidKhodros.Where(p => p.SalMah == rrrr).ToList();

                if (dd != null && dd.Count>0)
                {
                    foreach (var item in dd)
                    {
                        re.Single(p => p.IdKhodro == item.KhodroId).Tadad = item.TedadTolid;
                        //re.Add(new AmartolidKhodro()
                        //{
                        //    Khodro = item.Khodro.Name,
                        //    khodroId = item.KhodroId,
                        //    Tadad = item.TedadTolid
                        //});
                    }
                }
                
            }

            return re;
        }

        public void SaveAmarTolid(string SelectedSal, string SelectedMah,List<AmartolidKhodro> AmarTolidList)
        {
            if (!string.IsNullOrEmpty(SelectedSal) & !string.IsNullOrEmpty(SelectedMah))
            {
                string rrrr = SelectedSal + SelectedMah;

                var dd = db.AmarTolidKhodros.Where(p => p.SalMah == rrrr).ToList();

                if (dd != null && dd.Count > 0)//Update
                {
                    foreach (var item in dd)
                    {
                        item.TedadTolid = AmarTolidList.Single(p => p.IdKhodro == item.KhodroId).Tadad;
                    }

                    db.SaveChanges();
                }
                else//Add
                {
                    List<AmarTolidKhodro> k = new List<AmarTolidKhodro>();

                    foreach (var item in AmarTolidList)
                    {
                        k.Add(new AmarTolidKhodro() { KhodroId = item.IdKhodro, SalMah = rrrr, TedadTolid = item.Tadad });
                    }

                    db.AmarTolidKhodros.AddRange(k);

                    db.SaveChanges();
                }
                
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace ImportingLib
{
    public class SampleData
    {
        public SampleData()
        {
            Columns = new List<Column>();
        }
        //public SampleData(List<Column> columns, string fileName, string address)
        //{
        //    Columns = columns;
        //    FileName = fileName;
        //    Address = address;
        //}
        public List<Column> Columns { get; set; }
        public string FileName { get; set; }
        public string Address { get; set; }

        //public Dictionary<string, List<string>> GetDataView()
        //{
        //    Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();

        //    foreach (var item in Columns)
        //    {
        //        ret.Add(item.Header, item.Values);
        //    }

        //    return ret;
        //}
        //public ImportData GetImportHeader()
        //{
        //    ImportData match = new ImportData()
        //    {
        //        Car = 
        //    }

        //    return "";
        //    //اينجا يك نوع ImportData برميگرداند كه داخل هر پراپرتي آن نام ستون مورد نظر بر گردانده ميشود
        //}

        //public void SetProperty(string position, string property)
        //{
        //    var c = Columns.SingleOrDefault(p => p.Position == position & !p.IsPropertyMatch());

        //    if (c==null)
        //    {
        //        throw new Exception("Property Is Assign Before");
        //    }
        //    c.Property = property;
        //}

        public ImportData GetMatch()
        {
            ImportData match = new ImportData();

            match.Car = (Columns.Exists(p => p.MatchName == "Car") ?
                Columns.FirstOrDefault(p => p.MatchName == "Car").Position :
                    null);

            match.Codekala = (Columns.Exists(p => p.MatchName == "Codekala") ?
                Columns.FirstOrDefault(p => p.MatchName == "Codekala").Position :
                    null);

            match.DriverName = (Columns.Exists(p => p.MatchName == "DriverName") ?
                Columns.FirstOrDefault(p => p.MatchName == "DriverName").Position :
                    null);

            match.FaniCode = (Columns.Exists(p => p.MatchName == "FaniCode") ?
                Columns.FirstOrDefault(p => p.MatchName == "FaniCode").Position :
                    null);

            match.Maghsad = (Columns.Exists(p => p.MatchName == "Maghsad") ?
                Columns.FirstOrDefault(p => p.MatchName == "Maghsad").Position :
                    null);

            match.Pelak = (Columns.Exists(p => p.MatchName == "Pelak") ?
                Columns.FirstOrDefault(p => p.MatchName == "Pelak").Position:
                    null);

            match.Pelak = (Columns.Exists(p => p.MatchName == "Pelak") ?
                Columns.FirstOrDefault(p => p.MatchName == "Pelak").Position :
                    null);

            match.Phone = (Columns.Exists(p => p.MatchName == "Phone") ?
                Columns.FirstOrDefault(p => p.MatchName == "Phone").Position :
                    null);

            match.SanadNumber = (Columns.Exists(p => p.MatchName == "SanadNumber") ?
                Columns.FirstOrDefault(p => p.MatchName == "SanadNumber").Position :
                   null);

            match.Tarikh = (Columns.Exists(p => p.MatchName == "Tarikh") ?
                Columns.FirstOrDefault(p => p.MatchName == "Tarikh").Position :
                    null);

            match.Tedad = (Columns.Exists(p => p.MatchName == "Tedad") ?
                Columns.FirstOrDefault(p => p.MatchName == "Tedad").Position :
                    null);

            return match;
            //MatchData match = new MatchData();

            //match.Car = (Columns.Exists(p => p.MatchName == "Car") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Car").Header
            //        ,Columns.FirstOrDefault(p => p.MatchName == "Car").Position):
            //        new KeyValuePair<string,string>());

            //match.Codekala = (Columns.Exists(p => p.MatchName == "Codekala") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Codekala").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "Codekala").Position) :
            //        new KeyValuePair<string, string>());

            //match.DriverName = (Columns.Exists(p => p.MatchName == "DriverName") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "DriverName").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "DriverName").Position) :
            //        new KeyValuePair<string, string>());

            //match.FaniCode = (Columns.Exists(p => p.MatchName == "FaniCode") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "FaniCode").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "FaniCode").Position) :
            //        new KeyValuePair<string, string>());

            //match.Maghsad = (Columns.Exists(p => p.MatchName == "Maghsad") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Maghsad").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "Maghsad").Position) :
            //        new KeyValuePair<string, string>());

            //match.Pelak = (Columns.Exists(p => p.MatchName == "Pelak") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Pelak").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "Pelak").Position) :
            //        new KeyValuePair<string, string>());

            //match.Pelak = (Columns.Exists(p => p.MatchName == "Pelak") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Pelak").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "Pelak").Position) :
            //        new KeyValuePair<string, string>());
            //match.Phone = (Columns.Exists(p => p.MatchName == "Phone") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Phone").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "Phone").Position) :
            //        new KeyValuePair<string, string>());

            //match.SanadNumber = (Columns.Exists(p => p.MatchName == "SanadNumber") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "SanadNumber").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "SanadNumber").Position) :
            //        new KeyValuePair<string, string>());

            //match.Tarikh = (Columns.Exists(p => p.MatchName == "Tarikh") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Tarikh").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "Tarikh").Position) :
            //        new KeyValuePair<string, string>());

            //match.Tedad = (Columns.Exists(p => p.MatchName == "Tedad") ?
            //    new KeyValuePair<string, string>(Columns.FirstOrDefault(p => p.MatchName == "Tedad").Header
            //        , Columns.FirstOrDefault(p => p.MatchName == "Tedad").Position) :
            //        new KeyValuePair<string, string>());

            //return match;
        }


        public Dictionary<string, string> GetSavedMatch()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();

            foreach (var item in Columns)
            {
                if (!string.IsNullOrEmpty(item.MatchName))
                {
                    res.Add(item.MatchName, item.Header);
                }
            }

            return res;
        }
    }
}

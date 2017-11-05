using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public interface IBaseImportData
    {
        string KalaName { get; set; }
        string CodeKala { get; set; }
        string FaniCode { get; set; }
        string CodeJense { get; set; }
        string Tedad { get; set; }
        string Maghsad { get; set; }
        string Ranande { get; set; }
        string Pelak { get; set; }
        string Car { get; set; }

        //public List<string> GetFieldEnum()
        //{
        //    var t = "";
        //    foreach (PropertyInfo prop in typeof(IBaseImportData).GetProperties())
        //    {
        //        t = t + prop;
        //    }

        //    return t;
        //}
    }
}

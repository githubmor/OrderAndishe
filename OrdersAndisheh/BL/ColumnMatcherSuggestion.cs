using ImportingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class ColumnMatcherSuggestion
    {
        public string GetImportDataColumnName(string sampleColumnHeaderPosition)
        {
            string res = "";
            var Data = Properties.Settings.Default.ImportData;

            if (Data!=null)
            {
                var prop = Data.GetType().GetProperties();

                foreach (var item in prop)
                {
                    
                    string p = item..GetValue(Data, null).ToString();
                    if (sampleColumnHeaderPosition == p)
                    {
                        res = item.Name;
                        break;
                    }
                } 
            }
            return res;
        }

        public void SaveSetting(ImportData match)
        {
            Properties.Settings.Default.ImportData = match;
            Properties.Settings.Default.Save();
        }
    }
}

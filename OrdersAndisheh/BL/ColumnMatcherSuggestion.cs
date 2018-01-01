using ImportingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class ColumnMatcherSuggestion
    {
        public ColumnMatcherSuggestion()
        {

        }
        public void SaveSetting(Dictionary<string,string> match)
        {
            var savedlist = Properties.Settings.Default.SavedMatch;

            Dictionary<string, string> merged = match;

            if (savedlist!=null)
            {
                merged.MergeIn(savedlist, match); 
            }

            Properties.Settings.Default.SavedMatch = merged;
            Properties.Settings.Default.Save();
        }

        public void GetSuggestionMatch(List<Column> column)
        {
            var savedlist = Properties.Settings.Default.SavedMatch;

            if (savedlist!=null)
            {
                foreach (var item in column)
                {
                    var t = savedlist.ContainsValue(item.Header);
                    if (t)
                    {
                        item.MatchName = savedlist.First(p => p.Value == item.Header).Key;
                    }
                } 
            }

        }
    }

    public static class DictionaryExtensionMethods
    {
        public static void MergeIn<TKey, TValue>(
            this Dictionary<TKey, TValue> main,
            params Dictionary<TKey, TValue>[] dictionaries)
        {
            foreach (var dictionary in dictionaries)
            {
                foreach (var item in dictionary)
                {
                    if (!main.ContainsKey(item.Key))
                    {
                        main[item.Key] = item.Value;
                    }
                }
            }
        }
    }
}



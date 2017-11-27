using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportingLib
{
    public class Column
    {
        public Column()
        {
            Values = new List<string>();
        }
        //public Column(string position,string header ,List<string> values)
        //{
        //    Position = position;
        //    Header = header;
        //    Values = values;
        //}

        public string Header { get; set; }
        public List<string> Values { get; set; }
        public string Position { get; set; }
        //Property of ImportData Like Codekala
        public string MatchName { get; set; }
    }
}

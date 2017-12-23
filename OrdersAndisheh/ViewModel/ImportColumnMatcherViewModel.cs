using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using ImportingLib;
using OrdersAndisheh.BL;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{
    public class ImportColumnMatcherViewModel : ViewModelBase
    {
        public Column Column;

        public ImportColumnMatcherViewModel(Column Column)
        {
            this.Column = Column;
            ImportingData = new List<string>();
            var prop = typeof(ImportData).GetProperties();
            foreach (var item in prop)
            {
                ImportingData.Add(item.Name);
            }

            ColumnMatcherSuggestion cs = new ColumnMatcherSuggestion();
            MatchName = cs.GetImportDataColumnName(Column.Position);
            
        }

        public string Header
        {
            get { return Column.Header; }
            set { Column.Header = value; }
        }

        public string MatchName
        {
            get { return Column.MatchName; }
            set 
            {
                Column.MatchName = value;
            }
        }

        public List<string> ImportingData { get; set; }
        
    }
}
using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using ImportingLib;
using OrdersAndisheh.BL;
using OrdersAndisheh.BL.Importing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace OrdersAndisheh.ViewModel
{
    public class ImportViewModel : ViewModelBase
    {
        
        private SampleData sample;
        private RelayCommand _myCommand6;
        private RelayCommand _myCommand7;
        Importer Importer;

        public ImportViewModel()
        {
            sample = new SampleData();
            Matcher = new ObservableCollection<ImportColumnMatcherViewModel>();
            ImportType = new List<string> { "سفارش روزانه", "توليد خودرو" };
        }

        public List<string> ImportType { get; set; }

        public string FieldType 
        { 
            get {
                switch (SelectedImportType)
                {
                    case "سفارش روزانه":
                        return "تاريخ سفارش - 1397/04/19";
                    case "توليد خودرو":
                        return "سال ماه - 139704";
                    default:
                        return "";
                }
            } 
            //private set; 
        }

        public string Tarikh { get; set; }
        public string SelectedImportType
        {
            get { return selectedImportType; }
            set
            {
                selectedImportType = value;
                RaisePropertyChanged(() => SelectedImportType);
                RaisePropertyChanged(() => FieldType);
            }
        }

        private Table myVar;
        private string selectedImportType;
        

        public Table SampleColumn
        {
            get { return myVar; }
            set
            {
                myVar = value;
                RaisePropertyChanged(() => SampleColumn);
            }
        }

        public ObservableCollection<ImportColumnMatcherViewModel> Matcher { get; set; }

        
        public string FilePath
        {
            get { return sample.FileName; }
        }

        public RelayCommand GetFile
        {
            get
            {
                return _myCommand6 ?? (_myCommand6 = new RelayCommand(
                    ExecuteGetFile,
                    CanExecuteGetFile));
            }
        }

        private void ExecuteGetFile()
        {

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    openFileDialog1.Filter = "Excel Files (.xlsx)|*.xlsx|All Files (*.*)|*.*";
                    openFileDialog1.FilterIndex = 1;
                    var path = openFileDialog1.FileName;

                    
                    var day = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

                    //Tarikh = "1396/01/" + day;

                    Importer = new Importer(path);
                    sample = Importer.SampleData;
                    RaisePropertyChanged(() => FilePath);
                    SampleColumn = new Table(new ObservableCollection<Column>(sample.Columns));

                    ColumnMatcherSuggestion cs = new ColumnMatcherSuggestion();
                    cs.GetSuggestionMatch(sample.Columns);

                    foreach (var item in sample.Columns)
                    {
                        Matcher.Add(new ImportColumnMatcherViewModel(item));
                    }
                    RaisePropertyChanged(() => SampleColumn);
                    RaisePropertyChanged(() => Tarikh);

                    
                    //Properties.Settings.Default.LastExcelBackUpPath = dlg.SelectedPath;
                    //Properties.Settings.Default.Save();
                    //var CodeKalaColumnName = Properties.Settings.Default.CodeKala;
                    
                }
            }
        }

        
        private bool CanExecuteGetFile()
        {
            return true;
        }

        public RelayCommand TestRun
        {
            get
            {
                return _myCommand7 ?? (_myCommand7 = new RelayCommand(
                    ExecuteTest));
            }
        }

        private void ExecuteTest()
        {
            try
            {
                var match = sample.GetMatch();

                var i = Importer.GetImportDataWithMatch(match);

                if (SelectedImportType=="سفارش روزانه")
                {
                    SefareshImporting s = new SefareshImporting(new SefareshService());

                    s.Tarikh = Tarikh;

                    s.SaveData(i);

                    ColumnMatcherSuggestion cs = new ColumnMatcherSuggestion();
                    cs.SaveSetting(sample.GetSavedMatch());

                    MessageBox.Show("data row is " + i.Count);
                }
                else if (SelectedImportType == "توليد خودرو")
	            {
                    TolidKhodroImporting s = new TolidKhodroImporting();

                    s.SalMah = Tarikh;

                    s.SaveData(i);

                    ColumnMatcherSuggestion cs = new ColumnMatcherSuggestion();
                    cs.SaveSetting(sample.GetSavedMatch());

                    MessageBox.Show("data row is " + i.Count);

	            }
                
            }
            catch (Exception t)
            {

                MessageBox.Show(t.Message.ToString());
            }
        }

        
    }

    public class RowPropertyDescriptor : PropertyDescriptor
    {
        private int index;

        public RowPropertyDescriptor(string name, int index)
            : base(name, null)
        {
            this.index = index;
        }

        #region PropertyDescriptor

        public override string DisplayName { get { return Name; } }

        public override Type ComponentType { get { return typeof(string); } }

        public override bool IsReadOnly { get { return false; } }

        public override Type PropertyType { get { return typeof(string); } }

        public override object GetValue(object component)
        {
            return ((Row)component)[index];
        }

        public override void SetValue(object component, object value)
        {
            ((Row)component)[index] = (string)value;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override void ResetValue(object component)
        {

        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        #endregion
    }

    public class Row : DynamicObject
    {
        private Table table;
        private int row;

        public Row(Table namedArraysView, int row)
        {
            this.table = namedArraysView;
            this.row = row;
        }

        public string this[int col]
        {
            get { return table.RawData[col].Values[row]; }
            set { table.RawData[col].Values[row] = value; }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            int idx;
            bool found = table.PropertiesIndex.TryGetValue(binder.Name, out idx);
            if (found)
            {
                try
                {
                    this[idx] = (string)value;
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return base.TrySetMember(binder, value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            int idx;
            bool found = table.PropertiesIndex.TryGetValue(binder.Name, out idx);
            if (found)
            {
                result = this[idx];
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return table.PropertyNames;
        }
    }

    public class Table : BindingList<Row>, ITypedList
    {
        public ObservableCollection<Column> RawData { get; private set; }
        internal List<string> PropertyNames { get; private set; }
        internal Dictionary<string, int> PropertiesIndex { get; private set; }

        public Table(ObservableCollection<Column> headeredArrays)
        {
            bind(headeredArrays);

            headeredArrays.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) => { bind(headeredArrays); };
        }

        private void bind(ObservableCollection<Column> headeredArrays)
        {
            Clear();

            if (headeredArrays == null)
            {
                RawData = null;
                PropertyNames = null;
                PropertiesIndex = null;
                return;
            }

            RawData = headeredArrays;
            PropertyNames = RawData.Select(d => d.Header).ToList();

            PropertiesIndex = new Dictionary<string, int>();
            for (int i = 0; i < RawData.Count; i++)
                PropertiesIndex.Add(RawData[i].Header, i);

            int nRows = headeredArrays[0].Values.Count;
            for (int i = 0; i < nRows; i++)
                Add(new Row(this, i));
        }

        #region ITypedList

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            var dynamicDescriptors = new List<PropertyDescriptor>();
            if (this[0].GetDynamicMemberNames() == null) return new PropertyDescriptorCollection(new PropertyDescriptor[] { });
            var memberNames = this[0].GetDynamicMemberNames().ToArray();

            for (int i = 0; i < memberNames.Length; i++)
                dynamicDescriptors.Add(new RowPropertyDescriptor(memberNames[i], i));

            return new PropertyDescriptorCollection(dynamicDescriptors.ToArray());
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return null;
        }

        #endregion
    }
}

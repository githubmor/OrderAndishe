using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OrdersAndisheh.ExcelManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersAndisheh.ViewModel
{
    public class TahvilforoshViewModel : ViewModelBase
    {
        ISefareshService ss;
        ExcelService exService;
        public TahvilforoshViewModel(ISefareshService service)
        {
            ss = service;
        }

        private string file;

        public string FilePath
        {
            get { return file; }
            private set { file = value; }
        }

        private ObservableCollection<TahvilItem> myVar;

        public ObservableCollection<TahvilItem> ErsalListForTahvilFrosh
        {
            get { return myVar; }
            set 
            {
                myVar = value;
            }
        }
        



        private RelayCommand _myCommand6;

        /// <summary>
        /// Gets the GetFile.
        /// </summary>
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
                    FilePath = openFileDialog1.FileName;
                    DbService dbService = new DbService();
                    exService = new ExcelService(FilePath, dbService);
                    
                }
            }
        }

        private bool CanExecuteGetFile()
        {
            return true;
        }
    }

     public class TahvilItem
    {
        public string KalaName { get; set; }
        public int Tedad { get; set; }
        public int TahvilFroshNum { get; set; }
        public string Maghsad { get; set; }
        
    }
}

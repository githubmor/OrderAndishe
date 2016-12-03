using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
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
        Sefaresh sefaresh;
        public TahvilforoshViewModel(ISefareshService service)
        {
            ss = service;
            Messenger.Default.Register<string>(this, "ThisSefaresh", ThisSefaresh);
        }

        private void ThisSefaresh(string obj)
        {
            sefaresh = ss.LoadSefaresh(obj);
        }

        private string file;

        public string FilePath
        {
            get { return file; }
            private set 
            { 
                file = value;
                RaisePropertyChanged(() => FilePath);
            }
        }

        private ObservableCollection<TahvilItem> myVar;

        public ObservableCollection<TahvilItem> ErsalListForTahvilFrosh
        {
            get { return myVar; }
            set 
            {
                myVar = value;
                RaisePropertyChanged(() => ErsalListForTahvilFrosh);
            }
        }

        public List<string> Errors { get; set; }



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
                    CalculateData();
                }
            }
        }

        private void CalculateData()
        {
            ExcelImportService eis = new ExcelImportService(ss,FilePath);
            ErsalListForTahvilFrosh = eis.GetTahvilfroshData();
            CalculateSefareshWithData();
        }
        private bool CanExecuteGetFile()
        {
            return true;
        }

        private void CalculateSefareshWithData()
        {
            var tar = ErsalListForTahvilFrosh.Select(p => p.TarikhSanad).Distinct();
            if (tar.Count()>0)
            {
                Errors.Add("فایل شامل اسناد بیش ");
            }

            if (sefaresh.Tarikh)
            {
                
            }
        }

        
    }

    
}

using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
using OrdersAndisheh.BL.Importing;
using OrdersAndisheh.DBL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace OrdersAndisheh.ViewModel
{
    public class LastAmountViewModel : ViewModelBase
    {
        SefareshService ss;
        //Sefaresh sefaresh;
        public LastAmountViewModel()
        {
            ss = new SefareshService();
            //sefaresh = new Sefaresh();
            //Errors = new List<string>();
            //sefaresh.Items = new ObservableCollection<ItemSefaresh>();
            Amounts = new List<AmountData>();
            //Messenger.Default.Register<string>(this, "sefareshForAsn", ThisSefaresh);
        }

        //private void ThisSefaresh(string obj)
        //{
        //    sefaresh = ss.LoadSefaresh(obj);
        //    RaisePropertyChanged(() => this.ErsalListForTahvilFrosh);
        //}

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

        //public ObservableCollection<AmountDto> Amounts;

        private List<AmountData> myVar;

        public List<AmountData> Amounts
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
        //private ObservableCollection<ItemSefaresh> myVar;

        //public ObservableCollection<ItemSefaresh> ErsalListForTahvilFrosh
        //{
        //    get { return sefaresh.Items; }
        //    //set 
        //    //{
        //    //    myVar = value;
        //    //    RaisePropertyChanged(() => ErsalListForTahvilFrosh);
        //    //}
        //}

        //public List<string> Errors { get; set; }



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
                    RaisePropertyChanged(() => this.Amounts);
                }
            }
        }

        private void CalculateData()
        {
            //try
            //{
                ExcelImportService eis = new ExcelImportService();
                Amounts = eis.GetAmountData(FilePath);

            //}
            //catch (Exception ree)
            //{

            //    MessageBox.Show(ree.Message.ToString());
            //}
        }
        private bool CanExecuteGetFile()
        {
            return true;
        }


        private RelayCommand _myCommand555557172;
            
        /// <summary>
        /// Gets the SaveTahvilFrosh.
        /// </summary>
        public RelayCommand SetLastAmount
        {
            get
            {
                return _myCommand555557172 ?? (_myCommand555557172 = new RelayCommand(
                    ExecuteSaveTahvilFrosh,
                    CanExecuteSaveTahvilFrosh));
            }
        }

        private void ExecuteSaveTahvilFrosh()
        {
            try
            {
                ss.AddNewAmount(Amounts);
                MessageBox.Show("اطلاعات ذخیره شد");
            }
            catch (Exception t)
            {

                MessageBox.Show(t.InnerException.InnerException.Message.ToString());
            }
            
        }

        private bool CanExecuteSaveTahvilFrosh()
        {
            return Amounts.Count>0;
        }
        
    }

    
}

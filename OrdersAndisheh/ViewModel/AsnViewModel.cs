using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
using OrdersAndisheh.BL.Asn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace OrdersAndisheh.ViewModel
{
    public class AsnViewModel : ViewModelBase
    {
        SefareshService ss;
        //ExcelService exService;
        Sefaresh sefaresh;
        public AsnViewModel(ISefareshService service)
        {
            ss = new SefareshService();
            sefaresh = new Sefaresh();
            Errors = new List<string>();
            sefaresh.Items = new ObservableCollection<ItemSefaresh>();
            Asns = new List<IAsn>();
            Messenger.Default.Register<string>(this, "sefareshForAsn", ThisSefaresh);
        }

        private void ThisSefaresh(string obj)
        {
            sefaresh = ss.LoadSefaresh(obj);
            RaisePropertyChanged(() => this.ErsalListForTahvilFrosh);
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

        private List<IAsn> Asns;
        //private ObservableCollection<ItemSefaresh> myVar;

        public ObservableCollection<ItemSefaresh> ErsalListForTahvilFrosh
        {
            get { return sefaresh.Items; }
            //set 
            //{
            //    myVar = value;
            //    RaisePropertyChanged(() => ErsalListForTahvilFrosh);
            //}
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
            try
            {
                ExcelImportService eis = new ExcelImportService(ss);
                Asns = eis.GetAsnData(FilePath);

                AsnChecker ck = new AsnChecker(Asns, sefaresh);
                Errors = ck.CheckAsns();
                //CalculateSefareshWithData();
            }
            catch (Exception ree)
            {

                MessageBox.Show(ree.Message.ToString());
            }
        }
        private bool CanExecuteGetFile()
        {
            return true;
        }

        //private void CalculateSefareshWithData()
        //{

        //    foreach (var item in sefaresh.Items)
        //    {
        //        var anbarNum = ss.GetAnbarNumber(item.Product, item.Customer);

        //        //چون کانبانی نشون میده ممکنه کل محموله بشه چند کانبان ولی تعداش در مجموع بشه همون عدد
        //        var findedAsnsWithSameAnbarAndFaniCode = Asns
        //            .Where(p => p.FaniCode == item.Product.FaniCode)
        //            .Where(p => p.AnbarNumber == anbarNum).ToList();
        //        Dictionary<aAsn,int> r = new Dictionary<aAsn,int>();
                
        //        //باید اول چک شود اول اینکه مجموع تعدادی که داخل یک بارنامه هست با تعداد میخونه یا نه
        //        //بعد راننده چک شود
        //        if (findedAsnsWithSameAnbarAndFaniCode!=null)
        //        {
        //            var findedAsnNumbers = findedAsnsWithSameAnbarAndFaniCode.Select(p => p.AsnNumber).Distinct().ToList();
        //            if (findedAsnNumbers.Count == 1)//نمی تواند صفر باشد
        //            {
        //                int majmo = findedAsnsWithSameAnbarAndFaniCode.Select(p => p.Tedad).Sum();
        //                if (majmo == item.Tedad)
        //                {
        //                    if (IsDriverSameAsAsn(item,findedAsnsWithSameAnbarAndFaniCode.First()))
        //                    {
        //                        findedAsnsWithSameAnbarAndFaniCode.First().IsOk = true;
        //                        item.AsnNumber = int.Parse(findedAsnNumbers.First());
        //                    }
        //                    else
        //                    {
        //                        Errors.Add("اطلاعات راننده در بارنامه"
        //                        + findedAsnNumbers.First()
        //                        + "اشتباه ثبت شده");
        //                    }
        //                }
        //                else
        //                {
        //                    Errors.Add("تعداد کالای "
        //                        + item.Kala
        //                        + " در بارنامه "
        //                        + findedAsnNumbers.First()
        //                        + " بجای "
        //                        + item.Tedad
        //                        + " عدد "
        //                        + majmo
        //                        + " عدد ثبت شده");
        //                    if (!IsDriverSameAsAsn(item, findedAsnsWithSameAnbarAndFaniCode.First()))
        //                    {
        //                        Errors.Add("اطلاعات راننده در بارنامه"
        //                        + findedAsnNumbers.First()
        //                        + "اشتباه ثبت شده");
        //                    }
        //                }
        //            }
        //            else//یعنی بارنامه های بیش از یکی ین شماره فنی و این انبار را دارد که باید چک شود راننده جدا دارد یا نه و تعداش می خواند یا نه
        //            {
        //                int[] tedadHa = new int[findedAsnNumbers.Count];
        //                for (int i = 0; i < tedadHa.Length; i++)
        //                {
        //                    tedadHa[i] = findedAsnsWithSameAnbarAndFaniCode
        //                        .Where(p=>p.AsnNumber==findedAsnNumbers[i])
        //                        .Select(p => p.Tedad).Sum();
        //                }
        //                if (tedadHa.Where(p => p == item.Tedad).Count()==1)
        //                {
        //                    var asnNum = tedadHa.Where(p=>p == item.Tedad).
        //                }
        //            }
        //        }
                
                
        //    }

            
        //    RaisePropertyChanged(() => Errors);
        //    RaisePropertyChanged(() => ErsalListForTahvilFrosh);
        //}

        private bool IsDriverSameAsAsn(ItemSefaresh item, aAsn asn)
        {
            return false;
        }

        
        private RelayCommand _myCommand55555772;
            
        /// <summary>
        /// Gets the SaveTahvilFrosh.
        /// </summary>
        public RelayCommand SaveTahvilFrosh
        {
            get
            {
                return _myCommand55555772 ?? (_myCommand55555772 = new RelayCommand(
                    ExecuteSaveTahvilFrosh,
                    CanExecuteSaveTahvilFrosh));
            }
        }

        private void ExecuteSaveTahvilFrosh()
        {
            //MessageBox.Show("تحویل فروش  تاریخ " + sefaresh.Tarikh + " ثبت شد " );
            //ss.Save();
        }

        private bool CanExecuteSaveTahvilFrosh()
        {
            return !sefaresh.Items.Any(p=>p.TahvilFrosh<1);
        }
        
    }

    
}

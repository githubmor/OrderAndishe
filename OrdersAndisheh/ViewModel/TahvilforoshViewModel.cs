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
        //ExcelService exService;
        Sefaresh sefaresh;
        public TahvilforoshViewModel(ISefareshService service)
        {
            ss = service;
            sefaresh = new Sefaresh();
            Errors = new List<string>();
            sefaresh.Items = new ObservableCollection<ItemSefaresh>();
            TahvilFroshs = new ObservableCollection<TahvilItem>();
            Messenger.Default.Register<string>(this, "sefareshForTahvilSet", ThisSefaresh);
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

        private ObservableCollection<TahvilItem> TahvilFroshs;
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
            ExcelImportService eis = new ExcelImportService(ss,FilePath);
            TahvilFroshs = eis.GetTahvilfroshData();
            CalculateSefareshWithData();
        }
        private bool CanExecuteGetFile()
        {
            return true;
        }

        private void CalculateSefareshWithData()
        {
            List<string> tar = TahvilFroshs.Select(p => p.TarikhSanad).Distinct().ToList();
            string tarikhha = "";
            
            //bool HasTarikhmorThanOne= false;
            
            if (tar.Count()>1)
            {
                for (int i = 0; i < tar.Count(); i++)
                {
                    tarikhha += tar[i] + "-";
                }
                string rr = "فایل شامل اسناد بیش از یک تاریخ می باشد " + tarikhha;
                //for (int i = 0; i < tar.Count(); i++)
                //{
                //    rr += tar[i] + "-";
                //}
                Errors.Add(rr);
                //HasTarikhmorThanOne = true;
            }

            foreach (var item in sefaresh.Items)
            {
                
                var findedTahvilFrishes = TahvilFroshs
                    .Where(o => o.CodeKala == item.CodeKala)
                    .Where(t => t.Tedad == item.Tedad)
                    .ToList();
                
                if (findedTahvilFrishes.Count == 0)
                {
                    Errors.Add("برای کالای " +item.Kala + " ارسالی به " + item.Maghsad + " تحویل فروشی ثبت نشده");
                }
                else if(findedTahvilFrishes.Count>1)
                {
                    //string tahvilha_tarikh = "";
                    //foreach (var yt in tr)
                    //{
                    //    tahvilha_tarikh += yt.TarikhSanad + " - " + yt.TahvilFroshNum;
                    //}
                    var po = findedTahvilFrishes.Select(pd => pd.TarikhSanad).Distinct();

                    if (po.Count()>1)
                    {
                        string defd = "برای کالای " + item.Kala
                        + " ارسالی به " + item.Maghsad + " "
                        + findedTahvilFrishes.Count + " تحویل فروش داریم ";
                        

                        foreach (var we in findedTahvilFrishes)
                        {
                            defd += we.TahvilFroshNum + "(" + we.TarikhSanad + ")" + "-";
                            we.IsOk = true;
                        }

                        Errors.Add(defd);
                    }
                    else
                    {
                        string defd = "برای کالای " + item.Kala
                        + " ارسالی به " + item.Maghsad + " "
                        + findedTahvilFrishes.Count + " تحویل فروش داریم ";

                        foreach (var we in findedTahvilFrishes)
                        {
                            defd += we.TahvilFroshNum + "-";
                            we.IsOk = true;
                        }

                        Errors.Add(defd);
                    }
                    
                }
                else //if(!HasTarikhmorThanOne)
                {
                    item.TahvilFrosh = findedTahvilFrishes[0].TahvilFroshNum;
                    findedTahvilFrishes[0].IsOk = true;
                }
                //else
                //{
                //    Errors.Add("برای کالای " + item.Kala + " ارسالی به " + item.Maghsad + " " + findedTahvilFrishes.Count + " تحویل فروش داریم ");
                //}

                
            }
            var nuy = TahvilFroshs.Where(isa => !isa.IsOk).ToList();
            foreach (var de in nuy)
            {
                Errors.Add("برای تحویل فروش " +
                    de.TahvilFroshNum + " کالای " +
                    de.KalaName + " به تعداد " +
                    de.Tedad + " آیتمی در سفارش وجود ندارد");
            }

            RaisePropertyChanged(() => Errors);
        }

        private RelayCommand _myCommand145;

        /// <summary>
        /// Gets the SaveTahvilFrosh.
        /// </summary>
        public RelayCommand SaveTahvilFrosh
        {
            get
            {
                return _myCommand145
                    ?? (_myCommand145 = new RelayCommand(ExecuteSaveTahvilFrosh));
            }
        }

        private void ExecuteSaveTahvilFrosh()
        {
            MessageBox.Show(sefaresh.Items.Count.ToString());
            ss.Save();
        }
        
    }

    
}

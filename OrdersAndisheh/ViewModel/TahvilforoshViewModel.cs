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
            try
            {
                ExcelImportService eis = new ExcelImportService(ss, FilePath);
                TahvilFroshs = eis.GetTahvilfroshData();
                CalculateSefareshWithData();
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

        private void CalculateSefareshWithData()
        {
            CheckMoreThanOneTarikh();

            foreach (var sefaresh_item in sefaresh.Items)
            {
                
                var finded_TF_FromCodeTedad = TahvilFroshs
                    .Where(o => o.CodeKala == sefaresh_item.CodeKala)
                    .Where(t => t.Tedad == sefaresh_item.Tedad)
                    .ToList();
                
                if (finded_TF_FromCodeTedad.Count == 0)
                {
                    Errors.Add("برای کالای "
                        +sefaresh_item.Kala + " ارسالی به " 
                        + sefaresh_item.Maghsad + " تحویل فروشی ثبت نشده");
                    sefaresh_item.TahvilFrosh = -1;//برای اینکه بدونیم اینا تحویل فروش نداشت نه اینکه دو تا داشت
                }
                else if (finded_TF_FromCodeTedad.Count == 1)
                {
                    sefaresh_item.TahvilFrosh = finded_TF_FromCodeTedad[0].TahvilFroshNum;
                    finded_TF_FromCodeTedad[0].IsOk = true;
                }
            }

            //سفارش هایی که هنوز تحویل فروش نگرفتن رو میگیریم
            var sefareshItemMoreThanOneTahvil = sefaresh.Items.Where(t => t.TahvilFrosh == 0).ToList();

            foreach (var itemi in sefareshItemMoreThanOneTahvil)
            {
                //سفارش هایی که با این سفارش هم راننده و هم مقصد هستن ، شماره تحویل فروش هاش رو میاریم
                var pofd = sefaresh.Items
                    .Where(iu => iu.Ranande == itemi.Ranande)
                    .Where(tt => tt.Maghsad == itemi.Maghsad)
                    .Where(tt => tt.IsImenKala == itemi.IsImenKala)
                    .Where(p => p.TahvilFrosh > 0);
                var y = pofd.Select(t => t.TahvilFrosh).Distinct();
                if (y.Count()==1)
                {
                    var sad = TahvilFroshs
                            .Where(te => !te.IsOk)
                            .Where(hd => hd.Tedad == itemi.Tedad)
                            .Where(hd => hd.CodeKala == itemi.CodeKala)
                            .Where(zx=>zx.TahvilFroshNum==y.First())
                            .FirstOrDefault();
                    //چک میکنیم این شماره تحویل فروش تو لیست اوکی نشده های تحویل فروش هست یا نه
                    if (sad != null)
                    {
                        // اگر بود این شماره رو میدیم به آیتم
                        itemi.TahvilFrosh = y.First();
                        sad.IsOk = true;
                    }
                }
                else if (y.Count() > 1)
                {
                     string defd = "برای کالای " + itemi.Kala
                        + " ارسالی به " + itemi.Maghsad + " "
                        + y.Count() + " تحویل فروش داریم ";


                     foreach (var ss in y)
                     {
                         defd += y + "-";
                     }

                     Errors.Add(defd);
                }
                //foreach (var sde in y)
                //{
                //    if (sde > 0)
                //    {
                //        var sad = TahvilFroshs
                //            .Where(te => !te.IsOk)
                //            .Where(hj => hj.TahvilFroshNum == sde)
                //            .Where(hd=>hd.Maghsad==itemi.Maghsad)
                //            .Where(hd => hd.CodeKala == itemi.CodeKala)
                //            .FirstOrDefault();
                //        //چک میکنیم این شماره تحویل فروش تو لیست اوکی نشده های تحویل فروش هست یا نه
                //        if (sad != null)
                //        {
                //            // اگر بود این شماره رو میدیم به آیتم
                //            itemi.TahvilFrosh = sad.TahvilFroshNum;
                //            sad.IsOk = true;
                //        }
                //    }
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



        private void CheckMoreThanOneTarikh()
        {
            List<string> tar = TahvilFroshs.Select(p => p.TarikhSanad).Distinct().ToList();
            string tarikhha = "";
            string rr = "";
            if (tar.Count() > 1)
            {
                for (int i = 0; i < tar.Count(); i++)
                {
                    tarikhha += tar[i] + "-";
                }
                rr = "فایل شامل اسناد بیش از یک تاریخ می باشد " + tarikhha;
                Errors.Add(rr);
            }

            
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

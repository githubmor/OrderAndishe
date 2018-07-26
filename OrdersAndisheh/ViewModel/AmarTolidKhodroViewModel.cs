using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
using OrdersAndisheh.BL.Importing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace OrdersAndisheh.ViewModel
{
    public class AmarTolidKhodroViewModel : ViewModelBase
    {
        AmarService ss;

        private string selectedSal;

        public AmarTolidKhodroViewModel()
        {
            ss = new AmarService();

            Sal = ss.LoadYears();
            amar = new ObservableCollection<AmartolidKhodro>();
           
        }

        public List<string> Sal { get; set; }

        public string SelectedSal
        {
            get { return selectedSal; }
            private set
            {
                selectedSal = value;
                RaisePropertyChanged(() => SelectedSal);
                RaisePropertyChanged(() => Mah);
            }
        }

        public List<string> Mah
        {
            get { return ss.LoadMonth(SelectedSal); }

        }


        private string selectedMah;
        public string SelectedMah
        {
            get { return selectedMah; }
            private set
            {
                selectedMah = value;
                RaisePropertyChanged(() => SelectedMah);
                amar = ss.GetAmarTolid(SelectedSal,SelectedMah);
                RaisePropertyChanged(() => AmarTolidList);
            }
        }

        private ObservableCollection<AmartolidKhodro> amar;

        public ObservableCollection<AmartolidKhodro> AmarTolidList
        {
            get
            {
                return amar;
            }
            set
            {
                amar = value;
                RaisePropertyChanged(() => AmarTolidList);
            }
        }
                    


       
        private RelayCommand _myCommand55555772;
            
        /// <summary>
        /// Gets the SaveTahvilFrosh.
        /// </summary>
        public RelayCommand SaveAmar
        {
            get
            {
                return _myCommand55555772 ?? (_myCommand55555772 = new RelayCommand(
                    ExecuteSaveAmar,
                    CanExecuteSaveAmar));
            }
        }

        private void ExecuteSaveAmar()
        {
            try
            {
                ss.SaveAmarTolid(SelectedSal, SelectedMah, AmarTolidList.ToList());
            }
            catch (Exception t)
            {

                MessageBox.Show(t.InnerException.Message.ToString()); ;
            }
        }

        private bool CanExecuteSaveAmar()
        {
            return AmarTolidList.Count > 0 & AmarTolidList.Any(p=>p.Tadad>0); 
        }

        private RelayCommand _myCommand55555sdf772;
        

        /// <summary>
        /// Gets the SaveTahvilFrosh.
        /// </summary>
        public RelayCommand ClearAmar
        {
            get
            {
                return _myCommand55555sdf772 ?? (_myCommand55555sdf772 = new RelayCommand(
                    ExecuteClearAmar,
                    CanExecuteClearAmar));
            }
        }

        private void ExecuteClearAmar()
        {
            foreach (var item in AmarTolidList)
            {
                item.Tadad = 0;
            }
        }

        private bool CanExecuteClearAmar()
        {
            return AmarTolidList.Any(p => p.Tadad > 0);
        }

        //private RelayCommand _myCommand5555as72;

        ///// <summary>
        ///// Gets the SaveTahvilFrosh.
        ///// </summary>
        //public RelayCommand TempSaveTahvilFrosh
        //{
        //    get
        //    {
        //        return _myCommand5555as72 ?? (_myCommand5555as72 = new RelayCommand(
        //            ExecuteTempSaveTahvilFrosh,
        //            CanExecuteTempSaveTahvilFrosh));
        //    }
        //}

        //private void ExecuteTempSaveTahvilFrosh()
        //{
        //    foreach (var item in sefaresh.Items.Where(p => p.TahvilFrosh <= 0))
        //    {
        //        item.TahvilFrosh = short.Parse("9999");
        //    }
        //    ExecuteSaveTahvilFrosh();
        //}

        //private bool CanExecuteTempSaveTahvilFrosh()
        //{
        //    return sefaresh.Items.Any(p => p.TahvilFrosh <=0);
        //}
    }

    
}

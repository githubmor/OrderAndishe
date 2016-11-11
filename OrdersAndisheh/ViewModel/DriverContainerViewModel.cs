using BL;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace OrdersAndisheh.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DriverContainerViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the DriverContainerViewModel class.
        /// </summary>
        public DriverContainerViewModel()
        {
            Mahmole = new ObservableCollection<ItemSefaresh>();
            Mahmole.CollectionChanged += (sender, e) =>
            {
                RaisePropertyChanged(() => this.VaznKol);
                RaisePropertyChanged(() => this.JaigahCount);
            };
        }


        public ObservableCollection<ItemSefaresh> Mahmole { get; set; }

        private int vaznCal()
        {
            int s = 0;
            foreach (var item in Mahmole)
            {
                s += item.Vazn;
            }
            return s;
        }

        public int VaznKol
        {
            get { return vaznCal(); }
        }
        public int JaigahCount
        {
            get { return JaigahCountCal(); }
        }

        private int JaigahCountCal()
        {
            int d = 0;
            foreach (var item in Mahmole)
            {
                d += int.Parse(item.Pallet);
            }
            return d;
        }
    }
}
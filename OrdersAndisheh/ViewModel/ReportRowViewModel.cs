using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace OrdersAndisheh.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ReportRowViewModel : ViewModelBase
    {
        public ReportRow item;

        public ReportRowViewModel(ReportRow item)
        {
            
            this.item = item;
        }

        public string Tedad
        {
            get { return item.Tedad; }
            set { item.Tedad = value; }
        }

        public string Kala
        {
            get { return item.Kala; }
            set { item.Kala = value; }
        }

        public int Pos
        {
            get { return item.Position; }
            set { item.Position = value; }
        }

        public string Ranande
        {
            get { return item.Ranande; }
            set { item.Ranande = value; }
        }

        public string Maghsad
        {
            get { return item.Maghsad; }
            set { item.Maghsad = value; }
        }

        private RelayCommand _myCommand565;

        /// <summary>
        /// Gets the DelRow.
        /// </summary>
        public RelayCommand DelRow
        {
            get
            {
                return _myCommand565
                    ?? (_myCommand565 = new RelayCommand(ExecuteDelRow));
            }
        }

        private void ExecuteDelRow()
        {
            Messenger.Default.Send<int>(Pos, "DelThisRow");
        }
    }
}
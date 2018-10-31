using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class AmartolidKhodro : INotifyPropertyChanged
    {
        private string khodro;

        public string Khodro
        {
            get { return khodro; }
            set
            { 
                khodro = value;
                NotifyPropertyChanged("Khodro");
            }
        }


        private int idKhodro;

        public int IdKhodro
        {
            get { return idKhodro; }
            set
            {
                idKhodro = value;
                NotifyPropertyChanged("IdKhodro");
            }
        }

        private string sazandeh;

        public string Sazandeh
        {
            get { return sazandeh; }
            set
            {
                sazandeh = value;
                NotifyPropertyChanged("Sazandeh");
            }
        }

        private int tadad;

        public int Tadad
        {
            get { return tadad; }
            set
            {
                tadad = value;
                NotifyPropertyChanged("Tadad");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}

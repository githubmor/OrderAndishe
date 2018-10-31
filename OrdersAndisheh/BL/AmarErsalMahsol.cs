using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL
{
    public class AmarErsalMahsol : INotifyPropertyChanged
    {
        private string kala;

        public string Kala
        {
            get { return kala; }
            set
            {
                kala = value;
                NotifyPropertyChanged("Kala");
            }
        }


        private int idkala;

        public int IdKala
        {
            get { return idkala; }
            set
            {
                idkala = value;
                NotifyPropertyChanged("IdKala");
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

        private string shaerkat;

        public string Sherkat
        {
            get { return shaerkat; }
            set
            {
                shaerkat = value;
                NotifyPropertyChanged("Sherkat");
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


        private int darsad;

        public int Sahm
        {
            get { return darsad; }
            set
            {
                darsad = value;
                NotifyPropertyChanged("Sahm");
            }
        }


        public int Sahm_100_Percent
        {
            get { return (darsad>100?100:darsad); }
            //set
            //{
            //    darsad = value;
            //    NotifyPropertyChanged("Sahm");
            //}
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

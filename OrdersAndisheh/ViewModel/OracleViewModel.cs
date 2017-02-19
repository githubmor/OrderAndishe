using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.DBL;
using System.Collections.Generic;
using System.Linq;

namespace OrdersAndisheh.ViewModel
{
    
    public class OracleViewModel : ViewModelBase
    {
        private ISefareshService service;
       
        public OracleViewModel(ISefareshService _service)
        {
            Oracles = new List<ItemSefaresh>();
            service = _service;
            Messenger.Default.Register<string>(this, "SefareshTarikh", LoadOracleSefaresh);
        }

        private void LoadOracleSefaresh(string tarikh)
        {
            Sefaresh sefaresh = service.LoadSefaresh(tarikh);
            foreach (var item in sefaresh.Items)
            {
                var rt = service.HasOracle(item.Product, item.Customer);
                if (rt)
                {
                    Oracles.Add(item);
                }
            }
        }

        private List<ItemSefaresh> oracle;

        public List<ItemSefaresh> Oracles
        {
            get { return oracle; }
            set { oracle = value; }
        }

        private RelayCommand _myCommand1;

        /// <summary>
        /// Gets the SaveOracle.
        /// </summary>
        public RelayCommand SaveOracle
        {
            get
            {
                return _myCommand1 ?? (_myCommand1 = new RelayCommand(
                    ExecuteSaveOracle,
                    CanExecuteSaveOracle));
            }
        }

        private void ExecuteSaveOracle()
        {
            try
            {
                //service.SaveOracle(Oracles);
                service.Save();
                System.Windows.Forms.MessageBox.Show("Save Complete");
            }
            catch (System.Exception r)
            {

                System.Windows.Forms.MessageBox.Show(r.Message.ToString());
            }
        }

        private bool CanExecuteSaveOracle()
        {
            return true;
        }
    }
}
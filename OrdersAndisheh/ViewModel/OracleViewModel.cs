using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace OrdersAndisheh.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OracleViewModel : ViewModelBase
    {
        private ISefareshService service;
        /// <summary>
        /// Initializes a new instance of the OracleViewModel class.
        /// </summary>
        public OracleViewModel(ISefareshService _service)
        {
            service = _service;
            Messenger.Default.Register<Sefaresh>(this, "SefareshTarikh", LoadOracleSefaresh);
        }

        private void LoadOracleSefaresh(Sefaresh sefaresh)
        {
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
        

    }
}
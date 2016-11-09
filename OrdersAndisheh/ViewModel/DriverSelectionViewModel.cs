using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.DBL;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class DriverSelectionViewModel : ViewModelBase
    {
        ISefareshService service;
        /// <summary>
        /// Initializes a new instance of the DriverSelection class.
        /// </summary>
        public DriverSelectionViewModel(ISefareshService _service)
        {
            service = _service;
            MyProperty = service.LoadGoods();
            MyProperty2 = new List<Product>();
            //Messenger.Default.Register<string>(this, "ThisSefaresh", ThisSefaresh);
        }

        public List<Product> MyProperty { get; set; }


        public List<Product> MyProperty2 { get; set; }
        //private void ThisSefaresh(string obj)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
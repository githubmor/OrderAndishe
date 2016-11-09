using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

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
        /// <summary>
        /// Initializes a new instance of the DriverSelection class.
        /// </summary>
        public DriverSelectionViewModel()
        {
            Messenger.Default.Register<string>(this, "ThisSefaresh", ThisSefaresh);
        }

        private void ThisSefaresh(string obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
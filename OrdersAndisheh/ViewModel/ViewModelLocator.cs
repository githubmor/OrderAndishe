/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:OrdersAndisheh"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using OrdersAndisheh.DBL;

namespace OrdersAndisheh.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<FirstViewModel>();
            SimpleIoc.Default.Register<DriverSelectionViewModel>();
            SimpleIoc.Default.Register<ISefareshService, SefareshService>();
            
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public FirstViewModel First
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FirstViewModel>();
            }
        }

        public DriverSelectionViewModel DriverSel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DriverSelectionViewModel>();
            }
        }
        public static void Cleanup()
        {
            
        }
    }
}
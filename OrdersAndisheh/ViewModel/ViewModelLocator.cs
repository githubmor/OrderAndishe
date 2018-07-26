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
using System;

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
            //SimpleIoc.Default.Register<FirstViewModel>();
            //SimpleIoc.Default.Register<DriverSelectionViewModel>();
            SimpleIoc.Default.Register<OracleRelationViewModel>();
            SimpleIoc.Default.Register<ISefareshService, SefareshService>();
            SimpleIoc.Default.Register<TahvilforoshViewModel>();
            SimpleIoc.Default.Register<IMyContextCF, MyContextCF>();
            SimpleIoc.Default.Register<AsnViewModel>();
            SimpleIoc.Default.Register<OracleViewModel>();
            SimpleIoc.Default.Register<ImportViewModel>();
            SimpleIoc.Default.Register<AmarTolidKhodroViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>(Guid.NewGuid().ToString());
            }
        }

        public AsnViewModel ASN
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AsnViewModel>(Guid.NewGuid().ToString());
            }
        }

        public ImportViewModel Import
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ImportViewModel>(Guid.NewGuid().ToString());
            }
        }

        public TahvilforoshViewModel Tahvilforosh
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TahvilforoshViewModel>(Guid.NewGuid().ToString());
            }
        }

        public AmarTolidKhodroViewModel AmarTolidKhodro
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AmarTolidKhodroViewModel>(Guid.NewGuid().ToString());
            }
        }
        //public DriverSelectionViewModel DriverSel
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<DriverSelectionViewModel>(Guid.NewGuid().ToString());
        //    }
        //}

        public OracleRelationViewModel OracleRelation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OracleRelationViewModel>(Guid.NewGuid().ToString());
            }
        }
        public OracleViewModel Oracle
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OracleViewModel>(Guid.NewGuid().ToString());
            }
        }
        public static void Cleanup()
        {
            
        }
    }
}
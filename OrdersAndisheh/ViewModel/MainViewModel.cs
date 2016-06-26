using GalaSoft.MvvmLight;
using OrdersAndisheh.DL;
using System.Collections.Generic;
using System.Linq;

namespace OrdersAndisheh.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                //using (MyContext db = new MyContext())
                //{
                //    try
                //    {
                //        Pallet os = new Pallet() { Name = "asd", Vazn = 60 };
                //        Bazres asd = new Bazres() { Name = "ujg" };

                //        Product p = new Product()
                //        {
                //            TedadDarPallet = 5,
                //            TedadDarSabad = 6,
                //            Code = "ttr",
                //            CodeJense = "sastta",
                //            FaniCode = "gh",
                //            Nam = "fghj",
                //            Weight = 2.3D
                //        };
                //        p.Pallet = os;
                //        p.Bazres = asd;

                //        db.Product.Add(p);
                //        db.SaveChanges();

                //        Plist = db.Product.ToList();
                //    }
                //    catch (System.Exception r)
                //    {
                //        //foreach (var item in r.StackTrace.)
                //        //{
                //            System.Windows.MessageBox.Show(r.Message.ToString());
                //        //}
                        
                //    }
                //}

                
                // Code runs "for real"
            }
        }

        private List<Product> myVar;

        public List<Product> Plist
        {
            get { return myVar; }
            set { myVar = value; }
        }
        
    }
}
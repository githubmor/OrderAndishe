using BL;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

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
            //List<ReportRow> r = new List<ReportRow>();

            //for (int i = 0; i < 10; i++)
            //{
            //    r.Add(new ReportRow(){Kala ="کمربند 4 درب " + i,Karton = (i * i).ToString(),Maghsad="ایرانخودرو " + i,Pallet="4",Ranande="پورشریف " + i,Tedad=(i + 9).ToString(),Vazn="500" });
            //}

            //FileManagar f = new FileManagar(r, "1395/10/15");
            //f.CreatFile("");
            
        }
    }
}
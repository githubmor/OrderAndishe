using OrdersAndisheh.ViewModel;
using System.Windows;

namespace OrdersAndisheh.View
{
    /// <summary>
    /// Interaction logic for TahvilfroshView.xaml
    /// </summary>
    public partial class LastAmountView : Window
    {
        public LastAmountView()
        {
            InitializeComponent();
            this.DataContext = new LastAmountViewModel();
        }
    }
}

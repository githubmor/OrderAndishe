using DevExpress.Xpf.Grid;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace OrdersAndisheh.View
{
    /// <summary>
    /// Description for MainView.
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();       
        }

        private void gg_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GridControl s = (GridControl)sender;
            //Messenger.Default.Send<string>(s.SelectedItem.ToString(), "SelectedItem");
            System.Windows.Forms.MessageBox.Show(s.SelectedItem.ToString());
        }

       
    }
}
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace OrdersAndisheh.View
{
    /// <summary>
    /// Description for FirstView.
    /// </summary>
    public partial class FirstView : Window
    {
        /// <summary>
        /// Initializes a new instance of the FirstView class.
        /// </summary>
        public FirstView()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView s = (ListView)sender;
            //System.Windows.Forms.MessageBox.Show(s.SelectedItem.ToString());

            MainView v = new MainView();
            Messenger.Default.Send(s.SelectedItem, "EditSefaresh");
            v.Show();
            
        }


    }
}
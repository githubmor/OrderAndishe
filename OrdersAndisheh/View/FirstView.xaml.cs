using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Excel Files (.xlsx)|*.xlsx|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == true)
            {
                //MessageBox.Show(openFileDialog1.FileName);
                Messenger.Default.Send(openFileDialog1.FileName, "path");

            }
        }


    }
}
using BL;
using GalaSoft.MvvmLight.Messaging;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using OrdersAndisheh.BL;
using OrdersAndisheh.ViewModel;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace OrdersAndisheh.View
{
    /// <summary>
    /// Description for FirstView.
    /// </summary>
    public partial class FirstView : Window
    {
        //private TaskbarIcon tb;
        //private NotifyIcon TrayIcon;
        //private ContextMenuStrip TrayIconContextMenu;
        //private ToolStripMenuItem CloseMenuItem;
        /// <summary>
        /// Initializes a new instance of the FirstView class.
        /// </summary>
        public FirstView()
        {
            InitializeComponent();
            
            this.DataContext = new FirstViewModel();
            

        }


        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }

        

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Controls.DataGrid s = (System.Windows.Controls.DataGrid)sender;
            CheckSefaresh ss = (CheckSefaresh)s.SelectedItem;

            MainView v = new MainView();
            Messenger.Default.Send<string>(ss.TarikhSefaresh, "Editsefaresh");
            v.ShowDialog();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the open file dialog box.
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();

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

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainView v = new MainView();
            v.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string today = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
            SefareshService s = new SefareshService();
            if (s.ChechHasSefaresh(today))
            {
                MainView v = new MainView();
                Messenger.Default.Send<string>(today, "Editsefaresh");
                v.ShowDialog();
            }
            else
            {
                MainView v = new MainView();
                v.ShowDialog();
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
using BL;
using GalaSoft.MvvmLight.Messaging;
using Hardcodet.Wpf.TaskbarNotification;
using OrdersAndisheh.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OrdersAndisheh
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private TaskbarIcon tb;

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    // here you take control
        //    tb = (TaskbarIcon)this.FindResource("MyNotifyIcon");

        //}

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    MainView v = new MainView();
        //    v.ShowDialog();
        //}

        //private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        //{
        //    string today = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
        //    SefareshService s = new SefareshService();
        //    if (s.ChechHasSefaresh(today))
        //    {
        //        MainView v = new MainView();
        //        Messenger.Default.Send<string>(today, "EditSefaresh");
        //        v.ShowDialog();
        //    }
        //    else
        //    {
        //        System.Windows.Forms.MessageBox.Show("سفارشی برای امروز ثبت نشده");
        //    }
        //}

        //private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        //{
        //    F.WindowState = WindowState.Normal;
        //}

        //public App()
        //{
        //    //tb = (TaskbarIcon)Application.Current.FindResource("MyNotifyIcon");
        //    //TaskbarIcon tbi = new TaskbarIcon();
        //    //tbi.Icon = System.Drawing.SystemIcons.Hand;
        //    //tbi.Visibility = Visibility.Collapsed;
        //    ////initialize NotifyIcon
        //    tb = (TaskbarIcon)this.FindResource("MyNotifyIcon");
        //    //tb = (TaskbarIcon)this.Resources["MyNotifyIcon"];
        //}
    }
}

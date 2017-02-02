using Hardcodet.Wpf.TaskbarNotification;
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
        private TaskbarIcon tb;


        public App()
        {
            TaskbarIcon tbi = new TaskbarIcon();
            tbi.Icon = Resources.Error;
            tbi.Visibility = Visibility.Collapsed;
            ////initialize NotifyIcon
            ////tb = (TaskbarIcon)FindResource("MyNotifyIcon");
            //tb = (TaskbarIcon)this.Resources["MyNotifyIcon"];
        }
    }
}

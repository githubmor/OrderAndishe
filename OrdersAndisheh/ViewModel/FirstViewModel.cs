using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.View;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace OrdersAndisheh.ViewModel
{
    public class FirstViewModel : ViewModelBase
    {
        SefareshService ss;
        public FirstViewModel()
        {
            ss = new SefareshService();
            Lists = ss.LoadAllSefareshTarikh();
        }

        private List<string> lists;

        public List<string> Lists
        {
            get { return lists; }
            set { lists = value; }
        }

        public string SelectedTarikh { get; set; }

        private RelayCommand _myCommand;

        /// <summary>
        /// Gets the NewSefaresh.
        /// </summary>
        public RelayCommand NewSefaresh
        {
            get
            {
                return _myCommand ?? (_myCommand = new RelayCommand(
                    ExecuteNewSefaresh,
                    CanExecuteNewSefaresh));
            }
        }

        private void ExecuteNewSefaresh()
        {
            MainView v = new MainView();
            v.Show();
        }

        private bool CanExecuteNewSefaresh()
        {
            return true;
        }

        //private RelayCommand<string> _myCommand3;

        ///// <summary>
        ///// Gets the EditSefaresh.
        ///// </summary>
        //public RelayCommand<string> EditSefaresh
        //{
        //    get
        //    {
        //        return _myCommand3
        //            ?? (_myCommand3 = new RelayCommand<string>(ExecuteEditSefaresh));
        //    }
        //}

        //private void ExecuteEditSefaresh(string parameter)
        //{
        //    MainView v = new MainView();
        //    v.Show();
        //    Messenger.Default.Send(parameter, "EditSefaresh");
        //}

        private RelayCommand _myCommand2;

        /// <summary>
        /// Gets the EditSefaresh.
        /// </summary>
        public RelayCommand EditSefaresh
        {
            get
            {
                return _myCommand2 ?? (_myCommand2 = new RelayCommand(
                    ExecuteEditSefaresh,
                    CanExecuteEditSefaresh));
            }
        }

        private void ExecuteEditSefaresh()
        {
            if (!string.IsNullOrEmpty(SelectedTarikh))
            {
                MainView v = new MainView();
                v.Show();
                Messenger.Default.Send(SelectedTarikh, "EditSefaresh");
            }
        }

        private bool CanExecuteEditSefaresh()
        {
            return true;
        }

        private RelayCommand _myCommand45;

        /// <summary>
        /// Gets the BackUpDatabase.
        /// </summary>
        public RelayCommand BackUpDatabase
        {
            get
            {
                return _myCommand45
                    ?? (_myCommand45 = new RelayCommand(ExecuteBackUpDatabase));
            }
        }

        private void ExecuteBackUpDatabase()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string sourcePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string sourceFile = "OrderDb.mdf";
                string destinationPath = folderDialog.SelectedPath;
                string destinationFile = "123p.backup";
                string sourceFileName = Path.Combine(sourcePath, sourceFile);
                string destinationFileName = Path.Combine(destinationPath, destinationFile);

                try
                {
                    //conn.disconnect();
                    //ss.Dispose();
                    File.Copy(sourceFileName, destinationFileName, true);
                    MessageBox.Show("Database backup saved.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    //conn.connect();
                }
            }
            //// read connectionstring from config file
            //var connectionString = ConfigurationManager.ConnectionStrings["CodeFirstConection"].ConnectionString;

            //// read backup folder from config file ("C:/temp/")
            //var backupFolder = ConfigurationManager.AppSettings["BackupFolder"];

            //var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            //// set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            //var backupFileName = String.Format("{0}{1}-{2}.bak",
            //    backupFolder, "b",
            //    DateTime.Now.ToString("yyyy-MM-dd"));

            //using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            //{
            //    var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
            //        sqlConStrBuilder.AttachDBFilename, backupFileName);

            //    using (var command = new SqlCommand(query, connection))
            //    {
            //        connection.Open();
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        
    }


}
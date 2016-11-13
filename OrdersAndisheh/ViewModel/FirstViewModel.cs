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
using OrdersAndisheh.ExcelManager;
using System.Data.Entity.Validation;
using OrdersAndisheh.DevUI.Views;

namespace OrdersAndisheh.ViewModel
{
    public class FirstViewModel : ViewModelBase
    {
        SefareshService ss;
        ExcelService exService;
        public FirstViewModel()
        {
            ss = new SefareshService();
            Lists = ss.LoadAllSefareshTarikh();
            Messenger.Default.Register<string>(this, "path", getFilePath);
        }
        private void getFilePath(string path)
        {
            try
            {
                DbService dbService = new DbService();
                exService = new ExcelService(path, dbService);



                FilePath = exService.FilePath;

                HasNewBazres = exService.HasNewBazres;
                HasNewCustomer = exService.HasNewCustomer;
                HasNewPallet = exService.HasNewPallet;
                HasNewProduct = exService.HasNewProduct;
                HasNewDriver = exService.HasNewDriver;

                RaisePropertyChanged(() => FilePath);
                RaisePropertyChanged(() => HasNewBazres);
                RaisePropertyChanged(() => HasNewCustomer);
                RaisePropertyChanged(() => HasNewPallet);
                RaisePropertyChanged(() => HasNewProduct);
                RaisePropertyChanged(() => HasNewDriver);

            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message.ToString());
            }
        }
        public string FilePath { get; set; }

        public string CheckResult { get; set; }

        public bool HasNewProduct { get; set; }
        public bool HasNewCustomer { get; set; }
        public bool HasNewDriver { get; set; }
        public bool HasNewPallet { get; set; }
        public bool HasNewBazres { get; set; }




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

        private RelayCommand _myCommand255;

        /// <summary>
        /// Gets the CheckingData.
        /// </summary>
        public RelayCommand CheckingData
        {
            get
            {
                return _myCommand255
                    ?? (_myCommand255 = new RelayCommand(ExecuteCheckingData));
            }
        }

        private void ExecuteCheckingData()
        {
            try
            {
               CheckResult = exService.CheckingData();
               HasNewBazres = exService.HasNewBazres;
               HasNewCustomer = exService.HasNewCustomer;
               HasNewPallet = exService.HasNewPallet;
               HasNewProduct = exService.HasNewProduct;
               HasNewDriver = exService.HasNewDriver;
               RaisePropertyChanged(() => CheckResult);
               RaisePropertyChanged(() => HasNewBazres);
               RaisePropertyChanged(() => HasNewCustomer);
               RaisePropertyChanged(() => HasNewPallet);
               RaisePropertyChanged(() => HasNewProduct);
               RaisePropertyChanged(() => HasNewDriver);
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message.ToString());
            }
        }

        private RelayCommand _myCommand2;

        /// <summary>
        /// Gets the AddNewProduct.
        /// </summary>
        public RelayCommand AddNewProduct
        {
            get
            {
                return _myCommand2 ?? (_myCommand2 = new RelayCommand(ExecuteAddNewProduct));
            }
        }

        private void ExecuteAddNewProduct()
        {
            try
            {
                exService.AddNewProductToDataBase();
                MessageBox.Show("کالاهای جدید اضافه شد");
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    MessageBox.Show("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + "\n" +
                        eve.Entry.Entity.GetType().Name + "\n" + eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show("- Property: \"{0}\", Error: \"{1}\"" + "\n" +
                            ve.PropertyName + "\n" + ve.ErrorMessage);
                    }
                }
                throw;
            }
        }


        private RelayCommand _myCommand3;

        /// <summary>
        /// Gets the AddNewCustomer.
        /// </summary>
        public RelayCommand AddNewCustomer
        {
            get
            {
                return _myCommand3 ?? (_myCommand3 = new RelayCommand(ExecuteAddNewCustomer));
            }
        }

        private void ExecuteAddNewCustomer()
        {
            try
            {
                exService.AddNewCustomerToDataBase();
                MessageBox.Show("مقاصد جدید اضافه شد");
            }
            catch (Exception r)
            {
                
                MessageBox.Show(r.Message.ToString());
            }
        }



        private RelayCommand _myCommand4;

        /// <summary>
        /// Gets the AddNewDriver.
        /// </summary>
        public RelayCommand AddNewDriver
        {
            get
            {
                return _myCommand4
                    ?? (_myCommand4 = new RelayCommand(ExecuteAddNewDriver));
            }
        }

        private void ExecuteAddNewDriver()
        {
            try
            {
                exService.AddNewDriverToDataBase();
                MessageBox.Show("رانندگان جدید اضافه شد");
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message.ToString());
            }
        }

        private RelayCommand _myCommand5;

        /// <summary>
        /// Gets the AddNewPallet.
        /// </summary>
        public RelayCommand AddNewPallet
        {
            get
            {
                return _myCommand5
                    ?? (_myCommand5 = new RelayCommand(ExecuteAddNewPallet));
            }
        }

        private void ExecuteAddNewPallet()
        {
            try
            {
                exService.AddNewPalletToDataBase();
                MessageBox.Show("پالت های جدید اضافه شد");
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message.ToString());
            }
        }

        private RelayCommand _myCommand6;

        /// <summary>
        /// Gets the AddNewBazres.
        /// </summary>
        public RelayCommand AddNewBazres
        {
            get
            {
                return _myCommand6
                    ?? (_myCommand6 = new RelayCommand(ExecuteAddNewBazres));
            }
        }

        private void ExecuteAddNewBazres()
        {
            try
            {
                exService.AddNewBazresToDataBase();
                MessageBox.Show("بازرسان جدید اضافه شد");
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message.ToString());
            }
        }

        private RelayCommand _myCommand7;

        /// <summary>
        /// Gets the AddDays.
        /// </summary>
        public RelayCommand AddDays
        {
            get
            {
                return _myCommand7 ?? (_myCommand7 = new RelayCommand(
                    ExecuteAddDays,
                    CanExecuteAddDays));
            }
        }

        private void ExecuteAddDays()
        {
            try
            {
                exService.AddDaysToDataBase();
                MessageBox.Show("بازرسان جدید اضافه شد");
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message.ToString());
            }
        }

        private bool CanExecuteAddDays()
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

        private RelayCommand _myCommand265545;

        /// <summary>
        /// Gets the EditSefaresh.
        /// </summary>
        public RelayCommand EditSefaresh
        {
            get
            {
                return _myCommand265545 ?? (_myCommand265545 = new RelayCommand(
                    ExecuteEditSefaresh,
                    CanExecuteEditSefaresh));
            }
        }

        private void ExecuteEditSefaresh()
        {
            if (!string.IsNullOrEmpty(SelectedTarikh))
            {
                MainView v = new MainView();
                Messenger.Default.Send<string>(SelectedTarikh, "EditSefaresh");
                v.Show();
                
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

        private RelayCommand _myCommand565;

        /// <summary>
        /// Gets the OracleRelation.
        /// </summary>
        public RelayCommand OracleRelation
        {
            get
            {
                return _myCommand565
                    ?? (_myCommand565 = new RelayCommand(ExecuteOracleRelation));
            }
        }

        private void ExecuteOracleRelation()
        {
            OracleRelationView v = new OracleRelationView();
            v.Show();
        }


        private RelayCommand _myCommand654654;

        /// <summary>
        /// Gets the DataUIEdition.
        /// </summary>
        public RelayCommand DataUIEdition
        {
            get
            {
                return _myCommand654654
                    ?? (_myCommand654654 = new RelayCommand(ExecuteDataUIEdition));
            }
        }

        private void ExecuteDataUIEdition()
        {
            //UIEditon v = new UIEditon();
            //v.Show();
            DriverSelectionView v = new DriverSelectionView();
            Messenger.Default.Send<string>("1395/08/12", "ThisSefaresh");
            v.Show();
        }
        
    }


}
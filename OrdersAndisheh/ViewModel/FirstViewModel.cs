﻿using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.BL;
using OrdersAndisheh.ExcelManager;
using OrdersAndisheh.View;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace OrdersAndisheh.ViewModel
{
    public class FirstViewModel : ViewModelBase
    {
        private SefareshService ss;
        private ExcelService exService;

        public FirstViewModel()
        {
            ss = new SefareshService();
            Messenger.Default.Register<string>(this, "path", getFilePath);
            //ss.change();
        }

        public string today
        {
            get { return "سفارش امروز - " + PersianDateTime.Now.ToString(PersianDateTimeFormat.Date); }
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

        public string selectedYear
        {
            get { return se; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    se = PersianDateTime.Now.Year.ToString();
                }
                else
                {
                    se = value;
                    RaisePropertyChanged(() => CheckSefareshs);
                }
            }
        }

        public List<string> Years
        {
            get { return ss.GetYears(); }
        }

        public List<CheckSefaresh> CheckSefareshs
        {
            get
            {
                return ss.LoadCheckSefareshs(int.Parse(selectedYear));
            }
        }

        public CheckSefaresh SelectedSefareshCheck { get; set; }

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
            v.ShowDialog();
            RaisePropertyChanged(() => this.CheckSefareshs);
        }

        private bool CanExecuteNewSefaresh()
        {
            return true;
        }

        #region Add New data from Excel

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

        #endregion Add New data from Excel

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
            if (SelectedSefareshCheck != null)
            {
                MainView v = new MainView();
                Messenger.Default.Send<string>(SelectedSefareshCheck.TarikhSefaresh, "EditSefaresh");
                v.ShowDialog();
                //ReloadList(SelectedSefareshCheck);
                //RaisePropertyChanged(() => this.CheckSefareshs);
            }
        }

        private bool CanExecuteEditSefaresh()
        {
            return SelectedSefareshCheck != null;
        }

        #region BackUp

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
            OpenFileDialog folderDialog = new OpenFileDialog();
            folderDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            folderDialog.ShowDialog();
            //if (folderDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string sourcePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //    string sourceFile = "OrderDbCompact.sdf";
            //    string destinationPath = folderDialog.SelectedPath;
            //    string destinationFile = "BackUp.sdf";
            //    string sourceFileName = Path.Combine(sourcePath, sourceFile);
            //    string destinationFileName = Path.Combine(destinationPath, destinationFile);

            //    try
            //    {
            //        //conn.disconnect();
            //        //ss.Dispose();
            //        File.Copy(sourceFileName, destinationFileName, true);
            //        MessageBox.Show("Database backup saved.");
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //    finally
            //    {
            //        //conn.connect();
            //    }
            //}
        }

        private RelayCommand _myCommand65656;

        /// <summary>
        /// Gets the RestoreDatabase.
        /// </summary>
        public RelayCommand RestoreDatabase
        {
            get
            {
                return _myCommand65656
                    ?? (_myCommand65656 = new RelayCommand(ExecuteRestoreDatabase));
            }
        }

        private void ExecuteRestoreDatabase()
        {
            OpenFileDialog folderDialog = new OpenFileDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                string destinationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string destinationFile = "OrderDbCompact.sdf";
                string sourceFileName = folderDialog.FileName;
                //string sourceFile = "OrderDbCompactB.sdf";
                //string sourceFileName = Path.Combine(sourcePath, sourceFile);
                string destinationFileName = Path.Combine(destinationPath, destinationFile);

                try
                {
                    //conn.disconnect();
                    //ss.Dispose();
                    File.Copy(sourceFileName, destinationFileName, true);
                    MessageBox.Show("Database Restored.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    //conn.connect();
                }
                //RaisePropertyChanged(() => this.CheckSefareshs);
            }
        }

        #endregion BackUp

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
            //ImportView v = new ImportView();
            //v.ShowDialog();

            AmarTolidKhodroView v = new AmarTolidKhodroView();
            v.ShowDialog();
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
            DataUIView v = new DataUIView();
            v.Show();
        }

        private RelayCommand _myCommand6459653;

        /// <summary>
        /// Gets the CheckTempDriver.
        /// </summary>
        public RelayCommand BackUpAsExcel
        {
            get
            {
                return _myCommand6459653
                    ?? (_myCommand6459653 = new RelayCommand(ExecuteBackUpAsExcel, CanExecuteBackUpAsExcel));
            }
        }

        private bool CanExecuteBackUpAsExcel()
        {
            return SelectedSefareshCheck != null;
        }

        private void ExecuteBackUpAsExcel()
        {
            try
            {
                ExcelBackUp p = new ExcelBackUp(ss);

                string y = p.ExportLastSavedSefaresh(SelectedSefareshCheck.TarikhSefaresh);

                MessageBox.Show("اطلاعات در آدرس " + y + " ذخیره شد");
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message.ToString());
            }
        }

        private RelayCommand _myCommand56565;

        /// <summary>
        /// Gets the DelSefaresh.
        /// </summary>
        public RelayCommand DelSefaresh
        {
            get
            {
                return _myCommand56565 ?? (_myCommand56565 = new RelayCommand(
                    ExecuteDelSefaresh,
                    CanExecuteDelSefaresh));
            }
        }

        private void ExecuteDelSefaresh()
        {
            ss.DeleteSefaresh(SelectedSefareshCheck.TarikhSefaresh);
            RaisePropertyChanged(() => this.CheckSefareshs);
            MessageBox.Show("اطلاعات سفارش حذف شد");
        }

        private bool CanExecuteDelSefaresh()
        {
            return SelectedSefareshCheck != null;
        }

        private RelayCommand _myCommand5252656;

        /// <summary>
        /// Gets the SetTahvilfrosh.
        /// </summary>
        public RelayCommand SetTahvilfrosh
        {
            get
            {
                return _myCommand5252656 ?? (_myCommand5252656 = new RelayCommand(
                    ExecuteSetTahvilfrosh,
                    CanExecuteSetTahvilfrosh));
            }
        }

        private void ExecuteSetTahvilfrosh()
        {
            try
            {
                TahvilfroshView v = new TahvilfroshView();
                Messenger.Default.Send<string>(SelectedSefareshCheck.TarikhSefaresh, "sefareshForTahvilSet");
                v.ShowDialog();
                RaisePropertyChanged(() => this.CheckSefareshs);
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message.ToString());
            }
        }

        private bool CanExecuteSetTahvilfrosh()
        {
            return SelectedSefareshCheck != null;
        }

        private RelayCommand _myCommand6565656562252;

        /// <summary>
        /// Gets the CheckAsns.
        /// </summary>
        public RelayCommand CheckAsns
        {
            get
            {
                return _myCommand6565656562252 ?? (_myCommand6565656562252 = new RelayCommand(
                    ExecuteCheckAsns,
                    CanExecuteCheckAsns));
            }
        }

        private void ExecuteCheckAsns()
        {
            AsnView v = new AsnView();
            Messenger.Default.Send<string>(SelectedSefareshCheck.TarikhSefaresh, "sefareshForAsn");
            v.Show();
        }

        private bool CanExecuteCheckAsns()
        {
            return SelectedSefareshCheck != null;
        }

        private RelayCommand _myCommand43556111;

        /// <summary>
        /// Gets the AcceptSefaresh.
        /// </summary>
        public RelayCommand AcceptSefaresh
        {
            get
            {
                return _myCommand43556111 ?? (_myCommand43556111 = new RelayCommand(
                    ExecuteAcceptSefaresh,
                    CanExecuteAcceptSefaresh));
            }
        }

        private void ExecuteAcceptSefaresh()
        {
            try
            {
                ss.AcceptSefaresh(SelectedSefareshCheck.TarikhSefaresh);
                ExcelBackUp p = new ExcelBackUp(ss);
                string y = p.ExportLastSavedSefaresh(SelectedSefareshCheck.TarikhSefaresh);
                MessageBox.Show("اطلاعات در آدرس " + y + " ذخیره شد");
                RaisePropertyChanged(() => this.CheckSefareshs);
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message.ToString());
            }
        }

        private bool CanExecuteAcceptSefaresh()
        {
            return SelectedSefareshCheck != null && SelectedSefareshCheck.IsEveryThingOk;
        }

        private RelayCommand _myCommand5656565478111;

        /// <summary>
        /// Gets the ErsalReporting.
        /// </summary>
        public RelayCommand ErsalReporting
        {
            get
            {
                return _myCommand5656565478111
                    ?? (_myCommand5656565478111 = new RelayCommand(ExecuteErsalReporting));
            }
        }

        private void ExecuteErsalReporting()
        {
            ErsalReportViewModel vm = new ErsalReportViewModel(int.Parse(selectedYear));
            ErsalReportView v = new ErsalReportView();
            v.DataContext = vm;
            v.Show();
        }

        private RelayCommand _myCommand5465656565656;

        /// <summary>
        /// Gets the PalletReporting.
        /// </summary>
        public RelayCommand PalletReporting
        {
            get
            {
                return _myCommand5465656565656
                    ?? (_myCommand5465656565656 = new RelayCommand(ExecutePalletReporting));
            }
        }

        private void ExecutePalletReporting()
        {
            PalletReportViewModel vm = new PalletReportViewModel(int.Parse(selectedYear));
            PalletReportView v = new PalletReportView();
            v.DataContext = vm;
            v.Show();
        }

        private RelayCommand _myCommand52333565656;

        /// <summary>
        /// Gets the PalletReporting.
        /// </summary>
        public RelayCommand Shakhes
        {
            get
            {
                return _myCommand52333565656
                    ?? (_myCommand52333565656 = new RelayCommand(ExecuteShakhes));
            }
        }

        private void ExecuteShakhes()
        {
            ShakhesView v = new ShakhesView();
            v.ShowDialog();
        }

        private RelayCommand _myCommand75555;

        /// <summary>
        /// Gets the PelaskoReport.
        /// </summary>
        public RelayCommand PelaskoReport
        {
            get
            {
                return _myCommand75555
                    ?? (_myCommand75555 = new RelayCommand(ExecutePelaskoReport));
            }
        }

        private void ExecutePelaskoReport()
        {
            var items = ss.LoadAllPelaskosefaresh();

            foreach (var er in items)
            {
                er.Des = er.OrderDetail.Order.Tarikh;
            }

            FileManagar f = new FileManagar();
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    f.CreatePlaskoreport(items, dialog.SelectedPath + "/pelasko.xlsx");
                }
            }
        }

        private RelayCommand _myCommand5595;
        private string se = PersianDateTime.Now.Year.ToString();

        /// <summary>
        /// Gets the DatabaseChecking.
        /// </summary>
        public RelayCommand DatabaseChecking
        {
            get
            {
                return _myCommand5595
                    ?? (_myCommand5595 = new RelayCommand(ExecuteDatabaseChecking));
            }
        }

        private void ExecuteDatabaseChecking()
        {
            ss.DatabaseChecking();
            MessageBox.Show("دیتابیس آماده شد");
        }
    }
}
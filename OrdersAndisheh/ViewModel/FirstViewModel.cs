using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using OrdersAndisheh.View;
using System.Collections.Generic;

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
        
    }


    //public class NodeSerfaresh
    //{
    //    public string Month { get; set; }
    //    public List<string> Roz { get; set; }
    //}
}
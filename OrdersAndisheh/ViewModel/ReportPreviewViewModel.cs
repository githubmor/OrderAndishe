using BL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OrdersAndisheh.ViewModel
{
    public class ReportPreviewViewModel : ViewModelBase
    {
        
        public ReportPreviewViewModel(List<ReportRow> rows)
        {
            ReportItems = new ObservableCollection<ReportRowViewModel>();
            foreach (var item in rows)
            {
                ReportItems.Add(new ReportRowViewModel(item));
            }
            Messenger.Default.Register<int>(this, "DelThisRow", DelThisRow);
        }

        private void DelThisRow(int obj)
        {
            var o =  ReportItems.Where(p => p.Pos == obj).FirstOrDefault();
            ReportItems.Remove(o);
            RaisePropertyChanged(() => reportRows);
        }

        public ObservableCollection<ReportRowViewModel> ReportItems { get; set; }



        public List<ReportRow> reportRows
        {
            get { return ReportItems.Select(p=>p.item).ToList(); }
             //priset { myVar = value; }
        }
        
    }
}
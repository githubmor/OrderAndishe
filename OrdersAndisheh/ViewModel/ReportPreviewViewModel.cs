using BL;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace OrdersAndisheh.ViewModel
{
    public class ReportPreviewViewModel : ViewModelBase
    {
        
        public ReportPreviewViewModel(List<ReportRow> rows)
        {
            ReportItems = new List<ReportRowViewModel>();
            foreach (var item in rows)
            {
                ReportItems.Add(new ReportRowViewModel(item));
            }
        }

        public List<ReportRowViewModel> ReportItems { get; set; }
    }
}
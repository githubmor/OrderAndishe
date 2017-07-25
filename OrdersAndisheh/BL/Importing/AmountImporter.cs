using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public class AmountImporter : FactoryImporter
    {
        public AmountImporter(string path)
            : base(path)
        {

        }

        public override List<IImportData> GetDataFromPath()
        {
            List<IImportData> items = new List<IImportData>();
            int CodeKalaCol = 5;
            int KalaNameCol = 4;
            int TedadCol = 8;

            for (int i = 1; i < end.Column; i++)
            {
                if (WorkSheet.Cells[1, i].Text == "کد")
                {
                    CodeKalaCol = i;
                }

                if (WorkSheet.Cells[1, i].Text == "مانده (اصلی)")
                {
                    TedadCol = i;
                }

                if (WorkSheet.Cells[1, i].Text == "عنوان")
                {
                    KalaNameCol = i;
                }
            }

            //ExcelAddress dd= WorkSheet.

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                AmountData a = new AmountData();
                a.CodeKala = WorkSheet.Cells[row, CodeKalaCol].Text;
                a.KalaName = WorkSheet.Cells[row, KalaNameCol].Text;
                a.LastAmount = double.Parse(WorkSheet.Cells[row, TedadCol].Text);

                items.Add(a);
            }

            return items;
        }
    }
}

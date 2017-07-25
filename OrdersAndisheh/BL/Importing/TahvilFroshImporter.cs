using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAndisheh.BL.Importing
{
    public class TahvilFroshImporter : FactoryImporter
    {
        public TahvilFroshImporter(string path):base(path)
        {

        }

        
    
        public override List<IImportData> GetDataFromPath()
        {
            List<IImportData> datas = new List<IImportData>();
            int CodeKalaCol = 5;
            int KalaNameCol = 4;
            int TarafeMoghabelCol = 2;
            int TahvilFroshNumCol = 3;
            int TedadCol = 6;
            int TarikhSanadCol = 12;

            for (int i = 1; i < end.Column; i++)
            {
                if (WorkSheet.Cells[1, i].Text == "کد کالا")
                {
                    CodeKalaCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "طرف مقابل")
                {
                    TarafeMoghabelCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "مقدار")
                {
                    TedadCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "شماره سند")
                {
                    TahvilFroshNumCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "تاریخ سند")
                {
                    TarikhSanadCol = i;
                }
                if (WorkSheet.Cells[1, i].Text == "نام کالا")
                {
                    KalaNameCol = i;
                }
            }

           
            for (int row = start.Row + 1; row <= end.Row; row++)
            {

                TahvilFroshData s = new TahvilFroshData();
                s.KalaName = WorkSheet.Cells[row, KalaNameCol].Text;
                s.CodeKala = WorkSheet.Cells[row, CodeKalaCol].Text;
                s.TarafeMoghabel = WorkSheet.Cells[row, TarafeMoghabelCol].Text;
                s.TahvilFroshNum = short.Parse(WorkSheet.Cells[row, TahvilFroshNumCol].Text);
                s.Tedad = Math.Abs(WorkSheet.Cells[row, TedadCol].Text != "" ? int.Parse(WorkSheet.Cells[row, TedadCol].Text, NumberStyles.AllowThousands) : 0);
                s.TarikhSanad = WorkSheet.Cells[row, TarikhSanadCol].Text;
                datas.Add(s);
            }

            return datas;
        }
    }
}

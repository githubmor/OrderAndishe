using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImportingLib;
using System.Reflection;

namespace ImportLibTest1
{
    [TestClass]
    public class ImporterTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNewImporter_throwException_ifPathIsNullOrEmpty()
        {
            Importer im = new Importer("");
            Importer im2 = new Importer(null);
        }
        [TestMethod]
        public void CreateNewImporter_IsOK()
        {
            Importer im = new Importer(@"D:\1.xlsx");

            var r = im.GetSampleData();

            var col = r.Columns;

            Assert.AreEqual("1.xlsx", r.FileName);
            Assert.AreEqual(@"D:\1.xlsx", r.Address);
            Assert.AreEqual(10, r.Columns.Count);

            Assert.AreEqual("کد محصول", col[0].Header);
            Assert.AreEqual("1", col[0].Position);
            Assert.AreEqual("15012007", col[0].Values[0]);

        }
        [TestMethod]
        public void CreateNewImporter_IsOKs()
        {
            Importer im = new Importer(@"D:\1.xlsx");

            var sample = im.GetSampleData();

            var col = sample.Columns;


            col[0].MatchName = "Codekala";
            col[0].MatchName = "Tedad";
            col[0].MatchName = "DriverName";

            var match = sample.GetMatch();

            //ImportData d = new ImportData()
            //{
            //    Codekala = col[0].Position,
            //    Tedad = col[1].Position,
            //    DriverName = col[3].Position
            //};

            var data = im.GetImportDataWithMatch(match, sample.DataRowCount());

            Assert.AreEqual("15012007", data[0].Codekala);
            //Assert.AreEqual(@"D:\1.xlsx", r.Address);
            //Assert.AreEqual(10, r.Columns.Count);

        }
    }
}

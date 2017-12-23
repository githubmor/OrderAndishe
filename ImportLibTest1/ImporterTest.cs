using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImportingLib;
using System.Reflection;

namespace ImportLibTest1
{
    [TestClass]
    public class ImporterTest
    {
        const string path = @"D:\andisheh\Programming\OrdersApp\OrdersAndisheh\ImportLibTest1\1.xlsx";
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

            Importer im = new Importer(path);

            var sample = im.SampleData;

            var col = sample.Columns;

            Assert.AreEqual("1.xlsx", sample.FileName);
            Assert.AreEqual(path, sample.Address);
            Assert.AreEqual(9, sample.Columns.Count);

            Assert.AreEqual("کد محصول", col[0].Header);
            Assert.AreEqual("1", col[0].Position);
            Assert.AreEqual("15012007", col[0].Values[0]);

        }
        [TestMethod]
        public void CreateNewImporter_IsOKs()
        {
            Importer im = new Importer(path);

            var sample = im.SampleData;

            var col = sample.Columns;


            col[0].MatchName = "Codekala";
            col[1].MatchName = "Tedad";
            col[6].MatchName = "DriverName";

            var match = sample.GetMatch();

            //ImportData d = new ImportData()
            //{
            //    Codekala = col[0].Position,
            //    Tedad = col[1].Position,
            //    DriverName = col[3].Position
            //};

            var data = im.GetImportDataWithMatch(match);

            Assert.AreEqual("15012007", data[0].Codekala);
            Assert.AreEqual("144", data[0].Tedad);
            Assert.AreEqual("تيرنژاد", data[0].DriverName);
            
            //Assert.AreEqual(@"D:\1.xlsx", r.Address);
            //Assert.AreEqual(10, r.Columns.Count);

        }
    }
}

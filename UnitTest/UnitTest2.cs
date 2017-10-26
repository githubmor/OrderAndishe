using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrdersAndisheh.ViewModel;
using BL;

namespace UnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TahvilFroshImporting()
        {
            ISefareshService s = new SefareshService();

            TahvilforoshViewModel tds = new TahvilforoshViewModel(s);

            
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImportingLib;
using System.Collections.Generic;

namespace ImportLibTest1
{
    [TestClass]
    public class ColumnTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Column c = new Column();

            Assert.AreEqual(0, c.Values.Count);
            Assert.IsNull(c.Header);
            Assert.IsNull(c.Position);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjRools.Expert.Tests
{
    [TestClass]
    public class ContextTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
        }

        [TestMethod]
        public void GetUndefinedValueAsNull()
        {
            Assert.IsNull(this.context.GetValue("Foo"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            this.context.SetValue("One", 1);
            Assert.AreEqual(1, this.context.GetValue("One"));
        }
    }
}

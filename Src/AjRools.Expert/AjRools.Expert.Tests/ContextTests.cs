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
    }
}

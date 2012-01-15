using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests.Facts
{
    [TestClass]
    public class PropertyIsFactTests
    {
        [TestMethod]
        public void IsSatisfiedByStringLength()
        {
            PropertyIsFact fact = new PropertyIsFact("p", "Length", 3);
            fact.IsSatisfiedByObject("123");
        }

        [TestMethod]
        public void IsSatisfiedByStringInContext()
        {
            Context context = new Context();
            context.SetValue("p", "123");
            PropertyIsFact fact = new PropertyIsFact("p", "Length", 3);
            fact.IsSatisfiedByContext(context);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests.Facts
{
    [TestClass]
    public class IsAFactTests
    {
        [TestMethod]
        public void IsSatisfied()
        {
            IsAFact fact = new IsAFact("p", "IsAFact");
            Assert.IsTrue(fact.IsSatisfiedByValue(fact));
        }

        [TestMethod]
        public void IsSatisfiedByFullName()
        {
            IsAFact fact = new IsAFact("p", "System.Int32");
            Assert.IsTrue(fact.IsSatisfiedByValue(12));
        }

        [TestMethod]
        public void IsNotSatisfied()
        {
            IsAFact fact = new IsAFact("p", "IsFact");
            Assert.IsFalse(fact.IsSatisfiedByValue(fact));
        }
    }
}


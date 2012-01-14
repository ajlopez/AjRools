using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests.Facts
{
    [TestClass]
    public class ComparisonFactTests
    {
        [TestMethod]
        public void LessFact()
        {
            ComparisonFact fact = new ComparisonFact("Temperature", Comparison.Less, 40);

            Assert.AreEqual("Temperature", fact.Name);
            Assert.AreEqual("<", fact.Verb);
            Assert.AreEqual(40, fact.Value);

            Assert.IsTrue(fact.IsSatisfied(38));
            Assert.IsTrue(fact.IsSatisfied(-10));
            Assert.IsFalse(fact.IsSatisfied(40));
            Assert.IsFalse(fact.IsSatisfied(42));

            Assert.IsFalse(fact.IsSatisfied((object)null));
        }

        [TestMethod]
        public void LessEqual()
        {
            ComparisonFact fact = new ComparisonFact("Temperature", Comparison.LessEqual, 40);

            Assert.AreEqual("Temperature", fact.Name);
            Assert.AreEqual("<=", fact.Verb);
            Assert.AreEqual(40, fact.Value);

            Assert.IsTrue(fact.IsSatisfied(38));
            Assert.IsTrue(fact.IsSatisfied(-10));
            Assert.IsTrue(fact.IsSatisfied(40));
            Assert.IsFalse(fact.IsSatisfied(42));

            Assert.IsFalse(fact.IsSatisfied((object)null));
        }

        [TestMethod]
        public void GreaterFact()
        {
            ComparisonFact fact = new ComparisonFact("Temperature", Comparison.Greater, 38);

            Assert.AreEqual("Temperature", fact.Name);
            Assert.AreEqual(">", fact.Verb);
            Assert.AreEqual(38, fact.Value);

            Assert.IsTrue(fact.IsSatisfied(39));
            Assert.IsTrue(fact.IsSatisfied(40));
            Assert.IsFalse(fact.IsSatisfied(38));
            Assert.IsFalse(fact.IsSatisfied(22));

            Assert.IsFalse(fact.IsSatisfied((object)null));
        }

        [TestMethod]
        public void GreaterEqual()
        {
            ComparisonFact fact = new ComparisonFact("Temperature", Comparison.GreaterEqual, 38);

            Assert.AreEqual("Temperature", fact.Name);
            Assert.AreEqual(">=", fact.Verb);
            Assert.AreEqual(38, fact.Value);

            Assert.IsTrue(fact.IsSatisfied(39));
            Assert.IsTrue(fact.IsSatisfied(40));
            Assert.IsTrue(fact.IsSatisfied(38));
            Assert.IsFalse(fact.IsSatisfied(22));

            Assert.IsFalse(fact.IsSatisfied((object)null));
        }
    }
}

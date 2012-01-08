using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests.Facts
{
    [TestClass]
    public class IsNotFactTests
    {
        [TestMethod]
        public void CreateIsNotFact()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);

            Assert.AreEqual("Temperature", fact.Name);
            Assert.AreEqual(38, fact.Value);
            Assert.AreEqual("is_not", fact.Verb);
        }

        [TestMethod]
        public void IsSatisfiedByValue()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);
            Assert.IsTrue(fact.IsSatisfied(30));
        }

        [TestMethod]
        public void IsNotSatisfiedByValue()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);
            Assert.IsFalse(fact.IsSatisfied(38));
        }

        [TestMethod]
        public void IsSatisfiedByContext()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);
            Context context = new Context();
            context.SetValue("Temperature", 30);
            Assert.IsTrue(fact.IsSatisfied(context));
        }

        [TestMethod]
        public void IsNotSatisfiedByContext()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);
            Context context = new Context();
            context.SetValue("Temperature", 38);
            Assert.IsFalse(fact.IsSatisfied(context));
        }

        [TestMethod]
        public void IsSatisfiedByNull()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);
            Assert.IsTrue(fact.IsSatisfied((object) null));
        }

        [TestMethod]
        public void IsNotSatisfiedByNullWhenNull()
        {
            IsNotFact fact = new IsNotFact("Temperature", null);
            Assert.IsFalse(fact.IsSatisfied((object)null));
        }

        [TestMethod]
        public void CompareTwoEqualFacts()
        {
            IsNotFact fact1 = new IsNotFact("Temperature", 38);
            IsNotFact fact2 = new IsNotFact("Temperature", 38);

            Assert.AreEqual(fact1, fact2);
            Assert.AreEqual(fact1.GetHashCode(), fact2.GetHashCode());
        }

        [TestMethod]
        public void CompareTwoEqualFactsWithNullValue()
        {
            IsNotFact fact1 = new IsNotFact("Temperature", null);
            IsNotFact fact2 = new IsNotFact("Temperature", null);

            Assert.AreEqual(fact1, fact2);
            Assert.AreEqual(fact1.GetHashCode(), fact2.GetHashCode());
        }

        [TestMethod]
        public void CompareTwoFactsWithDifferentValues()
        {
            IsNotFact fact1 = new IsNotFact("Temperature", 38);
            IsNotFact fact2 = new IsNotFact("Temperature", 40);

            Assert.AreNotEqual(fact1, fact2);
        }

        [TestMethod]
        public void CompareTwoFactsWithDifferentNames()
        {
            IsNotFact fact1 = new IsNotFact("Temperature", 38);
            IsNotFact fact2 = new IsNotFact("Age", 38);

            Assert.AreNotEqual(fact1, fact2);
        }

        [TestMethod]
        public void CompareTwoFactsWithDifferentVerbs()
        {
            IsNotFact fact1 = new IsNotFact("Temperature", 38);
            IsFact fact2 = new IsFact("Temperature", 38);

            Assert.AreNotEqual(fact1, fact2);
        }

        [TestMethod]
        public void CompareFactWithObject()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);

            Assert.AreNotEqual(fact, 38);
        }

        [TestMethod]
        public void CompareFactWithNull()
        {
            IsNotFact fact = new IsNotFact("Temperature", 38);

            Assert.IsFalse(fact.Equals(null));
        }
    }
}

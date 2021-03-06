﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests.Facts
{
    [TestClass]
    public class IsFactTests
    {
        [TestMethod]
        public void CreateIsFact()
        {
            IsFact fact = new IsFact("Temperature", 38);

            Assert.AreEqual("Temperature", fact.Name);
            Assert.AreEqual(38, fact.Value);
            Assert.AreEqual("is", fact.Verb);
        }

        [TestMethod]
        public void IsSatisfiedByValue()
        {
            IsFact fact = new IsFact("Temperature", 38);
            Assert.IsTrue(fact.IsSatisfiedByValue(38));
        }

        [TestMethod]
        public void IsNotSatisfiedByValue()
        {
            IsFact fact = new IsFact("Temperature", 38);
            Assert.IsFalse(fact.IsSatisfiedByValue(30));
        }

        [TestMethod]
        public void IsSatisfiedByContext()
        {
            IsFact fact = new IsFact("Temperature", 38);
            Context context = new Context();
            context.SetValue("Temperature", 38);
            Assert.IsTrue(fact.IsSatisfiedByContext(context));
        }

        [TestMethod]
        public void IsNotSatisfiedByContext()
        {
            IsFact fact = new IsFact("Temperature", 38);
            Context context = new Context();
            Assert.IsFalse(fact.IsSatisfiedByContext(context));
        }

        [TestMethod]
        public void IsNotSatisfiedByNull()
        {
            IsFact fact = new IsFact("Temperature", 38);
            Assert.IsFalse(fact.IsSatisfiedByValue(null));
        }

        [TestMethod]
        public void IsSatisfiedByNullWhenNull()
        {
            IsFact fact = new IsFact("Temperature", null);
            Assert.IsTrue(fact.IsSatisfiedByValue(null));
        }

        [TestMethod]
        public void CompareTwoEqualFacts()
        {
            IsFact fact1 = new IsFact("Temperature", 38);
            IsFact fact2 = new IsFact("Temperature", 38);

            Assert.AreEqual(fact1, fact2);
            Assert.AreEqual(fact1.GetHashCode(), fact2.GetHashCode());
        }

        [TestMethod]
        public void CompareTwoEqualFactsWithNullValue()
        {
            IsFact fact1 = new IsFact("Temperature", null);
            IsFact fact2 = new IsFact("Temperature", null);

            Assert.AreEqual(fact1, fact2);
            Assert.AreEqual(fact1.GetHashCode(), fact2.GetHashCode());
        }

        [TestMethod]
        public void CompareTwoFactsWithDifferentValues()
        {
            IsFact fact1 = new IsFact("Temperature", 38);
            IsFact fact2 = new IsFact("Temperature", 40);

            Assert.AreNotEqual(fact1, fact2);
        }

        [TestMethod]
        public void CompareTwoFactsWithDifferentNames()
        {
            IsFact fact1 = new IsFact("Temperature", 38);
            IsFact fact2 = new IsFact("Age", 38);

            Assert.AreNotEqual(fact1, fact2);
        }

        [TestMethod]
        public void CompareTwoFactsWithDifferentVerbs()
        {
            IsFact fact1 = new IsFact("Temperature", 38);
            IsNotFact fact2 = new IsNotFact("Temperature", 38);

            Assert.AreNotEqual(fact1, fact2);
        }

        [TestMethod]
        public void CompareFactWithObject()
        {
            IsFact fact = new IsFact("Temperature", 38);

            Assert.AreNotEqual(fact, 38);
        }

        [TestMethod]
        public void CompareFactWithNull()
        {
            IsFact fact = new IsFact("Temperature", 38);

            Assert.IsFalse(fact.Equals(null));
        }
    }
}

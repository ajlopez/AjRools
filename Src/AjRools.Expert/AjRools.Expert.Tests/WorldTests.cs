using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests
{
    [TestClass]
    public class WorldTests
    {
        private World world;

        [TestInitialize]
        public void Setup()
        {
            this.world = new World();
            this.world.AssertFact(new IsFact("Temperature", 38));
        }

        [TestMethod]
        public void FalseFact()
        {
            Fact fact = new IsFact("Temperature", 42);
            Assert.IsFalse(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void FalseComparisonFact()
        {
            Fact fact = new ComparisonFact("Temperature", Comparison.Greater, 42);
            Assert.IsFalse(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void TrueFact()
        {
            Fact fact = new IsFact("Temperature", 38);
            Assert.IsTrue(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void TrueComparisonFact()
        {
            Fact fact = new ComparisonFact("Temperature", Comparison.GreaterEqual, 38);
            Assert.IsTrue(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void RetractAndTestFact()
        {
            Fact fact = new IsFact("Temperature", 38);
            this.world.RetractFact(fact);
            Assert.IsFalse(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void AssertAndTestComparisonFact()
        {
            Fact fact = new ComparisonFact("Temperature", Comparison.GreaterEqual, 38);
            this.world.AssertFact(fact);
            Assert.IsTrue(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void AssertAndRetractAndTestComparisonFact()
        {
            Fact fact = new ComparisonFact("Pressure", Comparison.GreaterEqual, 20);
            this.world.AssertFact(fact);
            this.world.RetractFact(fact);
            Assert.IsFalse(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void AssertTwiceRetractAndTestFact()
        {
            Fact fact = new IsFact("Age", 40);
            this.world.AssertFact(fact);
            this.world.AssertFact(new IsFact("Age", 40));
            this.world.RetractFact(fact);
            Assert.IsFalse(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void AssertTwiceAndTestComparisonFact()
        {
            Fact fact = new ComparisonFact("Age", Comparison.GreaterEqual, 40);
            this.world.AssertFact(fact);
            this.world.AssertFact(new ComparisonFact("Age", Comparison.GreaterEqual, 40));
            Assert.IsTrue(this.world.IsAFact(fact));
        }

        [TestMethod]
        public void IsSatisfiedByWorldContext()
        {
            Fact fact = new IsNotFact("Temperature", 20);
            Assert.IsTrue(this.world.IsAFact(fact));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseWhenRetractFalseFact()
        {
            Fact fact = new IsFact("Age", 40);
            this.world.RetractFact(fact);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseWhenRetractFalseComparisonFact()
        {
            Fact fact = new ComparisonFact("Age", Comparison.Greater, 40);
            this.world.RetractFact(fact);
        }
    }
}

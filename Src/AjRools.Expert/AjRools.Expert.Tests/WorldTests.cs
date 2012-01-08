﻿using System;
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
        public void TrueFact()
        {
            Fact fact = new IsFact("Temperature", 38);
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
        public void AssertTwiceRetractAndTestFact()
        {
            Fact fact = new IsFact("Age", 40);
            this.world.AssertFact(fact);
            this.world.AssertFact(new IsFact("Age", 40));
            this.world.RetractFact(fact);
            Assert.IsFalse(this.world.IsAFact(fact));
        }
    }
}

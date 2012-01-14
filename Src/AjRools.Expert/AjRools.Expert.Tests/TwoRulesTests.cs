using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Compiler;
using System.IO;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests
{
    [TestClass]
    [DeploymentItem("Files\\TwoRules.txt")]
    public class TwoRulesTests
    {
        private World world;
        private Fact hasfever;

        [TestInitialize]
        public void Setup()
        {
            this.world = new World();
            this.hasfever = new IsFact("HasFever", true);

            Parser parser = new Parser(new StreamReader("TwoRules.txt"));

            foreach (var rule in parser.ParseRules())
                this.world.AddRule(rule);
        }

        [TestMethod]
        public void RunNoFever()
        {
            this.world.Run();
            Assert.IsFalse(this.HasFever());
        }

        [TestMethod]
        public void AssertTemperatureRunNoFever()
        {
            this.world.AssertFact(new IsFact("Temperature", 40));
            this.world.Run();
            Assert.IsFalse(this.HasFever());
        }

        [TestMethod]
        public void AssertTemperatureAgeRunHasFever()
        {
            this.world.AssertFact(new IsFact("Temperature", 40));
            this.world.AssertFact(new IsFact("Age", 30));
            this.world.Run();
            Assert.IsTrue(this.HasFever());
        }

        [TestMethod]
        public void AssertTemperatureAgeRunHasNoFever()
        {
            this.world.AssertFact(new IsFact("Temperature", 36));
            this.world.AssertFact(new IsFact("Age", 30));
            this.world.Run();
            Assert.IsFalse(this.HasFever());
        }

        [TestMethod]
        public void AssertTemperatureChildAgeRunHasNoFever()
        {
            this.world.AssertFact(new IsFact("Temperature", 38));
            this.world.AssertFact(new IsFact("Age", 11));
            this.world.Run();
            Assert.IsFalse(this.HasFever());
        }

        [TestMethod]
        public void AssertTemperatureAdultAgeRunHasNoFever()
        {
            this.world.AssertFact(new IsFact("Temperature", 38));
            this.world.AssertFact(new IsFact("Age", 30));
            this.world.Run();
            Assert.IsTrue(this.HasFever());
        }

        [TestMethod]
        public void AssertTemperatureAgeRunRetractAgeRunHasNoFever()
        {
            this.world.AssertFact(new IsFact("Temperature", 38));
            this.world.AssertFact(new IsFact("Age", 30));
            this.world.Run();
            Assert.IsTrue(this.HasFever());
            this.world.RetractFact(new IsFact("Temperature", 38));
            this.world.Run();
            Assert.IsFalse(this.HasFever());
        }

        private bool HasFever()
        {
            return this.world.IsAFact(this.hasfever);
        }
    }
}

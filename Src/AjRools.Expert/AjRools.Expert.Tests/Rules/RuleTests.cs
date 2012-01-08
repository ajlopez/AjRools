using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Rules;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests.Rules
{
    [TestClass]
    public class RuleTests
    {
        [TestMethod]
        public void RuleNotFireInEmptyWorld()
        {
            Rule rule = new Rule(new Fact[] {
                new IsFact("Temperature", 40),
                new IsFact("Age", 50)
            }, null);

            World world = new World();

            Assert.IsFalse(rule.FireIfReady(world));
        }

        [TestMethod]
        public void RuleFire()
        {
            Rule rule = new Rule(new Fact[] {
                new IsFact("Temperature", 40),
                new IsFact("Age", 50)
            }, new Fact[] {
                new IsFact("HasFever", true)
            });

            World world = new World();
            world.AssertFact(new IsFact("Age", 50));
            world.AssertFact(new IsFact("Temperature", 40));

            Assert.IsTrue(rule.FireIfReady(world));
            Assert.IsTrue(world.IsAFact(new IsFact("HasFever", true)));
        }

        [TestMethod]
        public void RuleNotFire()
        {
            Rule rule = new Rule(new Fact[] {
                new IsFact("Temperature", 40),
                new IsFact("Age", 50)
            }, null);

            World world = new World();
            world.AssertFact(new IsFact("Age", 60));
            world.AssertFact(new IsFact("Temperature", 40));

            Assert.IsFalse(rule.FireIfReady(world));
        }
    }
}

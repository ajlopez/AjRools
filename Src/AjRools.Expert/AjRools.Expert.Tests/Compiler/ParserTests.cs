using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Compiler;
using AjRools.Expert.Rules;
using AjRools.Expert.Facts;
using System.IO;

namespace AjRools.Expert.Tests.Compiler
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseSimpleRule()
        {
            Parser parser = new Parser("rule\r\nwhen\r\nTemperature is 39\r\nthen\r\nHasFever is true\r\nend");

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new IsFact("Temperature", 39)));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));
        }

        [TestMethod]
        public void ParseSimpleRuleWithImplicitIsTrue()
        {
            Parser parser = new Parser("rule\r\nwhen\r\nTemperature is 39\r\nthen\r\nHasFever\r\nend");

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new IsFact("Temperature", 39)));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));
        }

        [TestMethod]
        public void ParseRuleWithString()
        {
            Parser parser = new Parser("rule\r\nwhen\r\nTemperatureLevel is \"High\"\r\nthen\r\nHasFever is true\r\nend\r\n");

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new IsFact("TemperatureLevel", "High")));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));
        }

        [TestMethod]
        public void ParseRuleWithComparisonFact()
        {
            Parser parser = new Parser("rule\r\nwhen\r\nTemperatureLevel >= 38\r\nthen\r\nHasFever is true\r\nend\r\n");

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new ComparisonFact("TemperatureLevel", Comparison.GreaterEqual, 38)));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));
        }

        [TestMethod]
        public void RaiseWhenNoRule()
        {
            Parser parser = new Parser(new StringReader("foo"));
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Expected 'rule'");
        }

        [TestMethod]
        public void RaiseWhenNoCondition()
        {
            Parser parser = new Parser("rule\r\nwhen\r\n");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Unexpected End of Input");
        }

        [TestMethod]
        public void RaiseWhenNoValueInCondition()
        {
            Parser parser = new Parser("rule\r\nwhen\r\na is");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Unexpected End of Input");
        }

        [TestMethod]
        public void RaiseWhenUnknownVerb()
        {
            Parser parser = new Parser("rule\r\nwhen\r\na foo b");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Unexpected 'foo'");
        }

        [TestMethod]
        public void RaiseWhenConditionIsAtLastPosition()
        {
            Parser parser = new Parser("rule\r\nwhen\r\na");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Unexpected End of Input");
        }

        [TestMethod]
        public void RaiseWhenVerbIsANumber()
        {
            Parser parser = new Parser("rule\r\nwhen\r\na 1 b\r\nthen\r\na\r\nend");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Unexpected '1'");
        }

        [TestMethod]
        public void RaiseWhenUnexpectedValueInCondition()
        {
            Parser parser = new Parser("rule\r\nwhen\r\na is <=");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Unexpected '<='");
        }

        [TestMethod]
        public void RaiseWhenNoNameAtStartOfCondition()
        {
            Parser parser = new Parser("rule\r\nwhen\r\n1 is a");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Expected Name");
        }

        [TestMethod]
        public void RaiseWhenNoWhenInRule()
        {
            Parser parser = new Parser("rule\r\n1 is a");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Expected 'when'");
        }

        [TestMethod]
        public void RaiseWhenTwoManyWordsInCondition()
        {
            Parser parser = new Parser("rule\r\nwhen\r\na is 48 c");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(), "Expected End of Line/Input");
        }

        [TestMethod]
        [DeploymentItem("Files\\SimpleRule.txt")]
        public void ParseSimpleRuleFile()
        {
            Parser parser = new Parser(new StreamReader("SimpleRule.txt"));

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new IsFact("Temperature", 39)));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));

            Assert.IsNull(parser.ParseRule());
        }

        [TestMethod]
        [DeploymentItem("Files\\SimpleRuleWithIsNotCondition.txt")]
        public void ParseSimpleRuleFileWithIsNotCondition()
        {
            Parser parser = new Parser(new StreamReader("SimpleRuleWithIsNotCondition.txt"));

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new IsNotFact("Temperature", 39)));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));

            Assert.IsNull(parser.ParseRule());
        }

        [TestMethod]
        [DeploymentItem("Files\\SimpleComparisonRule.txt")]
        public void ParseSimpleConditionRule()
        {
            Parser parser = new Parser(new StreamReader("SimpleComparisonRule.txt"));

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new ComparisonFact("Temperature", Comparison.Greater, 37)));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));

            Assert.IsNull(parser.ParseRule());
        }
    }
}

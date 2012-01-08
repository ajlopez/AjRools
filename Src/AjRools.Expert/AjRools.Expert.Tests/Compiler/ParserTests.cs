using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Compiler;
using AjRools.Expert.Rules;
using AjRools.Expert.Facts;

namespace AjRools.Expert.Tests.Compiler
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseSimpleRule()
        {
            Parser parser = new Parser("rule\r\nwhen\r\nTemperature is 39\r\nthen\r\nHasFever is true\r\nend\r\n");

            Rule rule = parser.ParseRule();

            Assert.IsNotNull(rule);

            Assert.IsTrue(rule.Conditions.Contains(new IsFact("Temperature", 39)));
            Assert.IsTrue(rule.Assertions.Contains(new IsFact("HasFever", true)));
        }

        [TestMethod]
        public void RaiseWhenNoRule()
        {
            Parser parser = new Parser("foo");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(),
            "Expected 'rule'");
        }

        [TestMethod]
        public void RaiseWhenTwoManyWordsInCondition()
        {
            Parser parser = new Parser("rule\r\nwhen\r\na is 48 c");
            MyAssert.Throws<LexerException>(() => parser.ParseRule(), "Expected End of Line/Input");
        }
    }
}

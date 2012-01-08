using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Expressions;

namespace AjRools.Expert.Tests.Expressions
{
    [TestClass]
    public class ConstantExpressionTests
    {
        [TestMethod]
        public void CreateIntegerConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression(1);
            Assert.AreEqual(1, expr.Value);
        }

        [TestMethod]
        public void EvaluateStringConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression("Adam");
            Assert.AreEqual("Adam", expr.Evaluate(null));
        }
    }
}

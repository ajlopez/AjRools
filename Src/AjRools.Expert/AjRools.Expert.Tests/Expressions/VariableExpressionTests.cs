using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Expressions;

namespace AjRools.Expert.Tests.Expressions
{
    [TestClass]
    public class VariableExpressionTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
            this.context.SetValue("One", 1);
            this.context.SetValue("Two", 2);
        }

        [TestMethod]
        public void EvaluateUndefinedVariable()
        {
            VariableExpression expr = new VariableExpression("Foo");
            Assert.IsNull(expr.Evaluate(this.context));
        }

        [TestMethod]
        public void EvaluateDefinedVariable()
        {
            VariableExpression expr = new VariableExpression("One");
            Assert.AreEqual(1, expr.Evaluate(this.context));
        }
    }
}

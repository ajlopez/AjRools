using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjRools.Expert.Tests
{
    // Based on http://blog.drorhelper.com/2009/08/checking-expected-exception-message.html

    public static class MyAssert
    {
        public static void Throws<T>(Action action, string message) where T : Exception
        {
            try
            {
                action.Invoke();

                Assert.Fail(string.Format("Expected Exception of Type {0}", typeof(T)));
            }
            catch (T exception)
            {
                Assert.AreEqual(message, exception.Message);
            }
        }
    }
}

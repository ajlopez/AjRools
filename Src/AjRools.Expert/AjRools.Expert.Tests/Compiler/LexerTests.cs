using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjRools.Expert.Compiler;
using AjRools.Expert.Tests;

namespace AjLang.Tests.Compiler
{
    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("foo");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  foo   ");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.Name, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetGreaterOperator()
        {
            Lexer lexer = new Lexer(">");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(">", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetLessOrEqualAsOperator()
        {
            Lexer lexer = new Lexer("<=");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("<=", token.Value);
            Assert.AreEqual(TokenType.Operator, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("123", token.Value);
            Assert.AreEqual(TokenType.Integer, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSimpleString()
        {
            Lexer lexer = new Lexer("\"foo\"");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual("foo", token.Value);
            Assert.AreEqual(TokenType.String, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNewLineAsEndOfLine()
        {
            Lexer lexer = new Lexer("\n");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetCarriageReturnNewLineAsEndOfLine()
        {
            Lexer lexer = new Lexer("\r\n");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNewLineCarriageReturnAsEndOfLine()
        {
            Lexer lexer = new Lexer("\n\r");
            Token token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void RaiseWhenInvalidCharacter()
        {
            Lexer lexer = new Lexer("[");
            MyAssert.Throws<LexerException>(() => lexer.NextToken(),
                "Unexpected '['");
        }

        [TestMethod]
        public void RaiseWhenUnclosedString()
        {
            Lexer lexer = new Lexer("\"foo");
            MyAssert.Throws<LexerException>(() => lexer.NextToken(),
                "Unclosed String");
        }

        [TestMethod]
        public void ParseSimpleCondition()
        {
            Lexer lexer = new Lexer("Temperature > 36");

            Assert.IsTrue(IsName("Temperature", lexer.NextToken()));
            Assert.IsTrue(IsOperator(">", lexer.NextToken()));
            Assert.IsTrue(IsInteger("36", lexer.NextToken()));

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseSimpleConditionWithNumbers()
        {
            Lexer lexer = new Lexer("30 < 36");

            Assert.IsTrue(IsInteger("30", lexer.NextToken()));
            Assert.IsTrue(IsOperator("<", lexer.NextToken()));
            Assert.IsTrue(IsInteger("36", lexer.NextToken()));

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ParseBooleans()
        {
            Lexer lexer = new Lexer("true false");

            Assert.IsTrue(IsBoolean("true", lexer.NextToken()));
            Assert.IsTrue(IsBoolean("false", lexer.NextToken()));

            Assert.IsNull(lexer.NextToken());
        }

        private static bool IsName(string value, Token token)
        {
            return IsToken(TokenType.Name, value, token);
        }

        private static bool IsOperator(string value, Token token)
        {
            return IsToken(TokenType.Operator, value, token);
        }

        private static bool IsBoolean(string value, Token token)
        {
            return IsToken(TokenType.Boolean, value, token);
        }

        private static bool IsInteger(string value, Token token)
        {
            return IsToken(TokenType.Integer, value, token);
        }

        private static bool IsToken(TokenType type, string value, Token token)
        {
            if (token == null)
                return false;

            return token.Type == type && token.Value == value;
        }
    }
}

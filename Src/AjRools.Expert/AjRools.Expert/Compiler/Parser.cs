namespace AjRools.Expert.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using AjRools.Expert.Rules;
    using AjRools.Expert.Facts;

    public class Parser
    {
        private Lexer lexer;

        private Stack<Token> tokens = new Stack<Token>();

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public Parser(TextReader reader)
            : this(new Lexer(reader))
        {
        }

        public Parser(Lexer lexer)            
        {
            this.lexer = lexer;
        }

        public Rule ParseRule()
        {
            this.ParseName("rule");
            this.ParseEndOfLine();

            this.ParseName("when");
            this.ParseEndOfLine();

            IList<Fact> conditions = this.ParseFacts();

            this.ParseName("then");
            this.ParseEndOfLine();

            IList<Fact> assertions = this.ParseFacts();

            this.ParseName("end");
            this.ParseEndOfLine();

            return new Rule(conditions, assertions);
        }

        private IList<Fact> ParseFacts()
        {
            IList<Fact> facts = new List<Fact>();

            for (Fact fact = this.ParseFact(); fact != null; fact = this.ParseFact())
                facts.Add(fact);

            return facts;
        }

        private Fact ParseFact()
        {
            string name = this.ParseName();

            if (name == "then" || name == "end")
            {
                this.PushToken(name);
                return null;
            }

            this.ParseName("is");
            object value = this.ParseValue();

            this.ParseEndOfLine();

            return new IsFact(name, value);
        }

        private string ParseName()
        {
            Token token = this.NextToken();

            if (token == null)
                throw new LexerException("Unexpected End Of Input");

            if (token.Type != TokenType.Name)
                throw new LexerException("Expected Name");

            return token.Value;
        }

        private object ParseValue()
        {
            Token token = this.NextToken();

            if (token == null)
                throw new LexerException("Unexpected End Of Input");

            if (token.Type == TokenType.String)
                return token.Value;

            if (token.Type == TokenType.Boolean)
                return Boolean.Parse(token.Value);

            if (token.Type == TokenType.Integer)
                return Int32.Parse(token.Value);

            throw new LexerException(string.Format("Unexpected '{0}'", token.Value));
        }

        private void ParseName(string name)
        {
            Token token = this.NextToken();

            if (token == null || token.Type != TokenType.Name || token.Value != name)
                throw new LexerException(string.Format("Expected '{0}'", name));
        }

        private void ParseEndOfLine()
        {
            Token token = this.NextToken();

            if (token != null && token.Type != TokenType.EndOfLine)
                throw new LexerException("Expected End of Line/Input");
        }

        private Token NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Pop();

            return this.lexer.NextToken();
        }

        private void PushToken(string name)
        {
            this.tokens.Push(new Token(name, TokenType.Name));
        }
    }
}


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
            this.SkipBlankLines();
            Token token = this.NextToken();

            if (token == null)
                return null;

            this.PushToken(token);
            this.ParseWordLine("rule");

            this.ParseWordLine("when");

            IList<Fact> conditions = this.ParseFacts();

            this.ParseWordLine("then");

            IList<Fact> assertions = this.ParseFacts();

            this.ParseWordLine("end");

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

            Token token = this.NextToken();

            if (token == null || token.Type == TokenType.EndOfLine)
                return new IsFact(name, true);

            if (token.Type != TokenType.Name || (token.Value != "is" && token.Value != "is_not"))
                throw new LexerException(string.Format("Unexpected '{0}'", token.Value));

            string verb = token.Value;

            object value = this.ParseValue();

            this.ParseEndOfLine();

            if (verb == "is")
                return new IsFact(name, value);
            else 
                return new IsNotFact(name, value);
        }

        private string ParseName()
        {
            Token token = this.NextToken();

            if (token == null)
                throw new LexerException("Unexpected End of Input");

            if (token.Type != TokenType.Name)
                throw new LexerException("Expected Name");

            return token.Value;
        }

        private object ParseValue()
        {
            Token token = this.NextToken();

            if (token == null)
                throw new LexerException("Unexpected End of Input");

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

        private void ParseWordLine(string word)
        {
            this.SkipBlankLines();
            this.ParseName(word);
            this.ParseEndOfLine();
        }

        private void SkipBlankLines()
        {
            Token token;

            for (token = this.NextToken(); token != null && token.Type == TokenType.EndOfLine; token = this.NextToken())
                ;

            this.PushToken(token);
        }

        private void ParseEndOfLine()
        {
            Token token = this.NextToken();

            if (token == null)
                return;

            if (token.Type != TokenType.EndOfLine)
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

        private void PushToken(Token token)
        {
            this.tokens.Push(token);
        }
    }
}


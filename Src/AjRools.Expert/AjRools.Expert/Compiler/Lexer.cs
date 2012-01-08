namespace AjRools.Expert.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class Lexer
    {
        private static string[] operators = { ">", "<", ">=", "<=" };

        private Stack<int> characters = new Stack<int>();

        private TextReader reader;

        public Lexer(string text)
            : this(new StringReader(text))
        {
        }

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Token NextToken()
        {
            int ich = this.NextChar();

            while (ich != -1 && char.IsWhiteSpace((char)ich))
            {
                if (((char)ich) == '\n')
                    return new Token("\n", TokenType.EndOfLine);

                ich = this.NextChar();
            }

            if (ich == -1)
                return null;

            char ch = (char)ich;

            if (ch == '"')
                return this.NextString();

            if (char.IsLetter(ch))
                return this.NextName(ch);

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            if (operators.Any(op => op[0] == ch))
                return this.NextOperator(ch);

            throw new LexerException(string.Format("Unexpected '{0}'", ch));
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();
            int ich;

            for (ich = this.NextChar(); ich != -1 && char.IsLetterOrDigit((char)ich); ich = this.reader.Read())
                value += (char)ich;

            if (ich != -1)
                this.PushChar(ich);

            if (value == "false" || value == "true")
                return new Token(value, TokenType.Boolean);

            return new Token(value, TokenType.Name);
        }

        private Token NextString()
        {
            string value = "";
            int ich;

            for (ich = this.NextChar(); ich != -1 && ((char)ich) != '"'; ich = this.reader.Read())
                value += (char)ich;

            if (ich == -1)
                throw new LexerException("Unclosed String");

            return new Token(value, TokenType.String);
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();
            int ich;

            for (ich = this.NextChar(); ich != -1 && char.IsDigit((char)ich); ich = this.reader.Read())
                value += (char)ich;

            if (ich != -1)
                this.PushChar(ich);

            return new Token(value, TokenType.Integer);
        }

        private Token NextOperator(char ch)
        {
            // TODO improve/fix algorithm, multicharacter operators
            string value = ch.ToString();
            int ich = this.NextChar();

            if (ich != -1)
            {
                string value2 = value + (char)ich;

                if (operators.Contains(value2))
                    return new Token(value2, TokenType.Operator);

                this.PushChar(ich);
            }

            return new Token(ch.ToString(), TokenType.Operator);
        }

        private int NextChar()
        {
            if (this.characters.Count > 0)
                return this.characters.Pop();

            return this.reader.Read();
        }

        private void PushChar(int ich)
        {
            this.characters.Push(ich);
        }
    }
}

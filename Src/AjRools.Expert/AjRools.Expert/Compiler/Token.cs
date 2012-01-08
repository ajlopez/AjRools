namespace AjRools.Expert.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Token
    {
        public string value;
        public TokenType type;

        public Token(string value, TokenType type)
        {
            this.value = value;
            this.type = type;
        }

        public string Value { get { return this.value; } }

        public TokenType Type { get { return this.type; } }
    }
}



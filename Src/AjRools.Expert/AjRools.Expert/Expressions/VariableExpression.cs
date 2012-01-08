namespace AjRools.Expert.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VariableExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public object Evaluate(Context context)
        {
            return context.GetValue(this.name);
        }
    }
}

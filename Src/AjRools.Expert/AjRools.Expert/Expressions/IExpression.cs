using System;

namespace AjRools.Expert.Expressions
{
    public interface IExpression
    {
        object Evaluate(AjRools.Expert.Context context);
    }
}

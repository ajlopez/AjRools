namespace AjRools.Expert.Facts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IsNotFact : NameVerbValueFact
    {
        public IsNotFact(string name, object value)
            : base(name, "is_not", value)
        {
        }

        public override bool IsSatisfied(object value)
        {
            if (this.Value == null)
                return value != null;

            return !this.Value.Equals(value);
        }
    }
}


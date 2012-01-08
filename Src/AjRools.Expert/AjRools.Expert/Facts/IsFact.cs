namespace AjRools.Expert.Facts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IsFact : NameVerbValueFact
    {
        public IsFact(string name, object value)
            : base(name, "is", value)
        {
        }
    }
}


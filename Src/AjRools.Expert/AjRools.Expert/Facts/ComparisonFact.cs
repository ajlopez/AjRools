namespace AjRools.Expert.Facts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ComparisonFact : NameVerbValueFact
    {
        private static IDictionary<Comparison, Func<IComparable, object, bool>> predicates = new Dictionary<Comparison, Func<IComparable, object, bool>>
        {
            { Comparison.Less, (obj1, obj2) => obj1.CompareTo(obj2) < 0 },
            { Comparison.LessEqual, (obj1, obj2) => obj1.CompareTo(obj2) <= 0 },
            { Comparison.Greater, (obj1, obj2) => obj1.CompareTo(obj2) > 0 },
            { Comparison.GreaterEqual, (obj1, obj2) => obj1.CompareTo(obj2) >= 0 }
        };

        private static IDictionary<Comparison, string> verbs = new Dictionary<Comparison, string>()
        {
            { Comparison.Less, "<" },
            { Comparison.LessEqual, "<=" },
            { Comparison.Greater, ">" },
            { Comparison.GreaterEqual, ">=" }
        };

        private Comparison comparison;

        public ComparisonFact(string name, Comparison comparison, object value)
            : base(name, verbs[comparison], value)
        {
            this.comparison = comparison;
        }

        public override bool IsSatisfied(object value)
        {
            if (value == null)
                return false;

            return predicates[this.comparison]((IComparable) value, this.Value);
        }
    }
}


namespace AjRools.Expert.Facts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class NameVerbValueFact : Fact
    {
        private string name;
        private string verb;
        private object value;

        public NameVerbValueFact(string name, string verb, object value)
        {
            this.name = name;
            this.verb = verb;
            this.value = value;
        }

        public string Name { get { return this.name; } }

        public string Verb { get { return this.verb; } }

        public object Value { get { return this.value; } }

        public bool IsSatisfied(Context context)
        {
            return this.IsSatisfied(context.GetValue(this.name));
        }

        public abstract bool IsSatisfied(object value);

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is NameVerbValueFact))
                return false;

            NameVerbValueFact fact = (NameVerbValueFact)obj;

            if (this.name != fact.name)
                return false;

            if (this.verb != fact.verb)
                return false;

            if (this.value == null)
                return fact.value == null;

            if (!this.value.Equals(fact.value))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            if (this.value == null)
                return this.name.GetHashCode();

            return (this.name.GetHashCode() * 17 + this.verb.GetHashCode()) *13 + this.value.GetHashCode();
        }
    }
}

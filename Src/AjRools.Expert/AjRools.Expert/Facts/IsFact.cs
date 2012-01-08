namespace AjRools.Expert.Facts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IsFact : Fact
    {
        private string name;
        private object value;

        public IsFact(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name { get { return this.name; } }

        public object Value { get { return this.value; } }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is IsFact))
                return false;

            IsFact fact = (IsFact)obj;

            if (this.name != fact.name)
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

            return this.name.GetHashCode() * 17 + this.value.GetHashCode();
        }
    }
}


namespace AjRools.Expert.Facts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IsAFact : Fact
    {
        private string name;
        private string type;

        public IsAFact(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public bool IsSatisfiedByValue(object value)
        {
            Type type = value.GetType();

            if (type.Name.Equals(this.type))
                return true;

            if (type.FullName.Equals(this.type))
                return true;

            return false;
        }
    }
}


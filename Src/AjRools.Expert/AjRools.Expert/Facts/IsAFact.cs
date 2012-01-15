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
            return IsSatisfiedByType(value.GetType());
        }

        private bool IsSatisfiedByType(Type type)
        {
            if (type.Name.Equals(this.type))
                return true;

            if (type.FullName.Equals(this.type))
                return true;

            if (type.BaseType != null)
                if (this.IsSatisfiedByType(type.BaseType))
                    return true;

            foreach (var @interface in type.GetInterfaces())
                if (this.IsSatisfiedByType(@interface))
                    return true;

            return false;
        }
    }
}


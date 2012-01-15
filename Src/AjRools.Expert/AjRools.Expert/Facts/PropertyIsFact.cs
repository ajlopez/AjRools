namespace AjRools.Expert.Facts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;

    public class PropertyIsFact
    {
        private string name;
        private string property;
        private object value;

        public PropertyIsFact(string name, string property, object value)
        {
            this.name = name;
            this.property = property;
            this.value = value;
        }

        public bool IsSatisfiedByContext(Context context)
        {
            return this.IsSatisfiedByObject(context.GetValue(this.name));
        }

        public bool IsSatisfiedByObject(object @object)
        {
            Type type = @object.GetType();
            PropertyInfo pinfo = type.GetProperty(this.property);
            return pinfo.GetValue(@object, null).Equals(this.value);
        }
    }
}

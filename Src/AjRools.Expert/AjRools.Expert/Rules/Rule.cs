namespace AjRools.Expert.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjRools.Expert.Facts;

    public class Rule
    {
        private IList<Fact> conditions;
        private IList<Fact> assertions;

        public Rule(IList<Fact> conditions, IList<Fact> assertions)
        {
            this.conditions = conditions;
            this.assertions = assertions;
        }

        public bool IsReadyToFire(World world)
        {
            foreach (var condition in this.conditions)
                if (!world.IsAFact(condition))
                    return false;

            return true;
        }
    }
}

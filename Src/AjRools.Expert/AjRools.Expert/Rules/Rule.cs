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

        public IEnumerable<Fact> Conditions { get { return this.conditions; } }

        public IEnumerable<Fact> Assertions { get { return this.assertions; } }

        public bool FireIfReady(World world)
        {
            if (!this.IsReadyToFire(world))
                return false;

            this.Fire(world);

            return true;
        }

        private void Fire(World world)
        {
            foreach (var assertion in this.assertions)
                world.AssertFact(assertion);
        }

        private bool IsReadyToFire(World world)
        {
            foreach (var condition in this.conditions)
                if (!world.IsAFact(condition))
                    return false;

            return true;
        }
    }
}

namespace AjRools.Expert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjRools.Expert.Facts;

    public class World
    {
        private IList<Fact> facts = new List<Fact>();

        public void AssertFact(Fact fact)
        {
            this.facts.Add(fact);
        }

        public bool IsAFact(Fact fact)
        {
            return this.facts.Contains(fact);
        }
    }
}


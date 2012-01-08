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
        private Context context = new Context();

        public void AssertFact(Fact fact)
        {
            if (this.facts.Contains(fact))
                return;

            this.facts.Add(fact);

            if (fact is IsFact)
            {
                IsFact isfact = (IsFact)fact;

                this.context.SetValue(isfact.Name, isfact.Value);
            }                
        }

        public void RetractFact(Fact fact)
        {
            if (!this.facts.Contains(fact))
                throw new InvalidOperationException();

            this.facts.Remove(fact);

            if (fact is IsFact)
            {
                IsFact isfact = (IsFact)fact;
                // TODO control current value, first
                this.context.SetValue(isfact.Name, null);
            }
        }

        public bool IsAFact(Fact fact)
        {
            if (this.facts.Contains(fact))
                return true;

            NameVerbValueFact namefact = (NameVerbValueFact)fact;

            return namefact.IsSatisfied(this.context);
        }
    }
}


namespace AjRools.Expert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjRools.Expert.Facts;
    using AjRools.Expert.Rules;

    public class World
    {
        private IList<Fact> facts = new List<Fact>();
        private IList<Rule> rules = new List<Rule>();
        private Context context = new Context();

        private Queue<Fact> asserted = new Queue<Fact>();

        public void AssertFact(Fact fact)
        {
            if (fact is IsFact)
            {
                IsFact isfact = (IsFact)fact;

                this.context.SetValue(isfact.Name, isfact.Value);

                this.asserted.Enqueue(fact);

                return;
            }

            if (this.facts.Contains(fact))
                return;

            this.facts.Add(fact);
            this.asserted.Enqueue(fact);
        }

        public void RetractFact(Fact fact)
        {

            if (fact is IsFact)
            {
                IsFact isfact = (IsFact)fact;

                object current = this.context.GetValue(isfact.Name);

                if (current == null || !current.Equals(isfact.Value))
                    throw new InvalidOperationException();

                this.context.SetValue(isfact.Name, null);
                return;
            }

            if (!this.facts.Contains(fact))
                throw new InvalidOperationException();

            this.facts.Remove(fact);
        }

        public bool IsAFact(Fact fact)
        {
            if (this.facts.Contains(fact))
                return true;

            NameVerbValueFact namefact = (NameVerbValueFact)fact;

            return namefact.IsSatisfied(this.context);
        }

        public void AddRule(Rule rule)
        {
            this.rules.Add(rule);
        }

        public void Run()
        {
            while (this.asserted.Count > 0)
                this.ProcessAssertedFact(this.asserted.Dequeue());
        }

        private void ProcessAssertedFact(Fact fact)
        {
            if (fact is NameVerbValueFact)
            {
                this.ProcessAssertedFact((NameVerbValueFact)fact);
                return;
            }
        }

        private void ProcessAssertedFact(NameVerbValueFact fact)
        {
            foreach (var rule in this.rules)
                if (rule.Conditions.Any(c => c is NameVerbValueFact && ((NameVerbValueFact)c).Name == fact.Name))
                    rule.FireIfReady(this);
        }
    }
}


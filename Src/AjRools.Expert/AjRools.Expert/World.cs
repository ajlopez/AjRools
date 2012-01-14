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
        private Queue<Fact> retracted = new Queue<Fact>();

        private IList<Rule> fired;
        private IList<Rule> notfired;
        private IList<Rule> nottested = new List<Rule>();

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
                this.retracted.Enqueue(fact);
                return;
            }

            if (!this.facts.Contains(fact))
                throw new InvalidOperationException();

            this.facts.Remove(fact);
            this.retracted.Enqueue(fact);
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
            if (this.notfired != null)
                this.nottested.Add(rule);
        }

        public void Run()
        {
            if (this.fired == null)
            {
                this.fired = new List<Rule>();
                this.notfired = new List<Rule>(this.rules);
            }

            while (this.retracted.Count > 0)
                this.ProcessRetractedFact(this.retracted.Dequeue());

            this.ProcessNotTestedRules();

            while (this.asserted.Count > 0)
                this.ProcessAssertedFact(this.asserted.Dequeue());
        }

        private void ProcessNotTestedRules()
        {
            foreach (var rule in this.nottested)
                if (rule.FireIfReady(this))
                    this.fired.Add(rule);
                else
                    this.notfired.Add(rule);

            this.nottested.Clear();
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
            IList<Rule> newfired = new List<Rule>();

            foreach (var rule in this.notfired)
                if (rule.Conditions.Any(c => c is NameVerbValueFact && ((NameVerbValueFact)c).Name == fact.Name))
                    if (rule.FireIfReady(this))
                        newfired.Add(rule);

            foreach (var rule in newfired)
            {
                this.notfired.Remove(rule);
                this.fired.Add(rule);
            }
        }

        private void ProcessRetractedFact(Fact fact)
        {
            if (fact is NameVerbValueFact)
            {
                this.ProcessRetractedFact((NameVerbValueFact)fact);
                return;
            }
        }

        private void ProcessRetractedFact(NameVerbValueFact fact)
        {
            IList<Rule> newretracted = new List<Rule>();

            foreach (var rule in this.fired)
                if (rule.Conditions.Any(c => c is NameVerbValueFact && ((NameVerbValueFact)c).Name == fact.Name))
                    if (rule.RetractIfNotReady(this))
                        newretracted.Add(rule);

            foreach (var rule in newretracted)
            {
                this.fired.Remove(rule);
                this.notfired.Add(rule);
            }
        }
    }
}


namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class EqualPredicate : IPredicate
    {
        private readonly string name;
        private readonly object value;
        
        public EqualPredicate(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (value == null)
                throw new ArgumentNullException("value");

            if (name.Length == 0)
                throw new ArgumentException("The name should not be empty string.", "name");

            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return this.name; }
        }

        public object Value
        {
            get { return this.value; }
        }

        public string SqlText
        {
            get { return this.name.Remove(0, 1) + " = " + this.name; }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { yield return new Parameter(this.name, this.value); }
        }
    }
}
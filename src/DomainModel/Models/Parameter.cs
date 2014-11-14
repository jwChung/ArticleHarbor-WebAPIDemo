namespace ArticleHarbor.DomainModel.Models
{
    using System;

    public class Parameter : IParameter
    {
        private readonly string name;
        private readonly object value;

        public Parameter(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (value == null)
                throw new ArgumentNullException("value");

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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Parameter)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.name.GetHashCode() * 397) ^ this.value.GetHashCode();
            }
        }

        protected bool Equals(Parameter other)
        {
            return string.Equals(this.name, other.name, StringComparison.CurrentCultureIgnoreCase)
                && this.value.Equals(other.value);
        }
    }
}
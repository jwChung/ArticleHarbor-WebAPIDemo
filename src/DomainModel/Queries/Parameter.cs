namespace ArticleHarbor.DomainModel.Queries
{
    using System;
    using System.Globalization;

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

            if (name.Length == 0)
                throw new ArgumentException("The name should not be empty string.", "name");

            if (name[0] != '@')
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The name '{0}' should start with '@'.",
                        name),
                    "name");

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
            if (other == null)
                throw new ArgumentNullException("other");

            return string.Equals(this.name, other.name, StringComparison.CurrentCultureIgnoreCase)
                && this.value.Equals(other.value);
        }
    }
}
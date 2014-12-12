namespace ArticleHarbor.DomainModel.Queries
{
    using System;

    public class OrderByColumn : IOrderByColumn
    {
        private readonly string name;
        private readonly OrderDirection direction;

        public OrderByColumn(string name, OrderDirection direction)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.direction = direction;
        }

        public string Name
        {
            get { return this.name; }
        }

        public OrderDirection OrderDirection
        {
            get { return this.direction; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((OrderByColumn)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.name.GetHashCode() * 397) ^ (int)this.direction;
            }
        }

        protected bool Equals(OrderByColumn other)
        {
            return string.Equals(this.name, other.name, StringComparison.CurrentCultureIgnoreCase)
                && this.direction == other.direction;
        }
    }
}
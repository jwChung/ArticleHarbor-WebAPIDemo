namespace ArticleHarbor.DomainModel.Queries
{
    public class Top : ITop
    {
        public static readonly ITop None = new Top(int.MinValue);
        private readonly int count;

        public Top(int count)
        {
            this.count = count;
        }
        
        public int Count
        {
            get { return this.count; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((Top)obj);
        }

        public override int GetHashCode()
        {
            return this.count;
        }

        protected bool Equals(Top other)
        {
            return this.count == other.count;
        }
    }
}
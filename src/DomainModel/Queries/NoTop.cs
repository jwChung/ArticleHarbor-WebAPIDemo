namespace ArticleHarbor.DomainModel.Queries
{
    public sealed class NoTop : ITop
    {
        public int Count
        {
            get { return int.MinValue; }
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj.GetType() == this.GetType();
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
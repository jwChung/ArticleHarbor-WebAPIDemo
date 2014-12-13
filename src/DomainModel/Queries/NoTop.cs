namespace ArticleHarbor.DomainModel.Queries
{
    public sealed class NoTop : ITop
    {
        public int Count
        {
            get { return 0; }
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
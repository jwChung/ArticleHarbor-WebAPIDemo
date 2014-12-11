namespace ArticleHarbor.DomainModel.Models
{
    public class SubjectFromBodyTransformer : ModelTransformer
    {
        private readonly int length;

        public SubjectFromBodyTransformer(int length)
        {
            this.length = length;
        }

        public int Length
        {
            get { return this.length; }
        }
    }
}
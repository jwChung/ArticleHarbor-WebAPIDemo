namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

    public class AndPredicate : IPredicate
    {
        public string SqlText
        {
            get { throw new System.NotImplementedException(); }
        }

        public IEnumerable<IParameter> Parameters
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
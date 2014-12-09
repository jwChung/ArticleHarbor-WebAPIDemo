namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

    public class DeleteKeywordsCommand : ModelCommand<IEnumerable<IModel>>
    {
        public override IEnumerable<IModel> Value
        {
            get { yield break; }
        }
    }
}
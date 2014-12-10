namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

    public class DeleteBookmarksCommand : ModelCommand<IEnumerable<IModel>>
    {
        public override IEnumerable<IModel> Value
        {
            get { yield break; }
        }
    }
}
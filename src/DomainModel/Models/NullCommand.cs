namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

    public class NullCommand : ModelCommand<IEnumerable<IModel>>
    {
        public NullCommand() : base(new IModel[0])
        {
        }
    }
}
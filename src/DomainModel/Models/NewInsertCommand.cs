namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;

    public class NewInsertCommand : ModelCommand<IEnumerable<IModel>>
    {
        public NewInsertCommand(IEnumerable<IModel> value) : base(value)
        {
        }
    }
}
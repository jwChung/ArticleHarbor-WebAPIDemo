namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class InsertCommand : ModelCommand<Task<IModel>>
    {
        public override IEnumerable<Task<IModel>> Result
        {
            get { yield break; }
        }
    }
}
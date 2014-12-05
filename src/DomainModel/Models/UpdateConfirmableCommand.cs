namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class UpdateConfirmableCommand : ModelCommand<IEnumerable<IModel>>
    {
        public override IEnumerable<IModel> Value
        {
            get { throw new NotImplementedException(); }
        }
    }
}
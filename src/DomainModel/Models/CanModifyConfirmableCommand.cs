namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class CanModifyConfirmableCommand : ModelCommand<object>
    {
        public override IEnumerable<object> Result
        {
            get { throw new NotImplementedException(); }
        }
    }
}
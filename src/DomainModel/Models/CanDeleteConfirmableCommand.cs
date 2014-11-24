namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CanDeleteConfirmableCommand : ModelCommand<Task>
    {
        public override IEnumerable<Task> Result
        {
            get { yield break; }
        }
    }
}
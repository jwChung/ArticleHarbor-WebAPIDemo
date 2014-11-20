namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CreatePermissionInspectingCommand : ModelCommand<IEnumerable<Task>>
    {
        public override IEnumerable<Task> Result
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
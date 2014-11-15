namespace ArticleHarbor.DomainModel.Models
{
    using System;

    public class CompositeModel : IModel
    {
        public IModelCommand<TResult> ExecuteCommand<TResult>(IModelCommand<TResult> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            throw new NotImplementedException();
        }
    }
}
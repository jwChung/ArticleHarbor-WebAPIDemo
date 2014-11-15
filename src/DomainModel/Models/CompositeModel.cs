namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class CompositeModel : IModel
    {
        private readonly IEnumerable<IModel> models;

        public CompositeModel(IEnumerable<IModel> models)
        {
            if (models == null)
                throw new ArgumentNullException("models");

            this.models = models;
        }

        public IEnumerable<IModel> Models
        {
            get { return this.models; }
        }

        public IModelCommand<TResult> ExecuteCommand<TResult>(IModelCommand<TResult> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            throw new NotImplementedException();
        }
    }
}
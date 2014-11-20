namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public IKeys GetKeys()
        {
            return new Keys(this.models.SelectMany(m => m.GetKeys()));
        }

        public IModelCommand<TResult> ExecuteCommand<TResult>(IModelCommand<TResult> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return this.Models.Aggregate(command, (c, m) => m.ExecuteCommand(c));
        }
    }
}
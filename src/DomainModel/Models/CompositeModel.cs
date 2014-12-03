namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public Task<IModelCommand<TValue>> ExecuteAsync<TValue>(IModelCommand<TValue> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return this.ExecuteAsyncWith(command);
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith<TValue>(
            IModelCommand<TValue> command)
        {
            foreach (var model in this.Models)
                command = await model.ExecuteAsync(command);

            return command;
        }
    }
}
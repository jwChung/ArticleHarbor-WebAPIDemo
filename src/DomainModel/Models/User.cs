namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Threading.Tasks;

    public class User : IModel
    {
        private readonly string id;
        private readonly Role role;
        private readonly Guid apiKey;

        public User(string id, Role role, Guid apiKey)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (apiKey == Guid.Empty)
                throw new ArgumentException("The api-key should not be empty guid.");

            this.id = id;
            this.role = role;
            this.apiKey = apiKey;
        }

        public string Id
        {
            get { return this.id; }
        }
        
        public Role Role
        {
            get { return this.role; }
        }

        public Guid ApiKey
        {
            get { return this.apiKey; }
        }

        public IKeys GetKeys()
        {
            return new Keys<string>(this.id);
        }

        public IModelCommand<TResult> Execute<TResult>(IModelCommand<TResult> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.Execute(this);
        }

        public Task<IModelCommand<TValue>> ExecuteAsync<TValue>(IModelCommand<TValue> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.ExecuteAsync(this);
        }
    }
}
namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Commands;
    using Queries;

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

        public Task<IEnumerable<TReturn>> ExecuteAsync<TReturn>(IModelCommand<TReturn> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.ExecuteAsync(this);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return this.Equals((User)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.id.ToUpper(CultureInfo.CurrentCulture).GetHashCode();
                hashCode = (hashCode * 397) ^ (int)this.role;
                hashCode = (hashCode * 397) ^ this.apiKey.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(User other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            return string.Equals(this.id, other.id, StringComparison.CurrentCultureIgnoreCase)
                && this.role == other.role
                && this.apiKey.Equals(other.apiKey);
        }
    }
}
namespace ArticleHarbor.DomainModel
{
    using System;

    public class User
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
    }
}
namespace ArticleHarbor.DomainModel
{
    using System;

    public class User
    {
        private readonly string id;
        private readonly Role role;

        public User(string id, Role role)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            this.id = id;
            this.role = role;
        }

        public string Id
        {
            get { return this.id; }
        }
        
        public Role Role
        {
            get { return this.role; }
        }
    }
}
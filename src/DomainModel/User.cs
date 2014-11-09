namespace ArticleHarbor.DomainModel
{
    using System;

    public class User
    {
        private readonly string id;
        private readonly Roles roles;

        public User(string id, Roles roles)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            this.id = id;
            this.roles = roles;
        }

        public string Id
        {
            get { return this.id; }
        }

        public Roles Roles
        {
            get { return this.roles; }
        }
    }
}
namespace DomainModel
{
    using System;

    public class User
    {
        private readonly string id;
        private readonly string password;

        public User(string id, string password)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (password == null)
                throw new ArgumentNullException("password");

            this.id = id;
            this.password = password;
        }

        public string Id
        {
            get { return this.id; }
        }

        public string Password
        {
            get { return this.password; }
        }
    }
}
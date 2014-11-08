namespace DomainModel
{
    using System;
    using System.Collections.Generic;

    public class UserRoles
    {
        private readonly string id;
        private readonly IEnumerable<string> roles;

        public UserRoles(string id, IEnumerable<string> roles)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (roles == null)
                throw new ArgumentNullException("roles");

            this.id = id;
            this.roles = roles;
        }

        public string Id
        {
            get { return this.id; }
        }

        public IEnumerable<string> Roles
        {
            get { return this.roles; }
        }
    }
}
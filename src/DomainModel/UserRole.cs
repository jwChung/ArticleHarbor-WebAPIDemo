namespace DomainModel
{
    using System;

    public class UserRole
    {
        private readonly string id;
        private readonly RoleTypes role;

        public UserRole(string id, RoleTypes role)
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

        public RoleTypes Role
        {
            get { return this.role; }
        }
    }
}
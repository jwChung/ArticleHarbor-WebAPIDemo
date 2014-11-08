namespace EFDataAccess
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserRoleManager : RoleManager<UserRole>
    {
        public UserRoleManager(RoleStore<UserRole> store) : base(store)
        {
        }
    }
}
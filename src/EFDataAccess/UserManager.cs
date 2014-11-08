namespace EFDataAccess
{
    using Microsoft.AspNet.Identity;

    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store) : base(store)
        {
        }
    }
}
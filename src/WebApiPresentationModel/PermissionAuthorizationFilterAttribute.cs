namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Web.Http.Filters;
    using DomainModel;

    public class PermissionAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        private readonly UserPermissions permissions;

        public PermissionAuthorizationFilterAttribute(UserPermissions permissions)
        {
            this.permissions = permissions;
        }

        public UserPermissions Permissions
        {
            get { return this.permissions; }
        }
    }
}
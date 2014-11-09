namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
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

        public override Task OnAuthorizationAsync(
            HttpActionContext context, CancellationToken cancellationToken)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            IPrincipal principal = context.RequestContext.Principal;

            if (principal == null || !this.HasPermissions(principal))
                context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized);

            return base.OnAuthorizationAsync(context, cancellationToken);
        }

        private static Role GetRole(IPrincipal principal)
        {
            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                if (principal.IsInRole(role))
                    return (Role)Enum.Parse(typeof(Role), role);
            }

            throw new ArgumentOutOfRangeException("principal", "The role name is not valid.");
        }

        private bool HasPermissions(IPrincipal principal)
        {
            var permissions = (Role)(int)this.Permissions;
            Role role = GetRole(principal);
            return role.HasFlag(permissions);
        }
    }
}
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

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class PermissionAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        private readonly Permissions permissions;

        public PermissionAuthorizationFilterAttribute(Permissions permissions)
        {
            this.permissions = permissions;
        }

        public Permissions Permissions
        {
            get { return this.permissions; }
        }

        public override Task OnAuthorizationAsync(
            HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");

            IPrincipal principal = actionContext.RequestContext.Principal;

            if (principal == null || !principal.Identity.IsAuthenticated || !this.HasPermissions(principal))
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);

            return base.OnAuthorizationAsync(actionContext, cancellationToken);
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
            var permissionValue = (Role)(int)this.Permissions;
            Role role = GetRole(principal);
            return role.HasFlag(permissionValue);
        }
    }
}
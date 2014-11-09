namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using DomainModel;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Xunit;

    public class PermissionAuthorizationFilterAttributeTest
        : IdiomaticTest<PermissionAuthorizationFilterAttribute>
    {
        [Test]
        public void SutIsAuthorizationAttribute(
            PermissionAuthorizationFilterAttribute sut)
        {
            Assert.IsAssignableFrom<AuthorizationFilterAttribute>(sut);
        }

        [Test]
        public async Task OnAuthorizationAsyncWithNullPrincipalRepliesWithUnauthorized(
            PermissionAuthorizationFilterAttribute sut,
            HttpActionContext context)
        {
            context.RequestContext.Principal = null;
            context.Response = null;

            await sut.OnAuthorizationAsync(context, CancellationToken.None);

            Assert.Equal(HttpStatusCode.Unauthorized, context.Response.StatusCode);
        }

        [Test]
        public IEnumerable<ITestCase> OnAuthorizationAsyncRepliesWithUnauthorizedWhenUserDoesNotHaveCorrectPermissions()
        {
            var testData = new[]
            {
                new
                {
                    Permissions = UserPermissions.DeleteAnyArticle,
                    RoleName = "User"
                },
                new
                {
                    Permissions = UserPermissions.WriteArticle,
                    RoleName = "User"
                },
                new
                {
                    Permissions = UserPermissions.DeleteAnyArticle,
                    RoleName = "Author"
                },
            };
            return TestCases.WithArgs(testData).WithAuto<HttpActionContext, IFixture>().Create(
                (d, context, fixture) =>
                {
                    fixture.Inject(d.Permissions);
                    context.RequestContext.Principal.Of(x => x.IsInRole(d.RoleName));
                    context.Response = null;
                    var sut = fixture.Create<PermissionAuthorizationFilterAttribute>();

                    sut.OnAuthorizationAsync(context, CancellationToken.None).Wait();

                    Assert.Equal(HttpStatusCode.Unauthorized, context.Response.StatusCode);
                });
        }

        [Test]
        public IEnumerable<ITestCase> OnAuthorizationAsyncRepliesWithSuccessWhenUserHasCorrectPermissions()
        {
            var testData = new[]
            {
                new
                {
                    Permissions = UserPermissions.DeleteAnyArticle,
                    RoleName = "Administrator"
                },
                new
                {
                    Permissions = UserPermissions.None,
                    RoleName = "User"
                },
                new
                {
                    Permissions = UserPermissions.None,
                    RoleName = "Author"
                },
                new
                {
                    Permissions = UserPermissions.None,
                    RoleName = "Administrator"
                },
                new
                {
                    Permissions = UserPermissions.DeleteOwnArticle | UserPermissions.WriteArticle,
                    RoleName = "Author"
                },
                new
                {
                    Permissions = UserPermissions.DeleteOwnArticle | UserPermissions.WriteArticle,
                    RoleName = "Administrator"
                },
                new
                {
                    Permissions = UserPermissions.Authentication,
                    RoleName = "User"
                }
            };
            return TestCases.WithArgs(testData).WithAuto<HttpActionContext, IFixture>().Create(
                (d, context, fixture) =>
                {
                    fixture.Inject(d.Permissions);
                    context.RequestContext.Principal.Of(x => x.IsInRole(d.RoleName));
                    context.Response = null;
                    var sut = fixture.Create<PermissionAuthorizationFilterAttribute>();

                    sut.OnAuthorizationAsync(context, CancellationToken.None).Wait();

                    Assert.Null(context.Response);
                });
        }

        [Test]
        public void OnAuthorizationAsyncThrowsWhenUserRoleIsInvalid(
            PermissionAuthorizationFilterAttribute sut,
            HttpActionContext context,
            string roleName)
        {
            context.RequestContext.Principal.Of(x => x.IsInRole(roleName));
            Assert.Throws<ArgumentOutOfRangeException>(
                () => sut.OnAuthorizationAsync(context, CancellationToken.None).Wait());
        }
    }
}
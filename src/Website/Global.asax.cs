namespace ArticleHarbor.Website
{
    using System.Web;
    using System.Web.Http;

    public class WebApiApplication : HttpApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "시그니처 변경할 수 없음.")]
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
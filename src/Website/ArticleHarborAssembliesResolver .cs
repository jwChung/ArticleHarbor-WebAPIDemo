namespace ArticleHarbor.Website
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http.Dispatcher;
    using ArticleHarbor.WebApiPresentationModel;
    using WebApiPresentationModel.Controllers;

    public class ArticleHarborAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            var assemblies = base.GetAssemblies();

            var customControllersAssembly = typeof(ArticlesController).Assembly;
            if (!assemblies.Contains(customControllersAssembly))
                assemblies.Add(customControllersAssembly);

            return assemblies;
        }
    }
}
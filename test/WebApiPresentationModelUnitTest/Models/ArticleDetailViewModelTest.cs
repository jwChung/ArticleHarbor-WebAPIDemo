namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System.Reflection;
    using Newtonsoft.Json;
    using Xunit;

    public class ArticleDetailViewModelTest : IdiomaticTest<ArticleDetailViewModel>
    {
        [Test]
        public void ArticleHasJsonIgnoreAttribute()
        {
            var attribute = this.Properties.Select(x => x.Article)
                .GetCustomAttribute<JsonIgnoreAttribute>();
            Assert.NotNull(attribute);
        }
    }
}
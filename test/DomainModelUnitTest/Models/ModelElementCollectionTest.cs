namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ModelElementCollectionTest : IdiomaticTest<ModelElementCollection<object>>
    {
        [Test]
        public void SutIsModelElementCollection(ModelElementCollection<object> sut)
        {
            Assert.IsAssignableFrom<IModelElementCollection<object>>(sut);
        } 
    }
}
namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ModelElementCollectionTest : IdiomaticTest<ModelElementCollection<IIndentity, object>>
    {
        [Test]
        public void SutIsModelElementCollection(ModelElementCollection<IIndentity, object> sut)
        {
            Assert.IsAssignableFrom<IModelElementCollection<IIndentity, object>>(sut);
        }
    }
}
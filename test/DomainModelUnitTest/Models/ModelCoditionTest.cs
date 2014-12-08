namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class ModelCoditionTest
    {
        [Test]
        public void SutIsModelCondition(ModelCodition sut)
        {
            Assert.IsAssignableFrom<IModelCondition>(sut);
        }
    }
}
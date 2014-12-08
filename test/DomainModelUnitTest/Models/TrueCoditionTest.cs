namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class TrueCoditionTest
    {
        [Test]
        public void SutIsModelCondition(TrueCodition sut)
        {
            Assert.IsAssignableFrom<IModelCondition>(sut);
        }
    }
}
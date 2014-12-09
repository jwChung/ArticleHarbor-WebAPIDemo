namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class UpdateKeywordsConditionTest : IdiomaticTest<UpdateKeywordsCondition>
    {
        [Test]
        public void SutIsTrueCondition(UpdateKeywordsCondition sut)
        {
            Assert.IsAssignableFrom<TrueCondition>(sut);
        }
    }
}
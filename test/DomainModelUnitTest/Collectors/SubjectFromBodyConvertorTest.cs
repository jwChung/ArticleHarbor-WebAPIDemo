namespace ArticleHarbor.DomainModel.Collectors
{
    using Xunit;

    public class SubjectFromBodyConvertorTest : IdiomaticTest<SubjectFromBodyConvertor>
    {
        [Test]
        public void SutIsArticleConverter(SubjectFromBodyConvertor sut)
        {
            Assert.IsAssignableFrom<IArticleConvertor>(sut);
        }
    }
}
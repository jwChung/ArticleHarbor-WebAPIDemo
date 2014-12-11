namespace ArticleHarbor.DomainModel.Models
{
    using Xunit;

    public class SubjectFromBodyTransformerTest : IdiomaticTest<SubjectFromBodyTransformer>
    {
        [Test]
        public void SutIsModelTransformer(SubjectFromBodyTransformer sut)
        {
            Assert.IsAssignableFrom<IModelTransformer>(sut);
        }
    }
}
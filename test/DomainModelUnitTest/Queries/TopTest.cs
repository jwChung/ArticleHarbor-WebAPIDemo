namespace ArticleHarbor.DomainModel.Queries
{
    using Xunit;

    public class TopTest : IdiomaticTest<Top>
    {
        [Test]
        public void SutIsTop(Top sut)
        {
            Assert.IsAssignableFrom<ITop>(sut);
        }

        [Test]
        public void NoneIsNoTop()
        {
            var actual = Top.None;
            Assert.IsType<NoTop>(actual);
        }

        [Test]
        public void EqualsEqualsWithSameCount(Top sut)
        {
            var other = new Top(sut.Count);
            Assert.Equal(other, sut);
        }

        [Test]
        public void EqualsDoesNotEqualWithOtherCount(Top sut, Top other)
        {
            Assert.NotEqual(other, sut);
        }
    }
}
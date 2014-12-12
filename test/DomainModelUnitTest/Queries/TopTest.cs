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
        public void NoneIsCorrect()
        {
            var actual = Top.None;
            var top = Assert.IsAssignableFrom<ITop>(actual);
            Assert.Equal(int.MinValue, top.Count);
        }

        [Test]
        public void NoneAlwaysReturnsSameInstance()
        {
            var actual = Top.None;
            Assert.Same(Top.None, actual);
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
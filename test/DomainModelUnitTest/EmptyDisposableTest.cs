namespace ArticleHarbor.DomainModel
{
    using System;
    using Xunit;

    public class EmptyDisposableTest : IdiomaticTest<EmptyDisposable>
    {
        [Test]
        public void SutIsDisposable(EmptyDisposable sut)
        {
            Assert.IsAssignableFrom<IDisposable>(sut);
        } 
    }
}
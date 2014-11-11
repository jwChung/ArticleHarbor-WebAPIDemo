namespace ArticleHarbor.DomainModel
{
    using System;

    public sealed class EmptyDisposable : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
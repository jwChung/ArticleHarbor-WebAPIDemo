namespace DomainModel
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository Articles { get; }

        IArticleWordRepository ArticleWords { get; }
    }
}
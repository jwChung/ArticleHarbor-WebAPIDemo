namespace DomainModel
{
    using System;

    public interface IDatabaseContext : IDisposable
    {
        IArticleRepository Articles { get; }

        IArticleWordRepository ArticleWords { get; }
    }
}
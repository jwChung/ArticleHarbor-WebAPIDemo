namespace DomainModel
{
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository Articles { get; }

        IArticleWordRepository ArticleWords { get; }

        Task SaveAsync();
    }
}
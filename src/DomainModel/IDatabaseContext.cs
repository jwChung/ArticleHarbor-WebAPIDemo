namespace DomainModel
{
    using System;

    public interface IDatabaseContext : IDisposable
    {
        IArticleRepository Articles { get; }

        void Save();
    }
}
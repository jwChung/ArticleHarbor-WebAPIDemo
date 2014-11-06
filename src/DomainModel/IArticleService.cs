﻿namespace DomainModel
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAsync();

        Task<Article> AddOrModifyAsync(Article article);

        void Remove(int id);
    }
}
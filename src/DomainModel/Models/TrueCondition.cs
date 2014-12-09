﻿namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Threading.Tasks;

    public class TrueCondition : IModelCondition
    {
        public virtual Task<bool> CanExecuteAsync(User user)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanExecuteAsync(Article article)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanExecuteAsync(Bookmark bookmark)
        {
            return Task.FromResult(true);
        }

        public virtual Task<bool> CanExecuteAsync(Keyword keyword)
        {
            return Task.FromResult(true);
        }
    }
}
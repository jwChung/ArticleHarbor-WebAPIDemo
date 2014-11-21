﻿namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using EFDataAccess;
    using Article = DomainModel.Models.Article;
    using Bookmark = DomainModel.Models.Bookmark;
    using Keyword = DomainModel.Models.Keyword;
    using User = DomainModel.Models.User;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleHarborDbContext context;

        public UnitOfWork(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public IRepository<Keys<int>, Article> Articles
        {
            get { throw new NotImplementedException(); }
        }

        public IRepository<Keys<int, string>, Keyword> Keywords
        {
            get { throw new NotImplementedException(); }
        }

        public IRepository<Keys<string, int>, Bookmark> Bookmarks
        {
            get { throw new NotImplementedException(); }
        }

        public IRepository<Keys<string>, User> Users
        {
            get { throw new NotImplementedException(); }
        }

        public Task SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}
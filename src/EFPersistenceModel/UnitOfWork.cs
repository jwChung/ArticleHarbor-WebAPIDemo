﻿namespace EFPersistenceModel
{
    using System;
    using System.Threading.Tasks;
    using DomainModel;
    using EFDataAccess;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleRepository articles;
        private readonly ArticleWordRepository articleWords;
        private readonly ArticleHarborDbContext context;
        private readonly UserRepository userRepository;

        public UnitOfWork(ArticleHarborDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            this.context = context;
            this.articles = new ArticleRepository(this.context);
            this.articleWords = new ArticleWordRepository(this.context);
            this.userRepository = new UserRepository(this.context);
        }

        public ArticleHarborDbContext Context
        {
            get { return this.context; }
        }

        public IArticleRepository Articles
        {
            get
            {
                return this.articles;
            }
        }

        public IArticleWordRepository ArticleWords
        {
            get
            {
                return this.articleWords;
            }
        }

        public IUserRepository Users
        {
            get { return this.userRepository; }
        }

        public Task SaveAsync()
        {
            return this.Context.SaveChangesAsync();
        }
    }
}
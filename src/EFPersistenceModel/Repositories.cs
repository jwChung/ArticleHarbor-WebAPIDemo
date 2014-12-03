namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using DomainModel.Models;
    using DomainModel.Repositories;

    public class Repositories : IRepositories
    {
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
    }
}
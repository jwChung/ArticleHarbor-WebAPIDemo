namespace EFPersistenceModel
{
    using DomainModel;

    public class DatabaseContext : IDatabaseContext
    {
        public IArticleRepository Articles
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
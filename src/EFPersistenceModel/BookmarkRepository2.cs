namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Threading.Tasks;
    using DomainModel.Models;
    using EFDataAccess;
    using Bookmark = DomainModel.Models.Bookmark;

    public class BookmarkRepository2 : Repository<Keys<string, int>, Bookmark, EFDataAccess.Bookmark>
    {
        private readonly ArticleHarborDbContext context;

        public BookmarkRepository2(
            ArticleHarborDbContext context,
            DbSet<EFDataAccess.Bookmark> dbSet)
            : base(context, dbSet)
        {
            this.context = context;
        }

        public override Task<Bookmark> ConvertToModelAsync(EFDataAccess.Bookmark persistence)
        {
            if (persistence == null)
                throw new ArgumentNullException("persistence");

            return this.ConvertToModelAsyncWith(persistence);
        }

        public override Task<EFDataAccess.Bookmark> ConvertToPersistenceAsync(Bookmark model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            return this.ConvertToPersistenceAsyncWith(model);
        }

        private async Task<Bookmark> ConvertToModelAsyncWith(EFDataAccess.Bookmark persistence)
        {
            var user = await this.context.UserManager.FindByIdAsync(persistence.UserId);
            if (user == null)
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "There is no user matched with the id '{0}'.",
                        persistence.UserId),
                    "persistence");

            return new Bookmark(user.UserName, persistence.ArticleId);
        }

        private async Task<EFDataAccess.Bookmark> ConvertToPersistenceAsyncWith(Bookmark model)
        {
            var user = await this.context.UserManager.FindByNameAsync(model.UserId);
            if (user == null)
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "There is no user matched with the name '{0}'.",
                        model.UserId),
                    "persistence");

            return new EFDataAccess.Bookmark
            {
                ArticleId = model.ArticleId,
                UserId = user.Id
            };
        }
    }
}
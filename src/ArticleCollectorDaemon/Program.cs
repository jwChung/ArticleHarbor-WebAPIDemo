namespace ArticleHarbor.ArticleCollectorDaemon
{
    using System;
    using System.Data.Entity;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.DomainModel.Collectors;
    using ArticleHarbor.DomainModel.Services;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;

    internal class Programss
    {
        private static void Main(string[] args)
        {
            var context = new ArticleHarborDbContext(new CreateDatabaseIfNotExists<ArticleHarborDbContext>());
            var executor = CreateExecutor(context);
        }

        private static ArticleCollectionExecutor CreateExecutor(ArticleHarborDbContext context)
        {
            return new ArticleCollectionExecutor(
                collector: new CompositeArticleCollector(
                    new IArticleCollector[]
                    {
                        new HaniRssCollector("user1"),
                        new FacebookRssCollector("user2", "177323639028540"), // ASP.NET Korea group
                        new FacebookRssCollector("user2", "200708093411111")  // C# study group
                    }),
                service: new ArticleService(
                    new ArticleRepository(context),
                    new ArticleWordService(
                        new ArticleWordRepository(context),
                        new ArticleRepository(context),
                        KoreanNounExtractor.Execute)),
                delay: 300,
                callback: a => Console.WriteLine("Added: " + a.Subject));
        }
    }
}
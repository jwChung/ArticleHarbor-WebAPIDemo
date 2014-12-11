namespace ArticleHarbor.ArticleCollectorDaemon
{
    using System;
    using System.Data.Entity;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.DomainModel.Collectors;
    using ArticleHarbor.DomainModel.Services;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;
    using Article = DomainModel.Models.Article;

    internal class Program
    {
        private static void Main()
        {
            ////using (var context = new ArticleHarborDbContext(
            ////    new ArticleHarborDbContextTestInitializer()))
            ////{
            ////    var executor = CreateExecutor(context);
            ////    executor.ExecuteAsync().Wait();
            ////    context.SaveChanges();
            ////}
        }

        ////[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "It is appropriate to represent as literal because of simplity.")]
        ////private static ArticleCollectingExecutor CreateExecutor(ArticleHarborDbContext context)
        ////{
        ////    return new ArticleCollectingExecutor(
        ////        collector: new CompositeCollector(
        ////            new HaniRssCollector("user1"),
        ////            new ArticleTransformationCollector(
        ////                new ArticleTransformationCollector(
        ////                    new CompositeCollector(
        ////                        new FacebookRssCollector("user2", "177323639028540"), // ASP.NET Korea group
        ////                        new FacebookRssCollector("user2", "200708093411111")), // C# study group
        ////                    new DelegateTransformation(RemoveUnnecessaryContent)),
        ////                new SubjectFromBodyTransformation(50))),
        ////        service: new ArticleService(
        ////            new ArticleRepository(context),
        ////            new KeywordService(
        ////                new KeywordRepository(context),
        ////                new ArticleRepository(context),
        ////                KoreanNounExtractor.Execute)),
        ////        delay: 200,
        ////        callback: a => Console.WriteLine("Added: " + a.Subject));
        ////}

        private static Article RemoveUnnecessaryContent(Article article)
        {
            var index = article.Body.IndexOf(':');
            var newBody = article.Body.Remove(0, index + 1);
            return article.WithBody(newBody);
        }
    }
}
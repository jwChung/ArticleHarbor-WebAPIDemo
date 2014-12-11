namespace ArticleHarbor.ArticleCollectorDaemon
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.DomainModel.Collectors;
    using ArticleHarbor.DomainModel.Services;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;
    using DomainModel.Models;
    using Article = DomainModel.Models.Article;

    internal class Program
    {
        private static void Main()
        {
            CollectFacebookArticles().Wait();
        }

        private static async Task CollectFacebookArticles()
        {
            var context = CreateDbContext();
            var repositories = new Repositories(context);

            var articles = await new CompositeCollector(
                new FacebookRssCollector("user2", "177323639028540"),                 // ASP.NET Korea group
                new FacebookRssCollector("user2", "200708093411111")).CollectAsync(); // C# study group

            var command = new TransformableCommand<IModel>(
                    new RemoveUnnecessaryContentTransformer(),
                    new TransformableCommand<IModel>(
                        new SubjectFromBodyTransformer(50),
                        new CompositeCommand<IModel>(
                            new BeforeInsertMessageCommand(),
                            new InsertCommand(
                                repositories,
                                new RelayKeywordsCommand(
                                    KoreanNounExtractor.Execute,
                                    new InsertCommand(
                                        repositories,
                                        new ModelCommand<IModel>()))),
                            new AfterInsertMessageCommand())));
            
            await new CompositeModel(articles).ExecuteAsync(command);

            context.SaveChanges();
        }

        private static ArticleHarborDbContext CreateDbContext()
        {
            return new ArticleHarborDbContext(
                new ArticleHarborDbContextTestInitializer());
        }

        private class RemoveUnnecessaryContentTransformer : ModelTransformer
        {
            public override Task<Article> TransformAsync(Article article)
            {
                var index = article.Body.IndexOf(':');
                var newBody = article.Body.Remove(0, index + 1);
                return Task.FromResult(article.WithBody(newBody));
            }
        }
        
        private class BeforeInsertMessageCommand : ModelCommand<IModel>
        {
            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                Console.WriteLine("Adding: " + article.Subject + "...");
                return base.ExecuteAsync(article);
            }
        }

        private class AfterInsertMessageCommand : ModelCommand<IModel>
        {
            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                Console.WriteLine("Added: " + article.Subject);
                return base.ExecuteAsync(article);
            }
        }
    }
}
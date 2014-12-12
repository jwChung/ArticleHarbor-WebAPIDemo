namespace ArticleHarbor.ArticleCollectorDaemon
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.DomainModel.Collectors;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;
    using DomainModel.Commands;
    using DomainModel.Models;
    using Article = DomainModel.Models.Article;

    internal class Program
    {
        private static void Main()
        {
            Task.WaitAll(
                    CollectHaniArticles("user1"),
                CollectFacebookArticles("user2", "177323639028540", "ASP.NET Korea Group"),
                CollectFacebookArticles("user2", "200708093411111", "C# Study Group"));
        }

        private static async Task CollectHaniArticles(string author)
        {
            using (var context = CreateDbContext())
            {
                var repositories = new Repositories(context);
                var articles = await new HaniRssCollector(author).CollectAsync();
                await new CompositeModel(articles).ExecuteAsync(CreateCommand(repositories));
                context.SaveChanges();
            }
        }

        private static async Task CollectFacebookArticles(
            string author, string facebookId, string facebookName)
        {
            using (var context = CreateDbContext())
            {
                var repositories = new Repositories(context);
                var articles = await new FacebookRssCollector(author, facebookId, facebookName)
                    .CollectAsync();
                await new CompositeModel(articles).ExecuteAsync(CreateFacebookCommand(repositories));
                context.SaveChanges();
            }
        }

        private static IModelCommand<IModel> CreateFacebookCommand(Repositories repositories)
        {
            return new TransformableCommand<IModel>(
                new RemoveUnnecessaryContentTransformer(),
                new TransformableCommand<IModel>(
                    new SubjectFromBodyTransformer(50),
                    CreateCommand(repositories)));
        }

        private static IModelCommand<IModel> CreateCommand(Repositories repositories)
        {
            return new CompositeCommand<IModel>(
                new BeforeLogCommand(),
                new InsertCommand(
                    repositories,
                    new RelayKeywordsCommand(
                        KoreanNounExtractor.Execute,
                        new InsertCommand(
                            repositories,
                            new ModelCommand<IModel>()))),
                new AfterLogCommand());
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
                if (article == null)
                    throw new ArgumentNullException("article");

                var index = article.Body.IndexOf(':');
                var newBody = article.Body.Remove(0, index + 2);
                return Task.FromResult(article.WithBody(newBody));
            }
        }
        
        private class BeforeLogCommand : ModelCommand<IModel>
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This can be suppressed as the message is simple.")]
            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                if (article == null)
                    throw new ArgumentNullException("article");

                Console.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} (adding): {1}...",
                    article.Provider,
                    article.Subject));

                return base.ExecuteAsync(article);
            }
        }

        private class AfterLogCommand : ModelCommand<IModel>
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This can be suppressed as the message is simple.")]
            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                if (article == null)
                    throw new ArgumentNullException("article");

                Console.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}  (added): {1}",
                    article.Provider,
                    article.Subject));

                return base.ExecuteAsync(article);
            }
        }
    }
}
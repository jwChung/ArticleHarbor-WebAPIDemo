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
                    CollectHaniArticles("             한겨레", "user1"),
                CollectFacebookArticles("ASP.NET Korea Group", "user2", "177323639028540"),
                CollectFacebookArticles("     C# Study Group", "user2", "200708093411111"));
        }

        private static async Task CollectHaniArticles(string header, string author)
        {
            using (var context = CreateDbContext())
            {
                var repositories = new Repositories(context);
                var articles = await new HaniRssCollector(author).CollectAsync();
                await new CompositeModel(articles).ExecuteAsync(CreateCommand(header, repositories));
                context.SaveChanges();
            }
        }

        private static async Task CollectFacebookArticles(string header, string author, string facebookId)
        {
            using (var context = CreateDbContext())
            {
                var repositories = new Repositories(context);
                var articles = await new FacebookRssCollector(author, facebookId).CollectAsync();
                await new CompositeModel(articles).ExecuteAsync(CreateFacebookCommand(header, repositories));
                context.SaveChanges();
            }
        }

        private static IModelCommand<IModel> CreateFacebookCommand(string header, Repositories repositories)
        {
            return new TransformableCommand<IModel>(
                new RemoveUnnecessaryContentTransformer(),
                new TransformableCommand<IModel>(
                    new SubjectFromBodyTransformer(50),
                    CreateCommand(header, repositories)));
        }

        private static IModelCommand<IModel> CreateCommand(string header, Repositories repositories)
        {
            return new CompositeCommand<IModel>(
                new BeforeLogCommand(header),
                new InsertCommand(
                    repositories,
                    new RelayKeywordsCommand(
                        KoreanNounExtractor.Execute,
                        new InsertCommand(
                            repositories,
                            new ModelCommand<IModel>()))),
                new AfterLogCommand(header));
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
            private string header;

            public BeforeLogCommand(string header)
            {
                this.header = header;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This can be suppressed as the message is simple.")]
            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                if (article == null)
                    throw new ArgumentNullException("article");

                Console.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} [Adding]: {1}",
                    this.header,
                    article.Subject));

                return base.ExecuteAsync(article);
            }
        }

        private class AfterLogCommand : ModelCommand<IModel>
        {
            private string header;

            public AfterLogCommand(string header)
            {
                this.header = header;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This can be suppressed as the message is simple.")]
            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                if (article == null)
                    throw new ArgumentNullException("article");

                Console.WriteLine(string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} [ Added]: {1}",
                    this.header,
                    article.Subject));

                return base.ExecuteAsync(article);
            }
        }
    }
}
namespace ArticleHarbor.ArticleCollectorDaemon
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using ArticleHarbor.DomainModel;
    using ArticleHarbor.DomainModel.Collectors;
    using ArticleHarbor.EFDataAccess;
    using ArticleHarbor.EFPersistenceModel;
    using DomainModel.Models;
    using Article = DomainModel.Models.Article;

    internal class Program
    {
        private static void Main()
        {
            Task.WaitAll(
                CollectHaniArticles("user1"),
                CollectFacebookArticles("user2", /* ASP.NET Korea group */ "177323639028540"),
                CollectFacebookArticles("user2", /* C# study group */ "200708093411111"));
        }

        private static async Task CollectHaniArticles(string author)
        {
            using (var context = CreateDbContext())
            {
                var repositories = new Repositories(context);
                var articles = await new HaniRssCollector(author).CollectAsync();
                await new CompositeModel(articles).ExecuteAsync(CreateCommand("Hani", repositories));
                context.SaveChanges();
            }
        }

        private static async Task CollectFacebookArticles(string author, string facebookId)
        {
            using (var context = CreateDbContext())
            {
                var repositories = new Repositories(context);
                var articles = await new FacebookRssCollector(author, facebookId).CollectAsync();
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
                    CreateCommand("Facebook", repositories)));
        }

        private static IModelCommand<IModel> CreateCommand(string name, Repositories repositories)
        {
            return new CompositeCommand<IModel>(
                new BeforeLogCommand(name),
                new InsertCommand(
                    repositories,
                    new RelayKeywordsCommand(
                        KoreanNounExtractor.Execute,
                        new InsertCommand(
                            repositories,
                            new ModelCommand<IModel>()))),
                new AfterLogCommand(name));
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
                var newBody = article.Body.Remove(0, index + 2);
                return Task.FromResult(article.WithBody(newBody));
            }
        }
        
        private class BeforeLogCommand : ModelCommand<IModel>
        {
            private string name;

            public BeforeLogCommand(string name)
            {
                this.name = name;
            }

            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                Console.WriteLine(string.Format("{0} (Adding): {1}", this.name.PadLeft(10), article.Subject));
                return base.ExecuteAsync(article);
            }
        }

        private class AfterLogCommand : ModelCommand<IModel>
        {
            private string name;

            public AfterLogCommand(string name)
            {
                this.name = name;
            }

            public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
            {
                Console.WriteLine(string.Format("{0}  (Added): {1}", this.name.PadLeft(10), article.Subject));
                return base.ExecuteAsync(article);
            }
        }
    }
}
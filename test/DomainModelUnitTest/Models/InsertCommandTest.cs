namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ploeh.AutoFixture;
    using Ploeh.SemanticComparison.Fluent;
    using Xunit;

    public class InsertCommandTest : IdiomaticTest<InsertCommand>
    {
        [Test]
        public void SutIsModelCommand(InsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyInsertsUser(
            InsertCommand sut,
            User user,
            User newUser)
        {
            sut.Repositories.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));

            var actual = sut.ExecuteAsync(user).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newUser, command.Value.Last());
        }
        
        [Test]
        public void ExecuteAsyncArticleRelaysInsertingKeywords(
            Article article,
            Article newArticle,
            string[] words,
            Keyword[] newKeywords,
            IFixture fixture)
        {
            // Fixture setup
            fixture.Inject<Func<string, IEnumerable<string>>>(x =>
            {
                Assert.Equal(newArticle.Subject, x);
                return words;
            });

            var sut = fixture.Create<InsertCommand>();

            sut.Repositories.Articles.Of(x => x.InsertAsync(article)
                == Task.FromResult(newArticle));

            var keywords = words.Select(w => new Keyword(newArticle.Id, w)
                .AsSource().OfLikeness<Keyword>().CreateProxy());

            keywords.Select((k, i) => sut.Repositories.Keywords.Of(
                x => x.InsertAsync(k) == Task.FromResult(newKeywords[i]))).ToArray();

            // Exercise system
            var actual = sut.ExecuteAsync(article).Result;

            // Verify outcome
            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            
            Assert.Equal((fixture.RepeatCount * 2) + 1, command.Value.Count());

            Assert.Contains(newArticle, command.Value);

            var keywordLikenesses = newKeywords.Select(k => k.AsSource().OfLikeness<Keyword>());
            foreach (var keywordLikeness in keywordLikenesses)
                command.Value.OfType<Keyword>().Single(keywordLikeness.Equals);
        }

        [Test]
        public void ExecuteAsyncKeywordCorrectlyInsertsKeyword(
            InsertCommand sut,
            Keyword keyword,
            Keyword newKeyword)
        {
            sut.Repositories.Keywords.Of(
                x => x.InsertAsync(keyword) == Task.FromResult(newKeyword));

            var actual = sut.ExecuteAsync(keyword).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newKeyword, command.Value.Last());
        }

        [Test]
        public void ExecuteAsyncBookmarkCorrectlyInsertsBookmark(
            InsertCommand sut,
            Bookmark bookmark,
            Bookmark newBookmark)
        {
            sut.Repositories.Bookmarks.Of(
                x => x.InsertAsync(bookmark) == Task.FromResult(newBookmark));

            var actual = sut.ExecuteAsync(bookmark).Result;

            var command = Assert.IsAssignableFrom<InsertCommand>(actual);
            Assert.Equal(newBookmark, command.Value.Last());
        }
    }
}
namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using Xunit;

    public class NewInsertCommandTest : IdiomaticTest<NewInsertCommand>
    {
        [Test]
        public void SutIsModelCommand(NewInsertCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyInsertsUser(
            NewInsertCommand sut,
            User user,
            User newUser,
            IEnumerable<IModel> newInnerCommandValue)
        {
            sut.Repositories.Users.Of(x => x.InsertAsync(user) == Task.FromResult(newUser));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newUser) == Task.FromResult(
                Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == newInnerCommandValue)));
            var expected = sut.Value.Concat(new IModel[] { newUser }).Concat(newInnerCommandValue);

            var actual = sut.ExecuteAsync(user).Result;

            var newCommand = Assert.IsAssignableFrom<NewInsertCommand>(actual);
            Assert.Equal(sut.InnerCommand, newCommand.InnerCommand);
            this.AssertEquivalent(expected, newCommand.Value);
        }

        [Test]
        public void ExecuteAsyncArticleCorrectlyInsertsArticle(
            NewInsertCommand sut,
            Article article,
            Article newArticle,
            IEnumerable<IModel> newInnerCommandValue)
        {
            sut.Repositories.Articles.Of(x => x.InsertAsync(article) == Task.FromResult(newArticle));
            sut.InnerCommand.Of(x => x.ExecuteAsync(newArticle) == Task.FromResult(
                Mock.Of<IModelCommand<IEnumerable<IModel>>>(c => c.Value == newInnerCommandValue)));
            var expected = sut.Value.Concat(new IModel[] { newArticle }).Concat(newInnerCommandValue);

            var actual = sut.ExecuteAsync(article).Result;

            var newCommand = Assert.IsAssignableFrom<NewInsertCommand>(actual);
            Assert.Equal(sut.InnerCommand, newCommand.InnerCommand);
            this.AssertEquivalent(expected, newCommand.Value);
        }

        private void AssertEquivalent<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.Equal(expected.Count(), actual.Count());
            Assert.Empty(expected.Except(actual));
        }
    }
}
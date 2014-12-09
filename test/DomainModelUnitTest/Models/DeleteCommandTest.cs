namespace ArticleHarbor.DomainModel.Models
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class DeleteCommandTest : IdiomaticTest<DeleteCommand>
    {
        [Test]
        public void SutIsModelCommand(DeleteCommand sut)
        {
            Assert.IsAssignableFrom<ModelCommand<IEnumerable<IModel>>>(sut);
        }

        [Test]
        public void ValueIsEmpty(DeleteCommand sut)
        {
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Test]
        public void ExecuteAsyncUserCorrectlyDeletes(
            DeleteCommand sut,
            User user)
        {
            bool verifies = false;
            var task = Task.Run(() =>
            {
                Thread.Sleep(300);
                verifies = true;
            });
            sut.Repositories.Users.Of(x => x.DeleteAsync(new Keys<string>(user.Id)) == task);

            var actual = sut.ExecuteAsync(user).Result;

            Assert.Equal(actual, sut);
            Assert.True(verifies);
        }

        protected override IEnumerable<MemberInfo> ExceptToVerifyInitialization()
        {
            yield return this.Properties.Select(x => x.Value);
        }
    }
}
namespace ArticleHarbor.EFPersistenceModel
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using Xunit;

    public class UserManagerTest : IdiomaticTest<UserManager>
    {
        [Test]
        public void SutIsUserManager(UserManager sut)
        {
            Assert.IsAssignableFrom<IUserManager>(sut);
        }

        [Test]
        public void FindAsyncWithIdAndPasswordReturnsCorrectUser(UserManager sut)
        {
            var actual = sut.FindAsync("user1", "password1").Result;

            Assert.Equal("user1", actual.Id);
            Assert.Equal(Guid.Parse("692c7798206844b88ba9a660e3374eef"), actual.ApiKey);
            Assert.Equal(Role.Administrator, actual.Role);
        }

        [Test]
        public void FindAsyncWithIncorectIdReturnsNullUser(UserManager sut)
        {
            var actual = sut.FindAsync("user", "password1").Result;
            Assert.Null(actual);
        }

        [Test]
        public void FindAsyncWithIncorectPasswordReturnsNullUser(UserManager sut)
        {
            var actual = sut.FindAsync("user1", "password").Result;
            Assert.Null(actual);
        }

        [Test]
        public void DisposeDiposesContext(UserManager sut)
        {
            sut.Dispose();

            var e = Assert.Throws<AggregateException>(
                () => sut.Context.UserManager.FindAsync("test", "test").Wait());
            Assert.Contains("disposed", e.InnerException.Message);
        }
        
        [Test]
        public void DisposeDoesNotThrowWhenCalledManyTime(UserManager sut)
        {
            sut.Dispose();
            sut.Dispose();
            sut.Dispose();
        }
    }
}
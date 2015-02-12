namespace ArticleHarbor.Website
{
    using System.Linq;
    using ArticleHarbor.DomainModel.Commands;
    using ArticleHarbor.WebApiPresentationModel.Controllers;
    using DomainModel.Models;
    using DomainModel.Repositories;
    using Jwc.Funz;
    using Moq;
    using Xunit;

    public class DependencyRegistrationsTest
    {
        [Test]
        public void DeleteCommandIsCorrectlyStructured(
            DependencyRegistrations sut,
            Container container,
            IRepositories repositories)
        {
            container.Accept(sut);
            container.Register(c => repositories);
            var expected = new[]
            {
                typeof(DeleteConfirmableCommand),
                typeof(DeleteKeywordsCommand),
                typeof(DeleteBookmarksCommand),
                typeof(DeleteCommand),
            };

            var controller = container.Resolve<ArticlesController>();

            var compositeCommands = Assert.IsAssignableFrom<CompositeCommand<IModel>>(
                controller.DeleteCommand);
            Assert.Equal(expected, compositeCommands.Commands.Select(c => c.GetType()));
        }
    }
}
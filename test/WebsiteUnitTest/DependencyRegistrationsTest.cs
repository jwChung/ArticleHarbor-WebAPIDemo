namespace ArticleHarbor.Website
{
    using System.Linq;
    using ArticleHarbor.DomainModel.Commands;
    using ArticleHarbor.WebApiPresentationModel.Controllers;
    using DomainModel.Models;
    using Jwc.Funz;
    using Xunit;

    public class DependencyRegistrationsTest
    {
        [Test]
        public void DeleteCommandIsCorrectlyStructured(
            DependencyRegistrations sut,
            Container container)
        {
            container.Accept(sut);
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
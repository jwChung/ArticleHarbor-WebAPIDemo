namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public class TransformableCommand<TReturn> : ModelCommand<TReturn>
    {
        private readonly IModelTransformation transformation;
        private readonly IModelCommand<TReturn> innerCommand;

        public TransformableCommand(
            IModelTransformation transformation, IModelCommand<TReturn> innerCommand)
        {
            if (transformation == null)
                throw new ArgumentNullException("transformation");

            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.transformation = transformation;
            this.innerCommand = innerCommand;
        }

        public IModelTransformation Transformation
        {
            get { return this.transformation; }
        }

        public IModelCommand<TReturn> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            var newUser = await this.transformation.TransformAsync(user);
            return await this.innerCommand.ExecuteAsync(newUser);
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
             var newArticle = await this.transformation.TransformAsync(article);
             return await this.innerCommand.ExecuteAsync(newArticle);
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            var newKeyword = await this.transformation.TransformAsync(keyword);
            return await this.innerCommand.ExecuteAsync(newKeyword);
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            var newBookmark = await this.transformation.TransformAsync(bookmark);
            return await this.innerCommand.ExecuteAsync(newBookmark);
        }
    }
}
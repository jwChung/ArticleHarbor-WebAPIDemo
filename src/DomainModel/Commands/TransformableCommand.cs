namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public class TransformableCommand<TReturn> : ModelCommand<TReturn>
    {
        private readonly IModelTransformer transformer;
        private readonly IModelCommand<TReturn> innerCommand;

        public TransformableCommand(
            IModelTransformer transformer, IModelCommand<TReturn> innerCommand)
        {
            if (transformer == null)
                throw new ArgumentNullException("transformer");

            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.transformer = transformer;
            this.innerCommand = innerCommand;
        }

        public IModelTransformer Transformer
        {
            get { return this.transformer; }
        }

        public IModelCommand<TReturn> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            var newUser = await this.transformer.TransformAsync(user);
            return await this.innerCommand.ExecuteAsync(newUser);
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
             var newArticle = await this.transformer.TransformAsync(article);
             return await this.innerCommand.ExecuteAsync(newArticle);
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            var newKeyword = await this.transformer.TransformAsync(keyword);
            return await this.innerCommand.ExecuteAsync(newKeyword);
        }

        public override async Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            var newBookmark = await this.transformer.TransformAsync(bookmark);
            return await this.innerCommand.ExecuteAsync(newBookmark);
        }
    }
}
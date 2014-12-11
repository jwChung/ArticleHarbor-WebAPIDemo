namespace ArticleHarbor.DomainModel.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;

    public class ConditionalCommand<TReturn> : ModelCommand<TReturn>
    {
        private readonly ICommandCondition condition;
        private readonly IModelCommand<TReturn> innerCommand;

        public ConditionalCommand(
            ICommandCondition condition,
            IModelCommand<TReturn> innerCommand)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.condition = condition;
            this.innerCommand = innerCommand;
        }

        public ICommandCondition Condition
        {
            get { return this.condition; }
        }

        public IModelCommand<TReturn> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return this.ExecuteAsyncWith(user);
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            return this.ExecuteAsyncWith(keyword);
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            return this.ExecuteAsyncWith(bookmark);
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(User user)
        {
            return await this.condition.CanExecuteAsync(user)
                ? await this.innerCommand.ExecuteAsync(user)
                : Enumerable.Empty<TReturn>();
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(Article article)
        {
             return await this.condition.CanExecuteAsync(article)
                ? await this.innerCommand.ExecuteAsync(article)
                : Enumerable.Empty<TReturn>();
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(Keyword keyword)
        {
             return await this.condition.CanExecuteAsync(keyword)
                ? await this.innerCommand.ExecuteAsync(keyword)
                : Enumerable.Empty<TReturn>();
        }

        private async Task<IEnumerable<TReturn>> ExecuteAsyncWith(Bookmark bookmark)
        {
             return await this.condition.CanExecuteAsync(bookmark)
                ? await this.innerCommand.ExecuteAsync(bookmark)
                : Enumerable.Empty<TReturn>();
        }
    }
}
namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Threading.Tasks;

    public class ConditionalCommand<TValue> : ModelCommand<TValue>
    {
        private readonly IModelCondition condition;
        private readonly IModelCommand<TValue> innerCommand;

        public ConditionalCommand(
            IModelCondition condition,
            IModelCommand<TValue> innerCommand)
        {
            if (condition == null)
                throw new ArgumentNullException("condition");

            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.condition = condition;
            this.innerCommand = innerCommand;
        }

        public IModelCondition Condition
        {
            get { return this.condition; }
        }

        public IModelCommand<TValue> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public override TValue Value
        {
            get { return this.innerCommand.Value; }
        }

        public override Task<IModelCommand<TValue>> ExecuteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return this.ExecuteAsyncWith(user);
        }

        public override Task<IModelCommand<TValue>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        public override Task<IModelCommand<TValue>> ExecuteAsync(Keyword keyword)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");

            return this.ExecuteAsyncWith(keyword);
        }

        public override Task<IModelCommand<TValue>> ExecuteAsync(Bookmark bookmark)
        {
            if (bookmark == null)
                throw new ArgumentNullException("bookmark");

            return this.ExecuteAsyncWith(bookmark);
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(User user)
        {
            if (!await this.condition.CanExecuteAsync(user))
                return this;

            var newInnerCommand = await this.innerCommand.ExecuteAsync(user);
            return new ConditionalCommand<TValue>(this.condition, newInnerCommand);
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(Article article)
        {
            if (!await this.condition.CanExecuteAsync(article))
                return this;

            var newInnerCommand = await this.innerCommand.ExecuteAsync(article);
            return new ConditionalCommand<TValue>(this.condition, newInnerCommand);
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(Keyword keyword)
        {
            if (!await this.condition.CanExecuteAsync(keyword))
                return this;

            var newInnerCommand = await this.innerCommand.ExecuteAsync(keyword);
            return new ConditionalCommand<TValue>(this.condition, newInnerCommand);
        }

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(Bookmark bookmark)
        {
            if (!await this.condition.CanExecuteAsync(bookmark))
                return this;

            var newInnerCommand = await this.innerCommand.ExecuteAsync(bookmark);
            return new ConditionalCommand<TValue>(this.condition, newInnerCommand);
        }
    }
}
namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ConditionalCommand<TReturn> : ModelCommand<TReturn>
    {
        private readonly IModelCondition condition;
        private readonly IModelCommand<TReturn> innerCommand;

        public ConditionalCommand(
            IModelCondition condition,
            IModelCommand<TReturn> innerCommand)
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

        public IModelCommand<TReturn> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(User user)
        {
            ////if (user == null)
            ////    throw new ArgumentNullException("user");

            ////return this.ExecuteAsyncWith(user);
            return null;
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(Article article)
        {
            ////if (article == null)
            ////    throw new ArgumentNullException("article");

            ////return this.ExecuteAsyncWith(article);
            return null;
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(Keyword keyword)
        {
            ////if (keyword == null)
            ////    throw new ArgumentNullException("keyword");

            ////return this.ExecuteAsyncWith(keyword);
            return null;
        }

        public override Task<IEnumerable<TReturn>> ExecuteAsync(Bookmark bookmark)
        {
            ////if (bookmark == null)
            ////    throw new ArgumentNullException("bookmark");

            ////return this.ExecuteAsyncWith(bookmark);
            return null;
        }

        private Task<IEnumerable<TReturn>> ExecuteAsyncWith(User user)
        {
            ////if (!await this.condition.CanExecuteAsync(user))
            ////    return this;

            ////var newInnerCommand = await this.innerCommand.ExecuteAsync(user);
            ////return new ConditionalCommand<TReturn>(this.condition, newInnerCommand);
            return null;
        }

        private Task<IEnumerable<TReturn>> ExecuteAsyncWith(Article article)
        {
            ////if (!await this.condition.CanExecuteAsync(article))
            ////    return this;

            ////var newInnerCommand = await this.innerCommand.ExecuteAsync(article);
            ////return new ConditionalCommand<TReturn>(this.condition, newInnerCommand);
            return null;
        }

        private Task<IEnumerable<TReturn>> ExecuteAsyncWith(Keyword keyword)
        {
            ////if (!await this.condition.CanExecuteAsync(keyword))
            ////    return this;

            ////var newInnerCommand = await this.innerCommand.ExecuteAsync(keyword);
            ////return new ConditionalCommand<TReturn>(this.condition, newInnerCommand);
            return null;
        }

        private Task<IEnumerable<TReturn>> ExecuteAsyncWith(Bookmark bookmark)
        {
            ////if (!await this.condition.CanExecuteAsync(bookmark))
            ////    return this;

            ////var newInnerCommand = await this.innerCommand.ExecuteAsync(bookmark);
            ////return new ConditionalCommand<TReturn>(this.condition, newInnerCommand);
            return null;
        }
    }
}
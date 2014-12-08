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

        private async Task<IModelCommand<TValue>> ExecuteAsyncWith(User user)
        {
            if (!await this.condition.CanExecuteAsync(user))
                return this;

            var newInnerCommand = await this.innerCommand.ExecuteAsync(user);
            return new ConditionalCommand<TValue>(this.condition, newInnerCommand);
        }
    }
}
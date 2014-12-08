namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

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
            get { throw new NotImplementedException(); }
        }
    }
}
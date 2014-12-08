namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class ConditionalCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IModelCondition condition;
        private readonly IModelCommand<IEnumerable<IModel>> innerCommand;

        public ConditionalCommand(
            IModelCondition condition, IModelCommand<IEnumerable<IModel>> innerCommand)
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

        public IModelCommand<IEnumerable<IModel>> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public override IEnumerable<IModel> Value
        {
            get { throw new NotImplementedException(); }
        }
    }
}
namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class ConditionalCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IModelCommand<IEnumerable<IModel>> innerCommand;

        public ConditionalCommand(IModelCommand<IEnumerable<IModel>> innerCommand)
        {
            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.innerCommand = innerCommand;
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
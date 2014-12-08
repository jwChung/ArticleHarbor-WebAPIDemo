namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class NewInsertCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IModelCommand<IEnumerable<IModel>> innerCommand;

        public NewInsertCommand(
            IModelCommand<IEnumerable<IModel>> innerCommand,
            IEnumerable<IModel> value) : base(value)
        {
            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.innerCommand = innerCommand;
        }

        public IModelCommand<IEnumerable<IModel>> InnerCommand
        {
            get { return this.innerCommand; }
        }
    }
}
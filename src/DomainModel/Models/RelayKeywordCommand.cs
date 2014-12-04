namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;

    public class RelayKeywordCommand : ModelCommand<IEnumerable<IModel>>
    {
        private readonly IModelCommand<IEnumerable<IModel>> innerCommand;
        private readonly Func<string, IEnumerable<string>> nounExtractor;

        public RelayKeywordCommand(
            IModelCommand<IEnumerable<IModel>> innerCommand,
            Func<string, IEnumerable<string>> nounExtractor)
        {
            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            if (nounExtractor == null)
                throw new ArgumentNullException("nounExtractor");

            this.innerCommand = innerCommand;
            this.nounExtractor = nounExtractor;
        }

        public IModelCommand<IEnumerable<IModel>> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public Func<string, IEnumerable<string>> NounExtractor
        {
            get { return this.nounExtractor; }
        }

        public override IEnumerable<IModel> Value
        {
            get { return this.innerCommand.Value; }
        }
    }
}
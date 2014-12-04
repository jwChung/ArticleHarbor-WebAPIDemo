namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public override Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        private async Task<IModelCommand<IEnumerable<IModel>>> ExecuteAsyncWith(Article article)
        {
            var innerCommand = await this.innerCommand.ExecuteAsync(article);
            
            var insertedArticle = innerCommand.Value.OfType<Article>().Single();
            var keywords = this.nounExtractor(insertedArticle.Subject)
                .Select(w => new Keyword(insertedArticle.Id, w));

            innerCommand = await innerCommand.ExecuteAsync(keywords);

            return new RelayKeywordCommand(innerCommand, this.nounExtractor);
        }
    }
}
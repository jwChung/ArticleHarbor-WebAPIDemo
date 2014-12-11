namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RelayKeywordsCommand : ModelCommand<IModel>
    {
        private readonly Func<string, IEnumerable<string>> nounExtractor;
        private readonly IModelCommand<IModel> innerCommand;

        public RelayKeywordsCommand(
            Func<string, IEnumerable<string>> nounExtractor,
            IModelCommand<IModel> innerCommand)
        {
            if (nounExtractor == null)
                throw new ArgumentNullException("nounExtractor");

            if (innerCommand == null)
                throw new ArgumentNullException("innerCommand");

            this.innerCommand = innerCommand;
            this.nounExtractor = nounExtractor;
        }

        public Func<string, IEnumerable<string>> NounExtractor
        {
            get { return this.nounExtractor; }
        }

        public IModelCommand<IModel> InnerCommand
        {
            get { return this.innerCommand; }
        }

        public override Task<IEnumerable<IModel>> ExecuteAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.ExecuteAsyncWith(article);
        }

        private async Task<IEnumerable<IModel>> ExecuteAsyncWith(Article article)
        {
            var keywords = this.nounExtractor(article.Subject)
                .Select(w => new Keyword(article.Id, w));

            var values = Enumerable.Empty<IModel>();

            foreach (var keyword in keywords)
                values = values.Concat(await this.innerCommand.ExecuteAsync(keyword));

            return values;
        }
    }
}
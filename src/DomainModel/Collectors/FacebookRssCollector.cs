namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public class FacebookRssCollector : IArticleCollector
    {
        private readonly string actor;
        private readonly string facebookId;

        public FacebookRssCollector(string actor, string facebookId)
        {
            if (actor == null)
                throw new ArgumentNullException("actor");

            if (facebookId == null)
                throw new ArgumentNullException("facebookId");

            this.actor = actor;
            this.facebookId = facebookId;
        }

        public string Actor
        {
            get { return this.actor; }
        }

        public string FacebookId
        {
            get { return this.facebookId; }
        }

        public Task<IEnumerable<Article>> CollectAsync()
        {
            throw new NotImplementedException();
        }
    }
}
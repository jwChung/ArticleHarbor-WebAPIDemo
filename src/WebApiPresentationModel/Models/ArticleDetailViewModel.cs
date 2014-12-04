namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;
    using System.Collections.Generic;
    using DomainModel.Models;

    public class ArticleDetailViewModel
    {
        private readonly Article article;
        private readonly IEnumerable<Keyword> keywords;

        public ArticleDetailViewModel(Article article, IEnumerable<Keyword> keywords)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            if (keywords == null)
                throw new ArgumentNullException("keywords");

            this.article = article;
            this.keywords = keywords;
        }

        public Article Article
        {
            get { return this.article; }
        }

        public IEnumerable<Keyword> Keywords
        {
            get { return this.keywords; }
        }
    }
}
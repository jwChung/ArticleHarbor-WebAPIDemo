namespace ArticleHarbor.DomainModel.Collectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class DelegateTransformation : IArticleTransformation
    {
        private readonly Func<Article, Article> transformer;

        public DelegateTransformation(Func<Article, Article> transformer)
        {
            if (transformer == null)
                throw new ArgumentNullException("transformer");

            this.transformer = transformer;
        }

        public Func<Article, Article> Transformer
        {
            get { return this.transformer; }
        }

        public Article Transform(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            return this.transformer(article);
        }

        public IEnumerable<Article> Transform(IEnumerable<Article> articles)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            return articles.Select(this.transformer);
        }
    }
}
namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articles;
        private readonly IArticleWordRepository articleWords;
        private readonly Func<string, IEnumerable<string>> nounExtractor;

        public ArticleService(
            IArticleRepository articles,
            IArticleWordRepository articleWords,
            Func<string, IEnumerable<string>> nounExtractor)
        {
            if (articles == null)
                throw new ArgumentNullException("articles");

            if (articleWords == null)
                throw new ArgumentNullException("articleWords");

            if (nounExtractor == null)
                throw new ArgumentNullException("nounExtractor");

            this.articles = articles;
            this.articleWords = articleWords;
            this.nounExtractor = nounExtractor;
        }

        public IArticleRepository Articles
        {
            get { return this.articles; }
        }

        public IArticleWordRepository ArticleWords
        {
            get { return this.articleWords; }
        }

        public Func<string, IEnumerable<string>> NounExtractor
        {
            get { return this.nounExtractor; }
        }

        public Task<IEnumerable<Article>> GetAsync()
        {
            return this.articles.SelectAsync();
        }

        public async Task<Article> AddOrModifyAsync(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            var oldArticle = this.articles.Select(article.Id);
            if (oldArticle == null)
            {
                var newArticle = await this.articles.InsertAsync(article);

#pragma warning disable 4014
                Task.Run(() => this.InsertArticleWords(newArticle)).ContinueWith(
                    t => ThreadPool.QueueUserWorkItem(x => { throw t.Exception; }),
                    TaskContinuationOptions.OnlyOnFaulted);
#pragma warning restore 4014

                return newArticle;
            }

            this.articles.Update(article);
            return article;
        }

        public void Remove(int id)
        {
            this.articles.Delete(id);
        }

        private void InsertArticleWords(Article newArticle)
        {
            var words = this.nounExtractor(newArticle.Subject);
            foreach (var word in words)
            {
                var articleWord = new ArticleWord(word, newArticle.Id);
                this.ArticleWords.Insert(articleWord);
            }
        }
    }
}
namespace WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using DomainModel;

    public class ArticlesController : ApiController
    {
        private readonly IArticleRepository repository;

        public ArticlesController(IArticleRepository repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            this.repository = repository;
        }

        public IArticleRepository Repository
        {
            get { return this.repository; }
        }

        public IEnumerable<Article> Get()
        {
            return this.repository.Select();
        }
    }
}
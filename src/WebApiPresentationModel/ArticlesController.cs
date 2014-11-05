namespace WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        public async Task<IEnumerable<Article>> GetAsync()
        {
            return await this.repository.SelectAsync();
        }
    }
}
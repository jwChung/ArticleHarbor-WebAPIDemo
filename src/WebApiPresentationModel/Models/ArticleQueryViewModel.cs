namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;
    using DomainModel.Queries;

    public class ArticleQueryViewModel : ISqlQueryable
    {
        public ISqlQuery ProvideQuery()
        {
            throw new NotImplementedException();
        }
    }
}
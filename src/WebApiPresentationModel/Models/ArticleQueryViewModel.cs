namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;
    using System.Globalization;
    using DomainModel.Queries;

    public class ArticleQueryViewModel : ISqlQueryable
    {
        private const int MaxCount = 50;
        private int count = MaxCount;
        
        public int Count
        {
            get
            {
                return this.count;
            }

            set
            {
                if (value > MaxCount)
                    throw new ArgumentException(string.Format(
                        CultureInfo.CurrentCulture,
                        "The count cannot exceed '{0}'.",
                        MaxCount));

                this.count = value;
            }
        }

        public ISqlQuery ProvideQuery()
        {
            throw new NotImplementedException();
        }
    }
}
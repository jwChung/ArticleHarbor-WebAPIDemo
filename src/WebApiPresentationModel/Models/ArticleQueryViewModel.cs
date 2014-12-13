namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;
    using System.Collections.Generic;
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
                        "The count cannot exceed '{0}', but '{1}'.",
                        MaxCount,
                        value));

                this.count = value;
            }
        }

        public int? PreviousId { get; set; }

        public string Subject { get; set; }

        public ISqlQuery ProvideQuery()
        {
            var predicates = new List<IPredicate>();
            
            if (this.PreviousId != null)
                predicates.Add(Predicate.GreatThan("Id", this.PreviousId));
            
            return new SqlQuery(
                new Top(this.count),
                OrderByColumns.None,
                Predicate.And(predicates.ToArray()));
        }
    }
}
namespace ArticleHarbor.WebApiPresentationModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using DomainModel.Queries;

    public class ArticleQueryViewModel : ISqlQueryable
    {
        public const int MaxCount = 100;
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

        public string Body { get; set; }

        public ISqlQuery ProvideQuery()
        {
            var predicates = new List<IPredicate>();
            
            if (this.PreviousId != null)
                predicates.Add(Predicate.GreatThan("Id", this.PreviousId));

            if (!string.IsNullOrEmpty(this.Subject))
                predicates.Add(Predicate.Like("Subject", "%" + this.Subject + "%"));

            if (!string.IsNullOrEmpty(this.Body))
                predicates.Add(Predicate.Like("Body", "%" + this.Body + "%"));
            
            return new SqlQuery(
                new Top(this.count),
                OrderByColumns.None,
                Predicate.And(predicates.ToArray()));
        }
    }
}
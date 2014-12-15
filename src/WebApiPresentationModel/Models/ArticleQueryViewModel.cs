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

        public ArticleQueryViewModel()
        {
            this.Before = DateTime.Now;
        }

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

        public DateTime Before { get; set; }

        public TimeSpan? Duration { get; set; }

        public string Provider { get; set; }

        public string UserId { get; set; }

        public virtual ISqlQuery ProvideQuery()
        {
            var predicates = new List<IPredicate>();
            
            if (this.PreviousId != null)
                predicates.Add(Predicate.GreatThan("Id", this.PreviousId));

            if (!string.IsNullOrEmpty(this.Subject))
                predicates.Add(Predicate.Contains("Subject", this.Subject));

            if (!string.IsNullOrEmpty(this.Body))
                predicates.Add(Predicate.Contains("Body", this.Body));

            if (this.Duration != null)
            {
                predicates.Add(Predicate.GreatOrEqualThan("Date", this.Before - this.Duration));
                predicates.Add(Predicate.LessOrEqualThan("Date", this.Before));
            }

            if (!string.IsNullOrEmpty(this.Provider))
                predicates.Add(Predicate.Contains("Provider", this.Provider));

            if (!string.IsNullOrEmpty(this.UserId))
                predicates.Add(Predicate.Contains("UserId", this.UserId));
            
            return new SqlQuery(
                new Top(this.count),
                OrderByColumns.None,
                predicates.Count == 0 ? Predicate.None : Predicate.And(predicates.ToArray()));
        }
    }
}
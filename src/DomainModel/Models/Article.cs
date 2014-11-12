namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class Article
    {
        private readonly string provider;
        private readonly string no;
        private readonly string subject;
        private readonly string body;
        private readonly DateTime date;
        private readonly string url;
        private readonly int id;
        private readonly string userId;

        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "6#", Justification = "To match database entity.")]
        public Article(
            int id,
            string provider,
            string no,
            string subject,
            string body,
            DateTime date,
            string url,
            string userId)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (no == null)
                throw new ArgumentNullException("no");

            if (subject == null)
                throw new ArgumentNullException("subject");

            if (body == null)
                throw new ArgumentNullException("body");

            if (url == null)
                throw new ArgumentNullException("url");

            if (userId == null)
                throw new ArgumentNullException("userId");

            if (provider.Length == 0)
                throw new ArgumentException(
                    "The 'provider' value should not be empty string.", "provider");

            if (no.Length == 0)
                throw new ArgumentException(
                    "The 'no' value should not be empty string.", "no");

            if (subject.Length == 0)
                throw new ArgumentException(
                    "The 'subject' value should not be empty string.", "subject");

            if (body.Length == 0)
                throw new ArgumentException(
                    "The 'body' value should not be empty string.", "body");

            if (url.Length == 0)
                throw new ArgumentException(
                    "The 'url' value should not be empty string.", "url");

            if (userId.Length == 0)
                throw new ArgumentException(
                    "The 'userId' value should not be empty string.", "userId");

            this.id = id;
            this.provider = provider;
            this.no = no;
            this.subject = subject;
            this.body = body;
            this.date = date;
            this.url = url;
            this.userId = userId;
        }

        public int Id
        {
            get { return this.id; }
        }

        public string Provider
        {
            get { return this.provider; }
        }

        public string No
        {
            get { return this.no; }
        }

        public string Subject
        {
            get { return this.subject; }
        }

        public string Body
        {
            get { return this.body; }
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "데이터베이스에 저장하기 위해 문자열로 취급.")]
        public string Url
        {
            get { return this.url; }
        }

        public string UserId
        {
            get { return this.userId; }
        }

        public Article WithId(int newId)
        {
            return new Article(
                newId,
                this.provider,
                this.no,
                this.subject,
                this.body,
                this.date,
                this.url,
                this.userId);
        }

        public Article WithSubject(string newSubject)
        {
            return new Article(
                this.id,
                this.provider,
                this.no,
                newSubject,
                this.body,
                this.date,
                this.url,
                this.userId);
        }

        public Article WithUserId(string newUserId)
        {
            return new Article(
                this.id,
                this.provider,
                this.no,
                this.subject,
                this.body,
                this.date,
                this.url,
                newUserId);
        }

        public Article WithBody(string newBody)
        {
            return new Article(
                this.id,
                this.provider,
                this.no,
                this.subject,
                newBody,
                this.date,
                this.url,
                this.userId);
        }
    }
}
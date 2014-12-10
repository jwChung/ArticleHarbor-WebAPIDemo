namespace ArticleHarbor.DomainModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    public class Article : IModel
    {
        private readonly string provider;
        private readonly string guid;
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
            string guid,
            string subject,
            string body,
            DateTime date,
            string url,
            string userId)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (guid == null)
                throw new ArgumentNullException("guid");

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

            if (guid.Length == 0)
                throw new ArgumentException(
                    "The 'guid' value should not be empty string.", "guid");

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
            this.guid = guid;
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

        public string Guid
        {
            get { return this.guid; }
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
                this.guid,
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
                this.guid,
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
                this.guid,
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
                this.guid,
                this.subject,
                newBody,
                this.date,
                this.url,
                this.userId);
        }

        public IKeys GetKeys()
        {
            return new Keys<int>(this.id);
        }

        public Task<IEnumerable<TReturn>> ExecuteAsync<TReturn>(IModelCommand<TReturn> command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            return command.ExecuteAsync(this);
        }
    }
}
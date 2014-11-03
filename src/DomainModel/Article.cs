namespace DomainModel
{
    using System;

    public class Article
    {
        private readonly string provider;
        private readonly string id;
        private readonly string subject;
        private readonly string body;
        private readonly DateTime date;
        private readonly string url;

        public Article(
            string provider,
            string id,
            string subject,
            string body,
            DateTime date,
            string url)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (id == null)
                throw new ArgumentNullException("id");

            if (subject == null)
                throw new ArgumentNullException("subject");

            if (body == null)
                throw new ArgumentNullException("body");

            if (url == null)
                throw new ArgumentNullException("url");

            this.provider = provider;
            this.id = id;
            this.subject = subject;
            this.body = body;
            this.date = date;
            this.url = url;
        }

        public string Provider
        {
            get { return this.provider; }
        }

        public string Id
        {
            get { return this.id; }
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

        public string Url
        {
            get { return this.url; }
        }
    }
}
namespace DomainModel
{
    using System;

    public class Article
    {
        private readonly string provider;
        private readonly string no;
        private readonly string subject;
        private readonly string body;
        private readonly DateTime date;
        private readonly string url;
        private int id = -1;

        public Article(
            string provider,
            string no,
            string subject,
            string body,
            DateTime date,
            string url)
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

            this.provider = provider;
            this.no = no;
            this.subject = subject;
            this.body = body;
            this.date = date;
            this.url = url;
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

        public string Url
        {
            get { return this.url; }
        }

        public Article WithId(int newId)
        {
            return new Article(
                this.provider, this.no, this.subject, this.body, this.date, this.url)
            {
                id = newId
            };
        }
    }
}
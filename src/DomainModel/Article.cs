namespace DomainModel
{
    using System;

    public class Article
    {
        private readonly string provider;
        private readonly string no;
        private readonly string subject;
        private readonly string summary;
        private readonly DateTime date;
        private readonly Uri url;

        public Article(
            string provider,
            string no,
            string subject,
            string summary,
            DateTime date,
            Uri url)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (no == null)
                throw new ArgumentNullException("no");

            if (subject == null)
                throw new ArgumentNullException("subject");

            if (summary == null)
                throw new ArgumentNullException("summary");

            if (url == null)
                throw new ArgumentNullException("url");

            this.provider = provider;
            this.no = no;
            this.subject = subject;
            this.summary = summary;
            this.date = date;
            this.url = url;
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

        public string Summary
        {
            get { return this.summary; }
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public Uri Url
        {
            get { return this.url; }
        }
    }
}
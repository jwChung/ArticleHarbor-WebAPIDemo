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
        private int id;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "5#", Justification = "데이터베이스에 저장하기 위해 문자열로 취급.")]
        public Article(
            string provider,
            string no,
            string subject,
            string body,
            DateTime date,
            string url) : this(-1, provider, no, subject, body, date, url)
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "6#", Justification = "데이터베이스에 저장하기 위해 문자열로 취급.")]
        public Article(
            int id,
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

            this.id = id;
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "데이터베이스에 저장하기 위해 문자열로 취급.")]
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
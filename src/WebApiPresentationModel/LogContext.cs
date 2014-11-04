namespace WebApiPresentationModel
{
    using System;
    using System.Globalization;

    public class LogContext
    {
        private readonly string requestUrl;
        private readonly string message;
        private readonly DateTime date = DateTime.Now;

        public LogContext(string requestUrl, string message)
        {
            if (requestUrl == null)
                throw new ArgumentNullException("requestUrl");

            if (message == null)
                throw new ArgumentNullException("message");

            this.requestUrl = requestUrl;
            this.message = message;
        }

        public string RequestUrl
        {
            get { return this.requestUrl; }
        }

        public string Message
        {
            get { return this.message; }
        }

        public DateTime Date
        {
            get { return this.date; }
        }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                "Request URL: {1}{0}Date: {2}{0}Message:{0}{3}",
                Environment.NewLine,
                this.requestUrl,
                this.date,
                this.message);
        }
    }
}
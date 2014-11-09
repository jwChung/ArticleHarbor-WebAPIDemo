namespace ArticleHarbor.WebApiPresentationModel
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class FileLogger : ILogger
    {
        private readonly string loggingDirectory;

        public FileLogger(string loggingDirectory)
        {
            if (loggingDirectory == null)
                throw new ArgumentNullException("loggingDirectory");

            this.loggingDirectory = loggingDirectory;
        }

        public string LoggingDirectory
        {
            get { return this.loggingDirectory; }
        }

        public Task LogAsync(LogContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            string path = this.LoggingDirectory + @"\" + Guid.NewGuid().ToString("N") + ".log";
            return Task.Run(() => File.WriteAllText(path, context.ToString()));
        }
    }
}
namespace WebApiPresentationModel
{
    using System;
    using System.Threading.Tasks;

    public class FileLogger : ILogger
    {
        public Task LogAsync(LogContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            throw new NotImplementedException();
        }
    }
}
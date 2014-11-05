namespace WebApiPresentationModel
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;

    public class UnhandledExceptionLogger : ExceptionLogger
    {
        private readonly ILogger logger;

        public UnhandledExceptionLogger(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            this.logger = logger;
        }

        public ILogger Logger
        {
            get { return this.logger; }
        }

        public override Task LogAsync(
            ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return this.logger.LogAsync(
                new LogContext(
                    context.Request.RequestUri,
                    context.Exception.ToString()));
        }

        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (typeof(ArgumentException).IsInstanceOfType(context.Exception))
                return false;

            return base.ShouldLog(context);
        }
    }
}
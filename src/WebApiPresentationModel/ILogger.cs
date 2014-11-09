namespace ArticleHarbor.WebApiPresentationModel
{
    using System.Threading.Tasks;

    public interface ILogger
    {
        Task LogAsync(LogContext context);
    }
}
namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class HttpResponseMessageExtensions
    {
        public static async Task<string> GetMessageAsync(this HttpResponseMessage response)
        {
            return "Actual status code: " + response.StatusCode + Environment.NewLine
                + await GetStringMessage(response);
        }

        private static async Task<string> GetStringMessage(HttpResponseMessage response)
        {
            return response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;
        }
    }
}
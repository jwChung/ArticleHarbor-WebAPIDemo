namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.Net.Http;
    using System.Web.Http.SelfHost;
    using ArticleHarbor.Website;

    public class HttpClientFactory
    {
        public static HttpClient Create()
        {
            var baseAddress = new Uri("http://localhost:30316/");
            var config = new HttpSelfHostConfiguration(baseAddress);
            WebApiConfig.Register(config);
            var server = new HttpSelfHostServer(config);
            var client = new HttpClient(server);
            
            try
            {
                client.BaseAddress = baseAddress;
                return client;
            }
            catch
            {
                client.Dispose();
                throw;
            }
        }
    }
}
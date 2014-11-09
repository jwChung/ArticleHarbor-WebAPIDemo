namespace ArticleHarbor.AcceptanceTest
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.SelfHost;
    using ArticleHarbor.Website;

    public class HttpClientFactory
    {
        public static HttpClient Create()
        {
            var baseAddress = new Uri("http://localhost:30316/");
            var config = new HttpSelfHostConfiguration(baseAddress)
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };
            WebApiConfig.Register(config);
            var server = new HttpSelfHostServer(config);
            var client = new HttpClientOwner(server, config);
            
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

        private class HttpClientOwner : HttpClient
        {
            private readonly HttpMessageHandler handler;
            private readonly IDisposable[] disposables;
            private bool disposed;

            public HttpClientOwner(HttpMessageHandler handler, params IDisposable[] disposables)
                : base(handler)
            {
                this.handler = handler;
                this.disposables = disposables;
            }

            protected override void Dispose(bool disposing)
            {
                if (this.disposed)
                    return;

                if (disposing)
                {
                    this.handler.Dispose();
                    foreach (var disposable in this.disposables)
                        disposable.Dispose();
                }

                base.Dispose(disposing);
                this.disposed = true;
            }
        }
    }
}
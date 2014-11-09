#if !CI
namespace ArticleHarbor.Website
{
    using System;
    using System.Web.Http;

    [RoutePrefix("api/throw")]
    public class ThrowController : ApiController
    {
        [HttpGet]
        [Route("ArgumentException")]
        public void ThrowArgumentException()
        {
            throw new ArgumentException("anonymous");
        }

        [HttpGet]
        [Route("InvalidOperationException")]
        public void ThrowInvalidOperationException()
        {
            throw new InvalidOperationException();
        }
    }
}
#endif
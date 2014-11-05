#if !CI
namespace Website
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
            throw new ArgumentException();
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
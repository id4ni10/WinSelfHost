using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WinSelfHost.Controllers
{
    public class ErrorController : ApiController
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public HttpResponseMessage Handle404()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

            responseMessage.ReasonPhrase = "O recurso solicitado não foi encontrado!";

            return responseMessage;
        }
    }
}

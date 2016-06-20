using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http;

namespace WinSelfHost.Controllers
{
    public class DefaultController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var output = JsonConvert.SerializeObject(new { id = 1, nome = "danilo" }, Formatting.Indented);

            return new HttpResponseMessage()
            {
                Content = new StringContent(output, Encoding.UTF8, "application/json")
            };
        }
    }
}

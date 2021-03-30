using System.Web.Http;

namespace UDPServer.Application.Controllers
{
    public class MessageController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Search(string query)
        {
            return Ok(query);
        }
    }
}
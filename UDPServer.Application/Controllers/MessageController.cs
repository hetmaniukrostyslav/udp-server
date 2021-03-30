using System.Linq;
using System.Web.Http;
using UDPServer.Persistence.Context;

namespace UDPServer.Application.Controllers
{
    public class MessageController : ApiController
    {
        public MessageController()
        {
            
        }

        [HttpGet]
        public IHttpActionResult Search(string query)
        {
            return Ok(query);
        }
    }
}
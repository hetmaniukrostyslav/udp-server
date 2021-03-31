using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using UDPServer.Application.Helper;
using UDPServer.Persistence.Context;
using UDPServer.Persistence.Models;
using UDPServer.Persistence.Repositories;

namespace UDPServer.Application.Controllers
{
    public class MessageController : ApiController
    {
        private readonly MessageRepository _messageRepository;

        public MessageController(MessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> FindManyAsync(CancellationToken cancellationToken,
                                                            string address = "",
                                                            double? startDate = null,
                                                            double? endDate = null)
        {
            var isAddressEmpty = string.IsNullOrWhiteSpace(address);
            var startDateValue = DateTimeHelper.FromUnixTime(startDate.HasValue ? startDate.Value : 0);
            var endDateValue = DateTimeHelper.FromUnixTime(endDate.HasValue ? endDate.Value : 0);

            var result = await _messageRepository.FindManyAsync(x => (isAddressEmpty || x.Text.ToLower() == address.ToLower()) &&
                                                                       (!startDate.HasValue || x.CreatedAt >= startDateValue) &&
                                                                       (!endDate.HasValue || x.CreatedAt <= endDateValue),
                                                                       cancellationToken);

            return Ok(result);
        }
    }
}
namespace ChatSystem.MessageHistoryAPI.Controllers
{
    using ChatSystem.MessageHistoryAPI.Contracts;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    [ApiController]
    [Route("/api")]
    public class HomeController : AbstractController
    {
        private readonly IMessageManager manager;

        public HomeController(IMessageManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("messages/{count}")]
        public IEnumerable<Message> Get(CancellationToken cancellationToken, int count = default)
        {
            return CreateOkOrErrorResult(manager.Get(cancellationToken, count));
        }

        [HttpGet("messages")]
        public IEnumerable<Message> GetForPeriod([FromBody] TimeFrame timeFrame, CancellationToken cancellationToken)
        {
            return CreateOkOrErrorResult(manager.GetPerTimePeriod(timeFrame, cancellationToken));
        }

        [HttpPost("messages")]
        public async Task SaveAsync([FromBody] IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            await CreateOkOrErrorResult(manager.SaveAsync(messages, cancellationToken));
        }
    }
}

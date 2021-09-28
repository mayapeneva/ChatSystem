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
        public IActionResult Get(CancellationToken cancellationToken, int count = default)
        {
            return CreateOkOrErrorResult(manager.Get(cancellationToken, count));
        }

        [HttpGet("messages")]
        public IActionResult GetForPeriod([FromBody] TimeFrame timeFrame, CancellationToken cancellationToken)
        {
            return CreateOkOrErrorResult(manager.GetPerTimePeriod(timeFrame, cancellationToken));
        }

        [HttpPost("messages")]
        public async Task<IActionResult> SaveAsync([FromBody] IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            return CreateOkOrErrorResult(await manager.SaveAsync(messages, cancellationToken));
        }
    }
}

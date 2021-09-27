namespace ChatSystem.MessageHistoryAPI.Controllers
{
    using Infrastructure.Common;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public class AbstractController : ControllerBase
    {
        protected IActionResult CreateOkOrErrorResult<T>(InternalResult<T> result, bool returnData = true)
        {
            return !result.IsSuccess
                ? CreateErrorResult(result)
                : returnData
                ? Ok(result)
                : (IActionResult)Ok();
        }

        protected IActionResult CreateErrorResult<T>(InternalResult<T> result)
        {
            switch (result.Code)
            {
                case InternalStatusCode.Conflict:
                    return Conflict(result);
                case InternalStatusCode.BadRequest:
                    return BadRequest(result);
                case InternalStatusCode.NotFound:
                    return NotFound(result);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError, result);
            }
        }
    }
}

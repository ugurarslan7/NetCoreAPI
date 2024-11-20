using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result )
        {
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
            }

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return Created(result.UrlAsCreated, result);
            }

            return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };

        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
            }
            return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };

        }
    }
}

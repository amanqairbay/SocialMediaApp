using System;
using Core.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiException(code));
            //return new ObjectResult(new ApiException(code));
        }
    }
}


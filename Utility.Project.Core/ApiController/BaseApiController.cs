using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Extensions;
using Utility.Project.Core.Model.Response;

namespace Utility.Project.Core.ApiController
{
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {

        }

        protected virtual IActionResult ApiResponse<T>(List<T> data)
        {
            if (data.IsNotNullOrEmpty())
                return Ok(data);
            else return NotFound(new ApiResponse { IsSuccessful = false, ErrorMessageList = new List<string> { "Not found." } });
        }

        protected virtual IActionResult ApiResponse<T>(T data)
        {
            if (data.IsNotNull())
                return Ok(data);
            else return NotFound(new ApiResponse { IsSuccessful = false, ErrorMessageList = new List<string> { "Not found." } });
        }

        protected virtual IActionResult ApiResponse(DataResponse result = null)
        {
            if (result.IsSuccessful)
            {
                switch (result.HttpStatusCode)
                {
                    case HttpStatusCode.OK:
                        return Ok(result.Document);
                    case HttpStatusCode.Created:
                        return Created("", result.Document);
                    case HttpStatusCode.Accepted:
                        return Accepted(result.Document);
                    case HttpStatusCode.NoContent:
                        return NoContent();
                }
            }
            else
            {
                switch (result.HttpStatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return BadRequest(new ErrorDataResonse(result.ErrorMessageList, result.ErrorCode, HttpStatusCode.BadRequest));
                    case HttpStatusCode.Unauthorized:
                        return Unauthorized(new { Message = "Unauthorized request" });
                    case HttpStatusCode.Forbidden:
                        return Forbid();
                    case HttpStatusCode.NotFound:
                        return NotFound();
                    default:
                        return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}

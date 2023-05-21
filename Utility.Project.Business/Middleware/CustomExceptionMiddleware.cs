using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Extensions;
using Utility.Project.Core.Model.Response;
using static System.Net.Mime.MediaTypeNames;

namespace Utility.Project.Business.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            List<string> Errors = new List<string>();

            if (exception != null && exception.Message.IsNotNullOrEmpty())
                Errors.Add(exception.Message);

            string errorMessage = "Unexpected error occured!";

            context.Response.ContentType = "application/json";

            StackTrace trace = new StackTrace(exception, true);

            var jsonObject = JsonConvert.SerializeObject(new DataResponse()
            {
                ErrorCode = context.Response.StatusCode.ToString(),
                ErrorMessageList = Errors.IsNotNullOrEmpty() ? Errors : new List<string> { errorMessage },
                IsSuccessful = false
            });

            return context.Response.WriteAsync(jsonObject, Encoding.UTF8);
        }

    }
}

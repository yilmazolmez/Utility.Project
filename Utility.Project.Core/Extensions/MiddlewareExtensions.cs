using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Core.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware<T>(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<T>();
        }
        public static IApplicationBuilder UseExceptionMiddleware<T>(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<T>();
        }
    }
}

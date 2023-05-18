using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Core.Extensions
{
    public static class FlurlExtensions
    {

        public static IFlurlRequest ToFlurlRequest(this string baseUrl, int timeoutSeconds = 30)
        {
            IFlurlRequest result = baseUrl.AllowAnyHttpStatus()
                .WithTimeout(timeoutSeconds);
            return result;
        }

        public static HttpResponseMessage Get(this IFlurlRequest request)
        {
            var task = Task.Run(async () => await request.GetAsync());

            HttpResponseMessage response = task.Result?.ResponseMessage;

            return response;
        }

        public static HttpResponseMessage Post(this IFlurlRequest request, object obj)
        {
            var task = Task.Run(async () => await request.PostJsonAsync(obj));

            HttpResponseMessage response = task.Result?.ResponseMessage;

            return response;
        }

        public static HttpResponseMessage Patch(this IFlurlRequest request, object obj)
        {
            var task = Task.Run(async () => await request.PatchJsonAsync(obj));

            HttpResponseMessage response = task.Result?.ResponseMessage;

            return response;
        }

        public static HttpResponseMessage Put(this IFlurlRequest request, object obj)
        {
            var task = Task.Run(async () => await request.PutJsonAsync(obj));

            HttpResponseMessage response = task.Result?.ResponseMessage;

            return response;
        }

        public static HttpResponseMessage Delete(this IFlurlRequest request)
        {
            var task = Task.Run(async () => await request.DeleteAsync());

            HttpResponseMessage response = task.Result.ResponseMessage;

            return response;
        }




        public static async Task<T> GetResultAsync<T>(this HttpResponseMessage response) where T : class
        {
            T result = await response.Content?.ReadFromJsonAsync<T>();

            return result;
        }

        public static string GetResult(this HttpResponseMessage response)
        {
            var task = Task.Run(async () => await response.Content?.ReadAsStringAsync());

            string result = task.Result;

            return result;
        }

        public static T GetResult<T>(this HttpResponseMessage response) where T : class
        {
            var task = Task.Run(async () => await response.Content?.ReadFromJsonAsync<T>());

            T result = task.Result;

            return result;
        }

    }
}

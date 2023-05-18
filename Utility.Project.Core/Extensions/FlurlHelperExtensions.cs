using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Core.Extensions
{
    public static class FlurlHelperExtensions
    {
        public static void Validate(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string message = response.GetResult();
                Exception error = new Exception(message);

                throw response.StatusCode switch
                {
                    HttpStatusCode.BadRequest => new ApplicationException(message),
                    HttpStatusCode.NotFound => new ApplicationException("404", error),
                    HttpStatusCode.ExpectationFailed => new ApplicationException("417", error),

                    _ => new ApplicationException(response.ReasonPhrase, error),
                };
            }
        }


        public static T GetAndValidate<T>(this IFlurlRequest request) where T : class
        {
            HttpResponseMessage response = request.Get();

            response.Validate();

            T result = GetResult<T>(response);

            return result;
        }

        public static T PostAndValidate<T>(this IFlurlRequest request, object obj) where T : class
        {
            HttpResponseMessage response = request.Post(obj);

            response.Validate();

            T result = GetResult<T>(response);

            return result;
        }

        public static T PatchAndValidate<T>(this IFlurlRequest request, object obj) where T : class
        {
            HttpResponseMessage response = request.Patch(obj);

            response.Validate();

            T result = GetResult<T>(response);

            return result;
        }

        public static T PutAndValidate<T>(this IFlurlRequest request, object obj) where T : class
        {
            HttpResponseMessage response = request.Put(obj);

            response.Validate();

            T result = GetResult<T>(response);

            return result;
        }

        public static void DeleteAndValidate(this IFlurlRequest request)
        {
            HttpResponseMessage response = request.Delete();

            response.Validate();
        }




        private static T GetResult<T>(HttpResponseMessage response) where T : class
        {
            T result;//ToDo: Farklı senaryoları dene
            if (typeof(T) == typeof(string))
                result = response.GetResult() as T;
            else
                result = response.GetResult<T>();
            return result;
        }
    }
}

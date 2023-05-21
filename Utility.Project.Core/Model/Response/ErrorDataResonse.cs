using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Project.Core.Model.Response
{
    public class ErrorDataResonse : DataResponse
    {
        public ErrorDataResonse(List<string> errorMessages, string errorCode, HttpStatusCode httpStatusCode)
        {
            IsSuccessful = false;
            ErrorMessageList = errorMessages;
            ErrorCode = errorCode;
            HttpStatusCode = httpStatusCode;
        }
    }
}

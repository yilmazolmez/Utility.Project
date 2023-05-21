using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Extensions;

namespace Utility.Project.Core.Model.Response
{
    public class ApiResponse
    {
        private List<ValidationFailure> _validationResult = new List<ValidationFailure>();
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool IsSuccessful { get; set; } = false;
        public string ErrorCode { get; set; }
        public List<string> ErrorMessageList { get; set; } = new List<string> { };
        public List<ValidationFailure> ValidationResult
        {
            get
            {
                if (_validationResult.IsNotNullOrEmpty())
                    return _validationResult;
                else
                {
                    List<ValidationFailure> tempValidFailList = new List<ValidationFailure>();
                    if (ErrorMessageList.Any())
                    {
                        foreach (string errorItem in ErrorMessageList)
                        {
                            tempValidFailList.Add(new ValidationFailure("", "")
                            {
                                ErrorMessage = errorItem
                            });
                        }
                    }

                    _validationResult = tempValidFailList;

                    return _validationResult;
                }
            }
            set { _validationResult = value; }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Model.Error;

namespace Utility.Project.Model.Constant.Error
{
    public class ProductErrorConstant
    {
        public static BaseServiceErrorConstant MODEL_CANNOT_BE_NULL => new BaseServiceErrorConstant { HttpStatusCode = HttpStatusCode.BadRequest, Code = "Product001", Message = "Model cannot be null." };
        public static BaseServiceErrorConstant MODEL_PROPERTY_CANNOT_BE_NULL(string propertyName) => new BaseServiceErrorConstant { HttpStatusCode = HttpStatusCode.BadRequest, Code = "Product002", Message = $"Model {propertyName} cannot be null." };
        public static BaseServiceErrorConstant MODEL_PROPERTY_FORMAT_NOT_VALID(string propertyName) => new BaseServiceErrorConstant { HttpStatusCode = HttpStatusCode.BadRequest, Code = "Productr003", Message = $"Model {propertyName} format not valid." };
        public static BaseServiceErrorConstant NOT_FOUND => new BaseServiceErrorConstant { HttpStatusCode = HttpStatusCode.BadRequest, Code = "Product004", Message = $"There is no model with provided Id." };
    }
}

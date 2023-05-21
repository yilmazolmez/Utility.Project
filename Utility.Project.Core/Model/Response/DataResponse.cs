using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Extensions;
using Utility.Project.Core.Model.Document;

namespace Utility.Project.Core.Model.Response
{
    public class DataResponse : ApiResponse
    {
        private IDocument? _data;

        public IDocument? Document
        {
            get { return _data; } 
            set
            {
                _data = value;

                if (_data.IsNotNull())
                {
                    IsSuccessful = true;
                }
                else if (!IsSuccessful &&  this.ErrorMessageList.IsNullOrEmpty())
                {
                    ErrorMessageList = new List<string> { "Not found." };
                }
            }
        }
    }
}

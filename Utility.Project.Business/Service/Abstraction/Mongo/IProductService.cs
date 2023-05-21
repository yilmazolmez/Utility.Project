using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Business.Abstraction.Mongo;
using Utility.Project.Core.Model.Response;
using Utility.Project.Model.Document;

namespace Utility.Project.Business.Service.Abstraction.Mongo
{
    public interface IProductService : IMongoService<Product>
    {
        List<Product> GettAll();
        DataResponse Add(Product document);
        DataResponse Update(Product document);
        DataResponse Patch(Product document);
        DataResponse Delete(Product document);
        void InsertMany(List<Product> document);
    }
}

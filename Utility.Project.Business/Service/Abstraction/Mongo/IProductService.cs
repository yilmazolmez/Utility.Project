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
        Task<List<Product>> GettAll();
        Task<DataResponse> Add(Product document);
        Task<DataResponse> Update(Product document);
        Task<DataResponse> Patch(Product document);
        Task<DataResponse> Delete(Product document);
        Task InsertMany(List<Product> document);
    }
}

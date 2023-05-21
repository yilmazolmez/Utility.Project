using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Business.Service.Abstraction.Mongo;
using Utility.Project.Core.Business.Concrete.Mongo;
using Utility.Project.Core.Data.Abstraction.Mongo;
using Utility.Project.Core.Extensions;
using Utility.Project.Core.Model.Response;
using Utility.Project.Model.Constant.Error;
using Utility.Project.Model.Document;

namespace Utility.Project.Business.Service.Concrete.Mongo
{
    public class ProductService : MongoService<Product>, IProductService
    {
        public ProductService(IMongoRepository<Product> _mongoRepository) : base(_mongoRepository)
        {
             
        }

        Task<List<Product>> IProductService.GettAll()
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> Add(Product document)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> Update(Product document)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> Patch(Product document)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse> Delete(Product document)
        {
            throw new NotImplementedException();
        }

        public Task InsertMany(List<Product> document)
        {
            throw new NotImplementedException();
        }
    }
}

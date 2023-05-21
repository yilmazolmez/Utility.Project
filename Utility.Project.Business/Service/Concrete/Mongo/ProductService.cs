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

        public List<Product> GettAll()
        {
            return base.GetAllWithoutFilter().ToList();
        }

        public DataResponse Add(Product document)
        {
            //DataResponse response = _businessRuleEngine.Validate(CheckInsertProperties(document));
            //if (!response.IsSuccessful)
            //    return response;

            Product insertedProduct = base.InsertOne(document);

            if (insertedProduct.IsNotNull())
                return new DataResponse { Document = insertedProduct, HttpStatusCode = HttpStatusCode.Created };
            else
                return new DataResponse { ErrorMessageList = new List<string> { "An error occured while inserting." }, ErrorCode = "", HttpStatusCode = HttpStatusCode.BadRequest };
        }
        public DataResponse Update(Product document)
        {
            //DataResponse response = _businessRuleEngine.Validate(CheckUpdateProperties(document));
            //if (!response.IsSuccessful)
            //    return response;

            Product updatedProduct = base.ReplaceOne(document);

            if (updatedProduct.IsNotNull())
                return new DataResponse { Document = updatedProduct, HttpStatusCode = HttpStatusCode.Created };
            else
                return new DataResponse { ErrorMessageList = new List<string> { "An error occured while updated." }, ErrorCode = "", HttpStatusCode = HttpStatusCode.BadRequest };
        }
        public DataResponse Patch(Product document)
        {
            //DataResponse response = _businessRuleEngine.Validate(CheckUpdateProperties(document));
            //if (!response.IsSuccessful)
            //    return response;
             
            Product updatedProduct = base.ReplaceOne(document);

            if (updatedProduct.IsNotNull())
                return new DataResponse { Document = updatedProduct, HttpStatusCode = HttpStatusCode.Created };
            else
                return new DataResponse { ErrorMessageList = new List<string> { "An error occured while patch." }, ErrorCode = "", HttpStatusCode = HttpStatusCode.BadRequest };
        }
        public DataResponse Delete(Product document)
        {
            //DataResponse response = _businessRuleEngine.Validate(CheckDeleteProperties(document));
            //if (!response.IsSuccessful)
            //    return response;

            bool deletedProduct = base.DeleteOne(document.Id);

            if (deletedProduct)
                return new DataResponse { Document = document, HttpStatusCode = HttpStatusCode.Created };
            else
                return new DataResponse { ErrorMessageList = new List<string> { "An error occured while deleted." }, ErrorCode = "", HttpStatusCode = HttpStatusCode.BadRequest };
        }

        public void InsertMany(List<Product> document)
        {
            base.InsertMany(document);
        }


        public DataResponse CheckInsertProperties(Product document)
        {
            if (document.IsNull())
                return ErrorDataResponse(ProductErrorConstant.MODEL_CANNOT_BE_NULL);

            foreach (PropertyInfo item in document.GetType().GetProperties())
            {
                var value = item.GetValue(document);

                if (value.IsNotNull())
                {
                    try
                    {
                        if (item.PropertyType == typeof(string))
                            if (value.ToString().IsNullOrEmpty())
                                return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_CANNOT_BE_NULL(item.Name));

                        if (item.PropertyType == typeof(Int32))
                            if (Convert.ToInt32(value) == 0)
                                return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_CANNOT_BE_NULL(item.Name));

                        if (item.PropertyType == typeof(double))
                            if (Convert.ToDouble(value) == 0)
                                return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_CANNOT_BE_NULL(item.Name));

                        if (item.PropertyType == typeof(DateTime))
                            if (Convert.ToDateTime(value) == null || Convert.ToDateTime(value) == DateTime.MinValue)
                                return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_CANNOT_BE_NULL(item.Name));
                    }
                    catch (Exception ex)
                    {
                        return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_FORMAT_NOT_VALID(item.Name + $" - {ex.Message}"));
                    }
                }
                else
                {
                    if (item.Name == nameof(Product.Id))
                        continue;
                    if (item.Name == nameof(Product.updated_at))
                        continue;
                    if (item.Name == nameof(Product.created_at))
                        continue;
                    return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_CANNOT_BE_NULL(item.Name));
                }
            }

            return new DataResponse { IsSuccessful = true };
        }

        public DataResponse CheckUpdateProperties(Product document)
        {
            if (document.IsNull())
                return ErrorDataResponse(ProductErrorConstant.MODEL_CANNOT_BE_NULL);

            if (document.Id.IsNullOrEmpty())
                return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_CANNOT_BE_NULL(nameof(document.Id)));

            if (this.FindByObjectId(document.Id) == null)
                return ErrorDataResponse(ProductErrorConstant.NOT_FOUND);

            return new DataResponse { IsSuccessful = true };
        }

        public DataResponse CheckDeleteProperties(Product document)
        {
            if (document.IsNull())
                return ErrorDataResponse(ProductErrorConstant.MODEL_CANNOT_BE_NULL);

            if (document.Id.IsNullOrEmpty())
                return ErrorDataResponse(ProductErrorConstant.MODEL_PROPERTY_CANNOT_BE_NULL(nameof(document.Id)));

            if (this.FindByObjectId(document.Id) == null)
                return ErrorDataResponse(ProductErrorConstant.NOT_FOUND);

            return new DataResponse { IsSuccessful = true };
        }

    }
}

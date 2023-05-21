using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Business.Abstraction.Mongo;
using Utility.Project.Core.Data.Abstraction.Mongo;
using Utility.Project.Core.Extensions;
using Utility.Project.Core.Model.Document;
using Utility.Project.Core.Model.Error;
using Utility.Project.Core.Model.Response;

namespace Utility.Project.Core.Business.Concrete.Mongo
{
    public class MongoService<TDocument> : IMongoService<TDocument> where TDocument : IDocument, new()
    {
        private readonly IMongoRepository<TDocument> mongoRepository;

        public MongoService(IMongoRepository<TDocument> _mongoRepository)
        {
            this.mongoRepository = _mongoRepository;
        }


        public virtual IEnumerable<TDocument> GetWithoutFilterPagination(int skipSize, int takeSize)
        {
            return mongoRepository.FilterByPagination(a => true, skipSize, takeSize);
        }
        public virtual IQueryable<TDocument> AsQueryable()
        {
            return mongoRepository.AsQueryable();
        }



        public virtual IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filterExpression)
        {
            return mongoRepository.FilterBy(filterExpression);
        }
        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return mongoRepository.FilterBy(filterExpression);
        }
        public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return mongoRepository.FilterBy(filterExpression, projectionExpression);
        }
        public virtual IEnumerable<TDocument> GetAllWithoutFilter()
        {
            return mongoRepository.FilterBy(a => true);
        }



        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() => mongoRepository.FindByIdAsync(id));
        }
        public virtual TDocument FindByObjectId(string id)
        {
            return mongoRepository.FindByObjectId(id);
        }
        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return mongoRepository.FindOne(filterExpression);
        }
        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => mongoRepository.FindOneAsync(filterExpression));
        }



        public virtual TDocument InsertOne(TDocument document)
        {
            return mongoRepository.InsertOne(document);
        }
        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => mongoRepository.InsertOneAsync(document));
        }
        public virtual void InsertMany(ICollection<TDocument> documents)
        {
            mongoRepository.InsertMany(documents);
        }
        public virtual Task InsertManyAsync(ICollection<TDocument> documents)
        {
            return Task.Run(() => mongoRepository.InsertManyAsync(documents));
        }


        public virtual TDocument ReplaceOne(TDocument document)
        {
            return mongoRepository.ReplaceOne(document);
        }
        public virtual Task ReplaceOneAsync(TDocument document)
        {
            return Task.Run(() => mongoRepository.ReplaceOneAsync(document));
        }



        public virtual void DeleteById(string id)
        {
            mongoRepository.DeleteById(id);
        }
        public bool DeleteOne(string id)
        {
            mongoRepository.DeleteById(id);
            if (mongoRepository.FindByObjectId(id).IsNull())
                return true;
            else
                return false;
        }
        public virtual bool DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            mongoRepository.DeleteOne(filterExpression);
            if (mongoRepository.FindOne(filterExpression).IsNull())
                return true;
            else
                return false;
        }
        public virtual Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => mongoRepository.DeleteOneAsync(filterExpression));
        }
        public virtual bool DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            mongoRepository.DeleteMany(filterExpression);
            if (mongoRepository.FindOne(filterExpression).IsNull())
                return true;
            else
                return false;
        }
        public virtual Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => mongoRepository.DeleteManyAsync(filterExpression));
        }



        public virtual long GetCount()
        {
            return mongoRepository.GetCount();
        }

        public virtual long GetCountBy(FilterDefinition<TDocument> filterExpression)
        {
            return mongoRepository.GetCountBy(filterExpression);
        }


        public DataResponse ErrorDataResponse(BaseServiceErrorConstant model)
        {
            return new DataResponse
            {
                IsSuccessful = false,
                ErrorCode = model.Code,
                ErrorMessageList = new List<string> { model.Message },
                HttpStatusCode = model.HttpStatusCode
            };
        }
    }
}

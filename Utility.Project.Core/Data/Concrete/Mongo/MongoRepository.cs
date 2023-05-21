using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Data.Abstraction.Mongo;
using Utility.Project.Core.Extensions;
using Utility.Project.Core.Model.AppSettings;
using Utility.Project.Core.Model.Document;

namespace Utility.Project.Core.Data.Concrete.Mongo
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public MongoRepository(IMongoDbSettings settings, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;

            var companyIdentifier = this.HttpContextAccessor.HttpContext.Request.Headers["CompanyIdentifier"];

            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(companyIdentifier); //Header'dan gelen customerId'ye göre collection belirlenecek
        }


        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }



        public virtual IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filterExpression)
        {
            return _collection.Find(filterExpression, new FindOptions { AllowDiskUse = true }).ToEnumerable();
        }
        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression, new FindOptions { AllowDiskUse = true }).ToEnumerable();
        }
        public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }
        public virtual IEnumerable<TDocument> FilterByPagination(Expression<Func<TDocument, bool>> filterExpression, int skipSize, int takeSize)
        {
            return _collection.Find(filterExpression, new FindOptions { AllowDiskUse = true }).Skip(skipSize).Limit(takeSize).ToEnumerable();
        }



        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }
        public virtual TDocument FindByObjectId(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }
        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }
        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }


        public virtual TDocument InsertOne(TDocument document)
        {
            do
            {
                document.Id = ObjectId.GenerateNewId().ToString();
                document.created_at = DateTime.Now;
            } while (this.FindByObjectId(document.Id.ToString()).IsNotNull());


            _collection.InsertOne(document);
            return document;
        }
        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() =>
            {
                do
                {
                    document.Id = ObjectId.GenerateNewId().ToString();
                    document.created_at = DateTime.Now;
                } while (this.FindByObjectId(document.Id.ToString()).IsNotNull());

                return _collection.InsertOneAsync(document);
            });
        }
        public void InsertMany(ICollection<TDocument> documents)
        {
            documents.ToList().ForEach(a =>
            {
                a.Id = ObjectId.GenerateNewId().ToString();
                a.created_at = DateTime.Now;
            });
            _collection.InsertMany(documents);
        }
        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            documents.ToList().ForEach(a =>
            {
                a.Id = ObjectId.GenerateNewId().ToString();
                a.created_at = DateTime.Now;
            });
            await _collection.InsertManyAsync(documents);
        }



        public void ReplaceOne(TDocument document)
        {
            document.updated_at = DateTime.Now;
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }
        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            document.updated_at = DateTime.Now;
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }
        TDocument IMongoRepository<TDocument>.ReplaceOne(TDocument document)
        {
            document.updated_at = DateTime.Now;
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            this._collection.FindOneAndReplace(filter, document);
            return document;
        }



        public void DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            _collection.FindOneAndDelete(filter);
        }
        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }
        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }
        
        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }
        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }



        public virtual long GetCount()
        {
            return _collection.CountDocuments(x => true);
        }
        public virtual long GetCountBy(FilterDefinition<TDocument> filterExpression)
        {
            return _collection.CountDocuments(filterExpression);
        }
    }
}

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Project.Core.Model.Document;

namespace Utility.Project.Core.Business.Abstraction.Mongo
{
    public interface IMongoService<TDocument> where TDocument : IDocument
    {
        IEnumerable<TDocument> GetAllWithoutFilter();
        IEnumerable<TDocument> GetWithoutFilterPagination(int skipSize, int takeSize);

        IQueryable<TDocument> AsQueryable();

        IEnumerable<TDocument> FilterBy(FilterDefinition<TDocument> filterExpression);
        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);
        IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression);


        Task<TDocument> FindByIdAsync(string id);
        TDocument FindByObjectId(string id);
        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);


        TDocument InsertOne(TDocument document);
        Task InsertOneAsync(TDocument document);
        void InsertMany(ICollection<TDocument> documents);
        Task InsertManyAsync(ICollection<TDocument> documents);



        TDocument ReplaceOne(TDocument document);
        Task ReplaceOneAsync(TDocument document);



        void DeleteById(string id);
        bool DeleteOne(string id);
        bool DeleteOne(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        bool DeleteMany(Expression<Func<TDocument, bool>> filterExpression);
        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);


        long GetCount();
        long GetCountBy(FilterDefinition<TDocument> filterExpression);
    }
}

using Domain.Common;
using System.Linq.Expressions;

namespace Application.Contracts.Persistence
{
    public interface IMongoRepository
    {
        IQueryable<T> AsQueryable<T>() where T : BaseEntity;

        Task<List<T>> FindAllAsync<T>() where T : BaseEntity;

        IEnumerable<T> FilterBy<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity;

        IEnumerable<TProjected> FilterBy<T, TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression) where T : BaseEntity;

        T FindOne<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity;

        Task<T> FindOneAsync<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity;

        T FindById<T>(string id) where T : BaseEntity;

        Task<T> FindByIdAsync<T>(string id) where T : BaseEntity;

        void InsertOne<T>(T document) where T : BaseEntity;

        Task InsertOneAsync<T>(T document) where T : BaseEntity;

        void InsertMany<T>(ICollection<T> documents) where T : BaseEntity;

        Task InsertManyAsync<T>(ICollection<T> documents) where T : BaseEntity;

        void ReplaceOne<T>(T document) where T : BaseEntity;

        Task ReplaceOneAsync<T>(T document) where T : BaseEntity;

        void DeleteOne<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity;

        Task DeleteOneAsync<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity;

        void DeleteById<T>(string id) where T : BaseEntity;

        Task DeleteByIdAsync<T>(string id) where T : BaseEntity;

        void DeleteMany<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity;

        Task DeleteManyAsync<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity;
        void CreateIndexWithExpiryTime<T>(double timeSpan, string column, string indexName) where T : BaseEntity;
        void CreateIndex<T>(string column, string indexName) where T : BaseEntity;
    }
}

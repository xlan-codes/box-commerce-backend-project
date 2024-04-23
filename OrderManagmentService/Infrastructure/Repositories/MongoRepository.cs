using System.Linq.Expressions;
using Application.Contracts.Persistence;
using Domain.Attributes;
using Domain.Common;
using Infrastructure.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private IMongoDatabase database;

        public MongoRepository(IMongoDbSettings settings)
        {
            database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        }

        public virtual async Task<IAsyncCursor<T>> FindAllCursorAsync<T>(int batchSize = 20) where T : BaseEntity
        {
            var filter = Builders<T>.Filter.Empty;
            var options = new FindOptions
            {
                BatchSize = batchSize
            };

            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return await _collection.Find(filter, options).ToCursorAsync();
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<T> AsQueryable<T>() where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);

            return _collection.AsQueryable();
        }

        public virtual IEnumerable<T> FilterBy<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public void CreateIndexWithExpiryTime<T>(double timeSpan, string column, string indexName) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            var indexModel = new CreateIndexModel<T>(
                keys: Builders<T>.IndexKeys.Descending(column),
                options: new CreateIndexOptions
                {
                    ExpireAfter = TimeSpan.FromSeconds(timeSpan),
                    Name = indexName
                });
            _collection.Indexes.CreateOne(indexModel);
        }

        public void CreateIndex<T>(string column, string indexName) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            var indexModel = new CreateIndexModel<T>(
                keys: Builders<T>.IndexKeys.Descending(column),
                options: new CreateIndexOptions
                {
                    Name = indexName
                });
            _collection.Indexes.CreateOne(indexModel);
        }


        public virtual async Task<List<T>> FindAllAsync<T>() where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return await _collection.Find(_ => true).ToListAsync();
        }

        public virtual IEnumerable<TProjected> FilterBy<T, TProjected>(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, TProjected>> projectionExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual T FindOne<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<T> FindOneAsync<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual T FindById<T>(string id) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<T> FindByIdAsync<T>(string id) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }


        public virtual void InsertOne<T>(T document) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            _collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync<T>(T document) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void InsertMany<T>(ICollection<T> documents) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            _collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync<T>(ICollection<T> documents) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            await _collection.InsertManyAsync(documents);
        }

        public void ReplaceOne<T>(T document) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            _collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync<T>(T document) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
            await _collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById<T>(string id) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync<T>(string id) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync<T>(Expression<Func<T, bool>> filterExpression) where T : BaseEntity
        {
            IMongoCollection<T> _collection = database.GetCollection<T>(typeof(T).Name);
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }
    }
}

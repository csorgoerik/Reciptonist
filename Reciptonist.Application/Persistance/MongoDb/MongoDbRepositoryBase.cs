using MongoDB.Bson;
using MongoDB.Driver;
using Reciptonist.Application.Persistance.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reciptonist.Application.Persistance.MongoDb;
public class MongoDbRepositoryBase<TDocument> : IDisposable where TDocument : IDocument
{
    protected readonly IMongoCollection<TDocument> collection;

    public MongoDbRepositoryBase(IMongoDbSettings settings)
    {
        var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
    }

    public virtual IQueryable<TDocument> AsQueryable()
    {
        return collection.AsQueryable();
    }

    public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
    {
        return collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
    {
        return collection.Find(filterExpression).FirstOrDefault();
    }

    public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => collection.Find(filterExpression).FirstOrDefaultAsync());
    }

    public virtual TDocument FindById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        return collection.Find(filter).SingleOrDefault();
    }

    public virtual async Task<TDocument> FindByIdAsync(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        var res = await collection.FindAsync(filter);
        return await res.SingleOrDefaultAsync();
    }


    public virtual void InsertOne(TDocument document)
    {
        collection.InsertOne(document);
    }

    public virtual async Task InsertOneAsync(TDocument document)
    {
        await collection.InsertOneAsync(document);
    }

    public void InsertMany(ICollection<TDocument> documents)
    {
        collection.InsertMany(documents);
    }


    public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
    {
        await collection.InsertManyAsync(documents);
    }

    public void ReplaceOne(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        collection.FindOneAndReplace(filter, document);
    }

    public virtual async Task ReplaceOneAsync(TDocument document)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
        await collection.FindOneAndReplaceAsync(filter, document);
    }

    public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
    {
        collection.FindOneAndDelete(filterExpression);
    }

    public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        await collection.FindOneAndDeleteAsync(filterExpression);
    }

    public void DeleteById(string id)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        collection.FindOneAndDelete(filter);
    }

    public async Task DeleteByIdAsync(string id)
    {       
        var objectId = new ObjectId(id);
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
        await collection.FindOneAndDeleteAsync(filter);
    }

    public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
    {
        collection.DeleteMany(filterExpression);
    }

    public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        await collection.DeleteManyAsync(filterExpression);
    }

    public void Dispose()
    {

    }
}

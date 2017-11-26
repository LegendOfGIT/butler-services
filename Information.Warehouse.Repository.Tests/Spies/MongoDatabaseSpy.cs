using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Information.Warehouse.Repository.MongoDatabase;

namespace Information.Warehouse.Repository.Tests.Spies
{
  public class MongoDatabaseSpy : IMongoDatabase
  {
    private Dictionary<string, IMongoCollection<InformationEntry>> collections;
    public string LastSelectedCollectionName { get; set; }

    public MongoDatabaseSpy(Dictionary<string, IMongoCollection<InformationEntry>> collections = null)
    {
      this.collections = collections;
    }

    public IMongoClient Client => throw new System.NotImplementedException();

    public DatabaseNamespace DatabaseNamespace => throw new System.NotImplementedException();

    public MongoDatabaseSettings Settings => throw new System.NotImplementedException();

    public void CreateCollection(string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task CreateCollectionAsync(string name, CreateCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public void CreateView<TDocument, TResult>(string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task CreateViewAsync<TDocument, TResult>(string viewName, string viewOn, PipelineDefinition<TDocument, TResult> pipeline, CreateViewOptions<TDocument> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public void DropCollection(string name, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task DropCollectionAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public IMongoCollection<TDocument> GetCollection<TDocument>(string name, MongoCollectionSettings settings = null)
    {
      this.LastSelectedCollectionName = name;
      return
        this.collections != null && this.collections.ContainsKey(name)
          ? this.collections[name] as IMongoCollection<TDocument>
          : default(IMongoCollection<TDocument>);
    }

    public IAsyncCursor<BsonDocument> ListCollections(ListCollectionsOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<IAsyncCursor<BsonDocument>> ListCollectionsAsync(ListCollectionsOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public void RenameCollection(string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task RenameCollectionAsync(string oldName, string newName, RenameCollectionOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public TResult RunCommand<TResult>(Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<TResult> RunCommandAsync<TResult>(Command<TResult> command, ReadPreference readPreference = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public IMongoDatabase WithReadConcern(ReadConcern readConcern)
    {
      throw new System.NotImplementedException();
    }

    public IMongoDatabase WithReadPreference(ReadPreference readPreference)
    {
      throw new System.NotImplementedException();
    }

    public IMongoDatabase WithWriteConcern(WriteConcern writeConcern)
    {
      throw new System.NotImplementedException();
    }
  }
}

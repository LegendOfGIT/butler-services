using MongoDB.Driver;
using Information.Store.Repository.MongoDatabase;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Information.Store.Repository.Tests.Stubs
{
  public class MongoCollectionReturnsSpecificDocumentsStub : IMongoCollection<InformationEntry>
  {
    public InformationEntry LastInsertedEntry { get; set; }
    public IEnumerable<InformationEntry> ReplacedEntries { get; set; }

    public static DateTime DiscoveryTimestamp = new DateTime(2017, 11, 11, 23, 42, 0);

    private IAsyncCursor<InformationEntry> findCursor;

    public MongoCollectionReturnsSpecificDocumentsStub(IAsyncCursor<InformationEntry> findCursor)
    {
      this.findCursor = findCursor;
    }

    public CollectionNamespace CollectionNamespace => throw new System.NotImplementedException();

    public IMongoDatabase Database => throw new System.NotImplementedException();

    public IBsonSerializer<InformationEntry> DocumentSerializer => throw new System.NotImplementedException();

    public IMongoIndexManager<InformationEntry> Indexes => throw new System.NotImplementedException();

    public MongoCollectionSettings Settings => throw new System.NotImplementedException();

    public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<InformationEntry, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<InformationEntry, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public BulkWriteResult<InformationEntry> BulkWrite(IEnumerable<WriteModel<InformationEntry>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<BulkWriteResult<InformationEntry>> BulkWriteAsync(IEnumerable<WriteModel<InformationEntry>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public long Count(FilterDefinition<InformationEntry> filter, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<long> CountAsync(FilterDefinition<InformationEntry> filter, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public DeleteResult DeleteMany(FilterDefinition<InformationEntry> filter, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public DeleteResult DeleteMany(FilterDefinition<InformationEntry> filter, DeleteOptions options, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<DeleteResult> DeleteManyAsync(FilterDefinition<InformationEntry> filter, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<DeleteResult> DeleteManyAsync(FilterDefinition<InformationEntry> filter, DeleteOptions options, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public DeleteResult DeleteOne(FilterDefinition<InformationEntry> filter, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public DeleteResult DeleteOne(FilterDefinition<InformationEntry> filter, DeleteOptions options, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<DeleteResult> DeleteOneAsync(FilterDefinition<InformationEntry> filter, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<DeleteResult> DeleteOneAsync(FilterDefinition<InformationEntry> filter, DeleteOptions options, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<InformationEntry, TField> field, FilterDefinition<InformationEntry> filter, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<InformationEntry, TField> field, FilterDefinition<InformationEntry> filter, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<InformationEntry> filter, FindOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public TProjection FindOneAndDelete<TProjection>(FilterDefinition<InformationEntry> filter, FindOneAndDeleteOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<InformationEntry> filter, FindOneAndDeleteOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public TProjection FindOneAndReplace<TProjection>(FilterDefinition<InformationEntry> filter, InformationEntry replacement, FindOneAndReplaceOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<InformationEntry> filter, InformationEntry replacement, FindOneAndReplaceOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<InformationEntry> filter, UpdateDefinition<InformationEntry> update, FindOneAndUpdateOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<InformationEntry> filter, UpdateDefinition<InformationEntry> update, FindOneAndUpdateOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<InformationEntry> filter, FindOptions<InformationEntry, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      return this.findCursor as IAsyncCursor<TProjection>;
    }

    public void InsertMany(IEnumerable<InformationEntry> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task InsertManyAsync(IEnumerable<InformationEntry> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public void InsertOne(InformationEntry document, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      document.DiscoveryTimestamp = DiscoveryTimestamp;
      this.LastInsertedEntry = document;
    }

    public Task InsertOneAsync(InformationEntry document, CancellationToken _cancellationToken)
    {
      throw new System.NotImplementedException();
    }

    public Task InsertOneAsync(InformationEntry document, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<InformationEntry, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<InformationEntry, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public ReplaceOneResult ReplaceOne(FilterDefinition<InformationEntry> filter, InformationEntry replacement, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      this.ReplacedEntries = this.ReplacedEntries ?? Enumerable.Empty<InformationEntry>();
      this.ReplacedEntries = this.ReplacedEntries.Concat(new[] { replacement });
      return default(ReplaceOneResult);
    }

    public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<InformationEntry> filter, InformationEntry replacement, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public UpdateResult UpdateMany(FilterDefinition<InformationEntry> filter, UpdateDefinition<InformationEntry> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<UpdateResult> UpdateManyAsync(FilterDefinition<InformationEntry> filter, UpdateDefinition<InformationEntry> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public UpdateResult UpdateOne(FilterDefinition<InformationEntry> filter, UpdateDefinition<InformationEntry> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public Task<UpdateResult> UpdateOneAsync(FilterDefinition<InformationEntry> filter, UpdateDefinition<InformationEntry> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      throw new System.NotImplementedException();
    }

    public IMongoCollection<InformationEntry> WithReadConcern(ReadConcern readConcern)
    {
      throw new System.NotImplementedException();
    }

    public IMongoCollection<InformationEntry> WithReadPreference(ReadPreference readPreference)
    {
      throw new System.NotImplementedException();
    }

    public IMongoCollection<InformationEntry> WithWriteConcern(WriteConcern writeConcern)
    {
      throw new System.NotImplementedException();
    }

    IFilteredMongoCollection<TDerivedDocument> IMongoCollection<InformationEntry>.OfType<TDerivedDocument>()
    {
      throw new System.NotImplementedException();
    }
  }
}

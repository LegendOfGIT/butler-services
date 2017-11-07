using Information.Store.Repository.Entity;
using Information.Store.Repository.MongoDatabase;
using Information.Store.Repository.Tests.Spies;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace Information.Store.Repository.Tests
{
  public class StoreInformationMongoDatabaseRepositoryTests
  {
    [Fact]
    public void RepositorySelectsCollectionInformationFromDatabase()
    {      
      var database = new MongoDatabaseSpy();
      var repository = new StoreInformationMongoDatabaseRepository(database);
      repository.StoreInformation(null);
      Assert.Equal("information", database.LastSelectedCollectionName);
    }

    [Fact]
    public void RepositoryInsertsInformationAsNewRecord()
    {
      var informationCollection = new MongoCollectionSpy();
      var database = new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });

      var repository = new StoreInformationMongoDatabaseRepository(database);
      repository.StoreInformation(new InformationEntity
      {
        Properties = new[]
        {
          new InformationPropertyEntity { Name = "title", Values = new object[]{ "sweet shoes" } },
          new InformationPropertyEntity { Name = "color", Values = new object[]{ "red" } },
          new InformationPropertyEntity { Name = "price", Values = new object[]{ (decimal)12.45 } },
          new InformationPropertyEntity { Name = "currency", Values = new object[]{ "€" } },
          new InformationPropertyEntity { Name = "size", Values = new object[]{ 43 } }
        }
      });

      var insertedEntry = informationCollection.LastInsertedEntry;
      var expectedEntry = new InformationEntry
      {
        Properties = new[]
        {
          new InformationPropertyEntry { Name = "color", Values = new BsonString[]{ "red" } },
          new InformationPropertyEntry { Name = "currency", Values = new BsonString[]{ "€" } },
          new InformationPropertyEntry { Name = "price", Values = new BsonDecimal128[]{ new BsonDecimal128((decimal)12.45) } },
          new InformationPropertyEntry { Name = "size", Values = new BsonValue[]{ new BsonInt32(43) } },
          new InformationPropertyEntry { Name = "title", Values = new BsonString[]{ "sweet shoes" } }
        }
      };
      Assert.Equal(
        JsonConvert.SerializeObject(expectedEntry),
        JsonConvert.SerializeObject(insertedEntry)
      );
    }
  }
}

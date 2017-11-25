using Information.Store.Repository.Entity;
using Information.Store.Repository.MongoDatabase;
using Information.Store.Repository.Tests.Spies;
using Information.Store.Repository.Tests.Stubs;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
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
    public void RepositoryInsertsInformationWithActiveFlagAndDiscoveryTimestampWhenInformationDoesNotAlreadyExist()
    {
      var informationCollection = new MongoCollectionReturnsSpecificDocumentsStub(new List<InformationEntry>());
      var database = new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });

      var id = "123-ABC";
      var repository = new StoreInformationMongoDatabaseRepository(database);
      repository.StoreInformation(new InformationEntity { Id = id });

      var expectedEntry = new InformationEntry { InformationId = id, IsActive = true, DiscoveryTimestamp = MongoCollectionReturnsSpecificDocumentsStub.DiscoveryTimestamp };
      Assert.Equal(
        JsonConvert.SerializeObject(expectedEntry),
        JsonConvert.SerializeObject(informationCollection.LastInsertedEntry)
      );
    }

    [Fact]
    public void RepositoryInsertsInformationWithActiveFlagAndResetsActiveFlagsOfOlderVersionsWhenInformationDoesAlreadyExist()
    {
      var existingEntries = new[]{
        new InformationEntry{ InformationId = "234-BCD", IsActive = false, DiscoveryTimestamp = new DateTime(2017, 2, 3) },
        new InformationEntry{ InformationId = "234-BCD", IsActive = true, DiscoveryTimestamp = new DateTime(2017, 10, 4) }
      };
      var informationCollection = new MongoCollectionReturnsSpecificDocumentsStub(existingEntries);
      var database = new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });

      var id = "234-BCD";
      var repository = new StoreInformationMongoDatabaseRepository(database);
      repository.StoreInformation(new InformationEntity { Id = id, Properties = new[] { new InformationPropertyEntity {
        Name = "titles",
        Values = new[] { "title A" }
      }}});

      var expectedEntry = new InformationEntry { InformationId = id, IsActive = true, DiscoveryTimestamp = MongoCollectionReturnsSpecificDocumentsStub.DiscoveryTimestamp,
        Properties = new[] { new InformationPropertyEntry {
        Name = "titles",
        Values = new BsonValue[] { "title A" }
      }}};
      Assert.Equal(
        JsonConvert.SerializeObject(expectedEntry),
        JsonConvert.SerializeObject(informationCollection.LastInsertedEntry)
      );

      existingEntries[1].IsActive = false;
      Assert.Equal(
        JsonConvert.SerializeObject(existingEntries),
        JsonConvert.SerializeObject(informationCollection.ReplacedEntries)
      );
    }

    [Fact]
    public void RepositoryInsertNoInformationWhenIdenticalActiveInformationExists()
    {
      var existingEntries = new[]{
        new InformationEntry{ InformationId = "234-BCD", IsActive = false, DiscoveryTimestamp = new DateTime(2017, 2, 3) },
        new InformationEntry{
          InformationId = "234-BCD",
          IsActive = true,
          DiscoveryTimestamp = new DateTime(2017, 10, 4),
          Properties = new[]
          {
            new InformationPropertyEntry{ Name = "titles", Values = new BsonValue[]{ "information A" } }
          }
        }
      };
      var informationCollection = new MongoCollectionReturnsSpecificDocumentsStub(existingEntries);
      var database = new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });

      var id = "234-BCD";
      var repository = new StoreInformationMongoDatabaseRepository(database);
      repository.StoreInformation(new InformationEntity {
        Id = id,
        Properties = new[]
        {
          new InformationPropertyEntity
          {
            Name = "titles", Values = new[]{ "information A" }
          }
        }
      });

      Assert.Null(informationCollection.ReplacedEntries);
    }

    [Fact]
    public void RepositoryInsertNoInformationWhenIdenticalActiveInformationWithBamboozledPropertiesExists()
    {
      var existingEntries = new[]{
        new InformationEntry{ InformationId = "234-BCD", IsActive = false, DiscoveryTimestamp = new DateTime(2017, 2, 3) },
        new InformationEntry{
          InformationId = "234-BCD",
          IsActive = true,
          DiscoveryTimestamp = new DateTime(2017, 10, 4),
          Properties = new[]
          {
            new InformationPropertyEntry{ Name = "titles", Values = new BsonValue[]{ "information A", "information B", "information C" } },
            new InformationPropertyEntry{ Name = "prices", Values = new BsonValue[]{ 12.50, 22.60 } },
          }
        }
      };
      var informationCollection = new MongoCollectionReturnsSpecificDocumentsStub(existingEntries);
      var database = new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });

      var id = "234-BCD";
      var repository = new StoreInformationMongoDatabaseRepository(database);
      repository.StoreInformation(new InformationEntity
      {
        Id = id,
        Properties = new[]
        {
          new InformationPropertyEntity{ Name = "prices", Values = new BsonValue[]{ 22.60, 12.50 } },
          new InformationPropertyEntity{ Name = "titles", Values = new[]{ "information B", "information C", "information A" } }
        }
      });

      Assert.Null(informationCollection.ReplacedEntries);
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
        IsActive = true,
        Properties = new[]
        {
          new InformationPropertyEntry { Name = "color", Values = new BsonString[]{ "red" } },
          new InformationPropertyEntry { Name = "currency", Values = new BsonString[]{ "€" } },
          new InformationPropertyEntry { Name = "price", Values = new BsonDecimal128[]{ new BsonDecimal128((decimal)12.45) } },
          new InformationPropertyEntry { Name = "size", Values = new BsonValue[]{ new BsonInt32(43) } },
          new InformationPropertyEntry { Name = "title", Values = new BsonString[]{ "sweet shoes" } }
        },
        DiscoveryTimestamp = MongoCollectionSpy.DiscoveryTimestamp
      };
      Assert.Equal(
        JsonConvert.SerializeObject(expectedEntry),
        JsonConvert.SerializeObject(insertedEntry)
      );
    }

    private MongoDatabaseSpy GetDatabaseSpy(InformationEntry[] entries)
    {
      var informationCollection = new MongoCollectionReturnsSpecificDocumentsStub(entries);
      return new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });
    }
  }
}

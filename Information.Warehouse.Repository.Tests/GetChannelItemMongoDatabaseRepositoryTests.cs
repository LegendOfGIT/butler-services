using Information.Warehouse.Entity;
using Information.Warehouse.Repository.MongoDatabase;
using Information.Warehouse.Repository.Tests.Spies;
using Information.Warehouse.Repository.Tests.Stubs;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Information.Warehouse.Repository.Tests
{
  public class GetChannelItemMongoDatabaseRepositoryTests
  {
    [Fact]
    public void RepositorySelectsCollectionInformationFromDatabase()
    {
      var database = new MongoDatabaseSpy();
      var repository = new GetChannelItemMongoDatabaseRepository(database);
      repository.GetChannelItem(string.Empty);
      Assert.Equal("information", database.LastSelectedCollectionName);
    }

    [Fact]
    public void RepositoryReturnsChannelItemFromDatabase()
    {
      var informationCollection = new MongoCollectionReturnsSpecificDocumentsStub(new List<InformationEntry>
      {
        new InformationEntry{
          InformationId = "Nintendo.de.Zelda-Twilight-Princess",
          Properties = new[]
          {
//            new InformationPropertyEntry{ Name = "titles", Values = new BsonValue[]{ "The Legend of Zelda: Twilight Princess" } },
            new InformationPropertyEntry{ Name = "release-dates", Values = new BsonValue[]{ new DateTime(2016, 3, 4).Ticks } }
          }
        },
        new InformationEntry{
          InformationId = "Steam.de.Sims4",
          Properties = new[]
          {
            new InformationPropertyEntry{ Name = "titles", Values = new BsonValue[]{ "Die Sims 4" } },
            new InformationPropertyEntry{ Name = "release-dates", Values = new BsonValue[]{ new DateTime(2014, 9, 2) } }
          }
        }
      });
      var database = new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });

      var repository = new GetChannelItemMongoDatabaseRepository(database);
      var channelItem = repository.GetChannelItem("Nintendo.de.Zelda-Twilight-Princess");

      Assert.Equal(
        JsonConvert.SerializeObject(new ChannelItem
        {
          Id = "Nintendo.de.Zelda-Twilight-Princess",
          Properties = new Dictionary<string, IEnumerable<object>>
          {
            //{ "titles", new object[]{ "The Legend of Zelda: Twilight Princess" } },
            { "release-dates", new object[]{ new DateTime(2016, 3, 4) } }
          }
        }),
        JsonConvert.SerializeObject(channelItem)
      );
    }

  }
}

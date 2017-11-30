using Information.Warehouse.Entity;
using Information.Warehouse.Repository.MongoDatabase;
using Information.Warehouse.Repository.Tests.Spies;
using Information.Warehouse.Repository.Tests.Stubs;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace Information.Warehouse.Repository.Tests
{
  public class GetChannelMongoDatabaseRepositoryTests
  {
    [Fact]
    public void RepositorySelectsCollectionInformationFromDatabase()
    {
      var database = new MongoDatabaseSpy();
      var repository = new GetChannelMongoDatabaseRepository(database);
      repository.GetChannel();
      Assert.Equal("information", database.LastSelectedCollectionName);
    }

    [Fact]
    public void RepositoryReturnsChannelItemsFromDatabase()
    {
      var informationCollection = new MongoCollectionReturnsSpecificDocumentsStub(new List<InformationEntry>
      {
        new InformationEntry{ InformationId = "Steam.de.Sims4" },
        new InformationEntry{ InformationId = "Nintendo.de.Zelda-Twilight-Princess" }
      });
      var database = new MongoDatabaseSpy(new Dictionary<string, IMongoCollection<InformationEntry>> {
        { "information", informationCollection }
      });

      var repository = new GetChannelMongoDatabaseRepository(database);
      var channel = repository.GetChannel();

      var expectedInformationIds = new[]
      {
        "Steam.de.Sims4",
        "Nintendo.de.Zelda-Twilight-Princess"
      };

      Assert.Equal(
        JsonConvert.SerializeObject(new Channel { InformationItemIds = expectedInformationIds }),
        JsonConvert.SerializeObject(channel)
      );
    }
  }
}

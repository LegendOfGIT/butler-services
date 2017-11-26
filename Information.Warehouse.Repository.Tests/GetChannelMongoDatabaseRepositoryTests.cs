using Information.Warehouse.Entity;
using Information.Warehouse.Repository.Tests.Spies;
using Newtonsoft.Json;
using Xunit;

namespace Information.Warehouse.Repository.Tests
{
  public class GetChannelMongoDatabaseRepositoryTests
  {
    [Fact]
    public void RepositoryReturnsChannelInformation()
    {
      var repository = new GetChannelMongoDatabaseRepository(new MongoDatabaseSpy());
      var channel = repository.GetChannel();
      Assert.Equal(
        JsonConvert.SerializeObject(new Channel()),
        JsonConvert.SerializeObject(channel)
      );
    }

    [Fact]
    public void RepositorySelectsCollectionInformationFromDatabase()
    {
      var database = new MongoDatabaseSpy();
      var repository = new GetChannelMongoDatabaseRepository(database);
      repository.GetChannel();
      Assert.Equal("information", database.LastSelectedCollectionName);
    }
  }
}

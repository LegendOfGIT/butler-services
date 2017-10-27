using Information.Store.Repository.MongoDatabase;
using Information.Store.Repository.Tests.Spies;
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
  }
}

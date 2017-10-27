using Information.Store.Repository.Entity;
using MongoDB.Driver;

namespace Information.Store.Repository.MongoDatabase
{
  public class StoreInformationMongoDatabaseRepository : IStoreInformationRepository
  {
    private IMongoDatabase Database;

    public StoreInformationMongoDatabaseRepository(IMongoDatabase Database)
    {
      this.Database = Database;
    }

    public void StoreInformation(InformationEntity information)
    {
      var informationCollection = this.Database.GetCollection<InformationEntity>("information");
    }
  }
}

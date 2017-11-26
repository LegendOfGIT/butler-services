using Information.Warehouse.Entity;
using Information.Warehouse.Repository.MongoDatabase;
using MongoDB.Driver;

namespace Information.Warehouse.Repository
{
  public class GetChannelMongoDatabaseRepository : IGetChannelRepository
  {
    private IMongoDatabase Database;

    public GetChannelMongoDatabaseRepository(IMongoDatabase Database)
    {
      this.Database = Database;
    }

    public Channel GetChannel()
    {
      var informationCollection = this.Database.GetCollection<InformationEntry>("information");

      return new Channel();
    }
  }
}

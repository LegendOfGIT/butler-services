using Information.Warehouse.Entity;
using Information.Warehouse.Repository.MongoDatabase;
using MongoDB.Driver;
using System.Linq;

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
      var informationItemIds = Enumerable.Empty<string>();

      var informationCollection = this.Database.GetCollection<InformationEntry>("information");
      if (informationCollection != null)
      {
        informationItemIds = informationCollection.Find(item => true).ToList().Select(
          item => item.InformationId
        );
      }

      return new Channel { InformationItemIds = informationItemIds };
    }
  }
}

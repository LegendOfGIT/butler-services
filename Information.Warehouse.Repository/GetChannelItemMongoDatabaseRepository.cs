using Information.Warehouse.Entity;
using Information.Warehouse.Repository.MongoDatabase;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Information.Warehouse.Repository
{
  public class GetChannelItemMongoDatabaseRepository : IGetChannelItemRepository
  {
    private IMongoDatabase Database;

    public GetChannelItemMongoDatabaseRepository(IMongoDatabase Database)
    {
      this.Database = Database;
    }

    public ChannelItem GetChannelItem(string channelItemId)
    {
      var informationCollection = this.Database.GetCollection<InformationEntry>("information");
      var informationItem = informationCollection.Find(item => item.InformationId == channelItemId && item.IsActive).First();

      return new ChannelItem
      {
        Id = channelItemId,
        Properties = informationItem.Properties.ToDictionary(item => item.Name, item => item.Values.Select(value => (object) new DateTime(value.AsInt64)))
      };
    }
  }
}

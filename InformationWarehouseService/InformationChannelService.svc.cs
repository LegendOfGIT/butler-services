using Information.Warehouse.Usecase;
using Information.Warehouse.Repository;
using MongoDB.Driver;

namespace InformationWarehouse
{
  public class InformationChannelService : IInformationChannelService
  {
    public GetChannelResponse GetChannel(string channelId)
    {
      var repository = new GetChannelMongoDatabaseRepository(this.GetMongoDatabase());
      var interactor = new GetChannelInteractor(repository);

      var response = interactor.Execute();
      response.ChannelId = channelId;
      return response;
    }

    public GetChannelItemResponse GetChannelItem(string channelItemId)
    {
      var repository = new GetChannelItemMongoDatabaseRepository(this.GetMongoDatabase());
      var interactor = new GetChannelItemInteractor(repository);

      var response = interactor.Execute(channelItemId);
      response.ChannelItemId = channelItemId;
      return response;
    }

    private IMongoDatabase GetMongoDatabase()
    {
      var mongoClient = new MongoClient();
      return mongoClient.GetDatabase("information-store");
    }
  }
}

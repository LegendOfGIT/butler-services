using Information.Warehouse.Interactor;
using Information.Warehouse.Repository;
using MongoDB.Driver;

namespace InformationWarehouse
{
  public class InformationChannelService : IInformationChannelService
  {
    public GetChannelResponse GetChannel(string channelId)
    {
      var mongoClient = new MongoClient();
      var repository = new GetChannelMongoDatabaseRepository(mongoClient.GetDatabase("information-store"));
      var interactor = new GetChannelInteractor(repository);          

      return interactor.Execute();
    }
  }
}

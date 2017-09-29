using Information.Warehouse.Entity;
using Information.Warehouse.Repository;

namespace Information.Warehouse.Interactor.Tests.Repository.Spies
{
  public class GetChannelRepositoryReturnsChannelInformationStub : IGetChannelRepository
  {
    public const string ChannelId = "654321";

    public Channel GetChannel()
    {
      return new Channel
      {
        Id = ChannelId
      };
    }
  }
}

using Information.Warehouse.Entity;
using Information.Warehouse.Repository;

namespace Information.Warehouse.Interactor.Tests.Repository.Spies
{
  public class GetChannelRepositoryReturnsChannelInformationStub : IGetChannelRepository
  {
    private Channel channelEntity;

    public GetChannelRepositoryReturnsChannelInformationStub(Channel channelEntity)
    {
      this.channelEntity = channelEntity;
    }

    public Channel GetChannel()
    {
      return this.channelEntity;
    }
  }
}

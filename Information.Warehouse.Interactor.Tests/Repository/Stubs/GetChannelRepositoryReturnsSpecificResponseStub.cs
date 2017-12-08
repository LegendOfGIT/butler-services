using Information.Warehouse.Entity;
using Information.Warehouse.Repository;

namespace Information.Warehouse.Usecase.Tests.Repository.Spies
{
  public class GetChannelRepositoryReturnsSpecificResponseStub : IGetChannelRepository
  {
    private Channel channelEntity;

    public GetChannelRepositoryReturnsSpecificResponseStub(Channel channelEntity)
    {
      this.channelEntity = channelEntity;
    }

    public Channel GetChannel()
    {
      return this.channelEntity;
    }
  }
}

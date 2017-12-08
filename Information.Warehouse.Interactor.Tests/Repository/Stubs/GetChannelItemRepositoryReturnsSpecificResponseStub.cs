using Information.Warehouse.Entity;
using Information.Warehouse.Repository;

namespace Information.Warehouse.Usecase.Tests.Repository.Spies
{
  public class GetChannelItemRepositoryReturnsSpecificResponseStub : IGetChannelItemRepository
  {
    private ChannelItem channelItemEntity;

    public GetChannelItemRepositoryReturnsSpecificResponseStub(ChannelItem channelItemEntity)
    {
      this.channelItemEntity = channelItemEntity;
    }

    public ChannelItem GetChannelItem(string channelItemId)
    {
      return this.channelItemEntity;
    }
  }
}

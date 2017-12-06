using Information.Warehouse.Entity;

namespace Information.Warehouse.Repository
{
  public interface IGetChannelItemRepository
  {
    ChannelItem GetChannelItem(string channelItemId);
  }
}

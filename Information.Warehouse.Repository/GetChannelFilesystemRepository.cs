using Information.Warehouse.Entity;

namespace Information.Warehouse.Repository
{
  public class GetChannelFilesystemRepository : IGetChannelRepository
  {
    public Channel GetChannel()
    {
      return new Channel();
    }
  }
}

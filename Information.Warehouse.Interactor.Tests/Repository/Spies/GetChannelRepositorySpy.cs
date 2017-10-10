using Information.Warehouse.Entity;
using Information.Warehouse.Repository;

namespace Information.Warehouse.Interactor.Tests.Repository.Spies
{
  public class GetChannelRepositorySpy : IGetChannelRepository
  {
    public int GetChannelCalls { get; set; }

    public Channel GetChannel()
    {
      this.GetChannelCalls++;
      return new Channel();
    }
  }
}

using Information.Warehouse.Entity;
using Information.Warehouse.Repository;
using System.Collections.Generic;

namespace Information.Warehouse.Usecase.Tests.Repository.Spies
{
  public class GetChannelItemRepositorySpy : IGetChannelItemRepository
  {
    public const string ParameterIdChannelItemId = "channelItemId";

    public int GetChannelItemCalls { get; set; }
    public Dictionary<string, object> LastUsedParameters { get; set; }

    public ChannelItem GetChannelItem(string channelItemId)
    {
      this.GetChannelItemCalls++;
      this.LastUsedParameters = new Dictionary<string, object>();
      this.LastUsedParameters[ParameterIdChannelItemId] = channelItemId;

      return null;
    }
  }
}

using Information.Warehouse.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Information.Warehouse.Usecase
{
  public class GetChannelItemInteractor
  {
    private IGetChannelItemRepository GetChannelItemRepository;

    public GetChannelItemInteractor(IGetChannelItemRepository GetChannelItemRepository)
    {
      this.GetChannelItemRepository = GetChannelItemRepository;
    }

    public GetChannelItemResponse Execute(string channelItemId)
    {
      var channelItem = this.GetChannelItemRepository.GetChannelItem(channelItemId);
      var channelItemProperties = channelItem?.Properties;

      return new GetChannelItemResponse
      {
        Title = GetInformationFromChannelItemProperties(channelItemProperties, "titles")
      };
    }

    private string GetInformationFromChannelItemProperties(Dictionary<string, IEnumerable<object>> properties, string key)
    {
      properties = properties ?? new Dictionary<string, IEnumerable<object>>();

      return 
        properties.ContainsKey(key) 
          ? properties[key].FirstOrDefault() as string 
          : null;
    }
  }
}

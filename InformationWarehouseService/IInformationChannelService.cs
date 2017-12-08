using Information.Warehouse.Usecase;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace InformationWarehouse
{
  [ServiceContract]
  public interface IInformationChannelService
  {
    [OperationContract]
    [WebGet(UriTemplate = "/GetChannel/{channelId}", ResponseFormat = WebMessageFormat.Json)]
    GetChannelResponse GetChannel(string channelId);

    [OperationContract]
    [WebGet(UriTemplate = "/GetChannelItem/{channelItemId}", ResponseFormat = WebMessageFormat.Json)]
    GetChannelItemResponse GetChannelItem(string channelItemId);
  }
}
